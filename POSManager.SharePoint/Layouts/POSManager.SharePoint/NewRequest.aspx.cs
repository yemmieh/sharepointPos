using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.Services;
using POSManager.Domain.Logic.Abstract;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using POSManager.Domain.Models;
using Newtonsoft.Json;
using POSManager.Domain.Logic;
using System.IO;
using Microsoft.SharePoint.Utilities;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class NewRequest : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDropDown();
                }
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void BindDropDown()
        {
            MerchantCategory.DataSource = POSCache.GetPOSOption(Domain.Models.POSOption.MERCHANT_CATEGORY);
            MerchantCategory.DataBind();
            MerchantCategory.Items.Insert(0, new ListItem("Select Merchant Category", ""));
            MerchantCategory.SelectedIndex = -1;

            DescriptionOfBusiness.DataSource = POSCache.GetPOSOption(POSOption.DESCRIPTION_OF_BUSINESS);
            DescriptionOfBusiness.DataBind();
            DescriptionOfBusiness.Items.Insert(0, new ListItem("Select Description of Business", ""));
            DescriptionOfBusiness.SelectedIndex = -1;

            POSType.DataSource = Domain.Models.POSType.GetAll();
            POSType.DataBind();
            POSType.Items.Insert(0, new ListItem("Select POS Type", ""));
            POSType.SelectedIndex = -1;

            BusinessClassification.DataSource = POSCache.GetPOSOption(POSOption.MERCHANT_BUSINESS_CLASSIFICATION);
            BusinessClassification.DataBind();
            BusinessClassification.Items.Insert(0, new ListItem("Select Business Classification", ""));
            BusinessClassification.SelectedIndex = -1;

            LocationCity.DataSource = POSCache.GetPOSOption(Domain.Models.POSOption.CITY);
            LocationCity.DataBind();
            LocationCity.Items.Insert(0, new ListItem("Select City", ""));
            LocationCity.SelectedIndex = -1;

            RequestingBranch.DataSource = POSCache.GetBranchList();
            RequestingBranch.DataTextField = "Name";
            RequestingBranch.DataValueField = "Code";
            RequestingBranch.DataBind();
            RequestingBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Branch", ""));
            RequestingBranch.SelectedIndex = -1;

            Department.DataSource = POSCache.GetDeparmentList();
            Department.DataBind();
            Department.Items.Insert(0, new ListItem("Select Department", ""));
            Department.SelectedIndex = -1;

            LocationState.DataSource = POSCache.GetState();
            LocationState.DataTextField = "Value";
            LocationState.DataValueField = "Key";
            LocationState.DataBind();
            LocationState.Items.Insert(0, new ListItem("Select State", ""));
            LocationState.SelectedIndex = -1;

            LastMonthTurnOverLabel.Text = string.Format("{0:MMMM} Turnover", DateTime.Now.AddMonths(-1));
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateRequest();
                string url = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\LAYOUTS\\POSManager.SharePoint\\POSDocumentation", 15);
                url = Path.Combine(url, string.Format("{0}-{2:mmssffff}{1}", Documentation.FileName.Split('.')[0], Path.GetExtension(Documentation.FileName), DateTime.Now));
                string documentation = Path.GetFileName(url);
                Documentation.PostedFile.SaveAs(url);
                string jaon = LocationJSON.Value;
                IManager imanager = new POSFlowManager(POSSettings, SharePointWeb);
                var posRequest = imanager.Add(AccountNumber.Text, PhoneNumber.Text, Email.Text, Username, string.Empty, RequestingBranch.SelectedValue, Department.SelectedValue, AccountOfficerPhone.Text, MerchantName.Text, BusinessClassification.Text, MerchantCategory.Text, DescriptionOfBusiness.Text, string.Format("{0} - {1}", FromTime.Value, ToTime.Value), IKEDCAccountNumber.Text, POSType.SelectedValue, documentation, JsonConvert.DeserializeObject<ICollection<Location>>(LocationJSON.Value));
                if (posRequest.Id > 0)
                {
                    IEmployee emp = (IEmployee)Assembly.GetAssembly(typeof(IEmployee)).CreateInstance(string.Format("{0}.{1}Employee", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                    emp.Settings = POSSettings;
                    string query = string.Format("select * from vw_employeeinfo where branch_code = '{0}' and (line_number = 'LL2' or line_number = 'LL1')", posRequest.RequestingBranch);
                    var employees = emp.GetEmployees(query);
                    var names = new List<string>();
                    foreach (var employee in employees)
                    {
                        names.Add(employee.Name);
                    }
                    Submitted.Value = JsonConvert.SerializeObject(names);
                }
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void ValidateRequest()
        {
            if (!Documentation.HasFile)
            {
                throw new ArgumentException("Document is compulsory, please attach document and submit request again");
            }
            if (Path.GetExtension(Documentation.FileName) != ".pdf" && Path.GetExtension(Documentation.FileName) != ".doc" && Path.GetExtension(Documentation.FileName) != ".docx")
            {
                throw new ArgumentException(string.Format("{0} is not a valid file type, Please upload valid file with .pdf, .doc or .docx extention", Documentation.FileName));
            }
            Account internalAccount = IAccount.GetInternalAccountDetails(AccountNumber.Text);
            if (internalAccount != null)
            {
                if (internalAccount.Status != "Active")
                {
                    throw new ArgumentException("Customer Internal Account is not Active, please activate internal account.");
                }
            }
        }

        [WebMethod]
        public static Customer GetCustomer(string accountNumber)
        {
            ICustomer iCustomer = (ICustomer)Assembly.GetAssembly(typeof(ICustomer)).CreateInstance(string.Format("{0}.{1}Customer", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            iCustomer.Settings = POSSettings;
            Customer customer = iCustomer.GetCustomer(accountNumber);
            if (customer != null)
            {
                Account internalAccount = IAccount.GetInternalAccountDetails(accountNumber);
                if (internalAccount != null)
                {
                    customer.InternalAccount = internalAccount.AccountNumber;
                    customer.InternalAccountStatus = internalAccount.Status;
                }
                customer.LastMonthTurnOver = string.Format("{0:n}", IAccount.GetTurnOver(accountNumber));
                customer.RelatedAccounts = JsonConvert.SerializeObject(IAccount.GetRelatedAccounts(accountNumber));
            }
            return customer;
        }

        [WebMethod]
        public static List<string> GetLocalGoverment(string stateCode)
        {
            List<string> lgas = POSCache.GetLGA(stateCode);
            return lgas;
        }

        protected void AddToList_Click(object sender, EventArgs e)
        {

        }
    }
}
