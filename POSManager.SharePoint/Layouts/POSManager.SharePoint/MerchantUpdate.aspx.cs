using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.Services;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;
using Microsoft.SharePoint.Utilities;
using System.IO;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class MerchantUpdate : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    LoadView();
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
            RequestingBranch.DataSource = POSCache.GetBranchList();
            RequestingBranch.DataTextField = "Name";
            RequestingBranch.DataValueField = "Code";
            RequestingBranch.DataBind();
            RequestingBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Branch", ""));
            RequestingBranch.SelectedIndex = -1;

            Department.DataSource = POSCache.GetDeparmentList();
            Department.DataBind();
            Department.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Department", ""));
            Department.SelectedIndex = -1;
        }

        private void LoadView()
        {
            string updateType = Request.QueryString["updateType"];
            UpdateType.Value = updateType;
            if (updateType == Domain.Models.MerchantUpdateType.SETTLEMENT)
            {
                settlementAccountView.Visible = true;
                notificationView.Visible = false;
                reportViewerView.Visible = false;
                terminalView.Visible = false;
            }
            else if (updateType == Domain.Models.MerchantUpdateType.TERMINAL)
            {
                settlementAccountView.Visible = false;
                notificationView.Visible = false;
                reportViewerView.Visible = false;
                terminalView.Visible = true;
            }
            else if (updateType == Domain.Models.MerchantUpdateType.REPORT_VIEWER)
            {
                settlementAccountView.Visible = false;
                notificationView.Visible = false;
                reportViewerView.Visible = true;
                terminalView.Visible = false;
            }
            else if (updateType == Domain.Models.MerchantUpdateType.NOTIFICATION)
            {
                settlementAccountView.Visible = false;
                notificationView.Visible = true;
                reportViewerView.Visible = false;
                terminalView.Visible = false;
            }
        }

        [WebMethod]
        public static Domain.Models.TerminalDetails GetMerchant(string merchantId)
        {
            var iMerchant = IMerchant;
            var merchantUpdate = iMerchant.GetLoadMerchantDetails(merchantId);
            if (merchantUpdate != null)
            {
                //merchantUpdate.UserId = iMerchant.GetUsername(merchantId);
            }
            return merchantUpdate;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string updateType = Request.QueryString["updateType"];
                if (string.IsNullOrEmpty(updateType))
                {
                    throw new ArgumentException("");
                }
                if (!Domain.Models.MerchantUpdateType.GetAll().Any(c => c == updateType))
                {
                    throw new ArgumentException("");
                }
                if (!Documentation.HasFile)
                {
                    throw new ArgumentException("Document is compulsory, please attach document and submit request again");
                }
                if (Path.GetExtension(Documentation.FileName) != ".pdf" && Path.GetExtension(Documentation.FileName) != ".doc" && Path.GetExtension(Documentation.FileName) != ".docx")
                {
                    throw new ArgumentException(string.Format("{0} is not a valid file type, Please upload valid file with .pdf, .doc or .docx extention", Documentation.FileName));
                }
                IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
                var merchantDetails = IMerchant.GetLoadMerchantDetails(MerchantId.Text);
                string url = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\LAYOUTS\\POSManager.SharePoint\\POSDocumentation", 15);
                url = Path.Combine(url, string.Format("{0}-{2:mmssffff}{1}", Documentation.FileName.Split('.')[0], Path.GetExtension(Documentation.FileName), DateTime.Now));
                string documentation = Path.GetFileName(url);
                Documentation.PostedFile.SaveAs(url);
                if (merchantDetails == null)
                {
                    throw new ArgumentException("");
                }
                bool addToExisting = false;
                if (updateType == Domain.Models.MerchantUpdateType.NOTIFICATION)
                {
                    addToExisting = bool.Parse(NotificationOption.SelectedValue);
                    iManager.SubmitMerchantUpdate(Username, updateType, Comment.Text, RequestingBranch.SelectedValue, Department.SelectedValue, MerchantId.Text, merchantDetails.MerchantName, merchantDetails.SettlementAccount, NewSettlementAccount.Text, AccountName.Text, string.Empty, string.Empty, CurrentEmailAddress.Text, NewEmailAddress.Text, CurrentUsername.Text, NewUsername.Text, NewEmailAddress.Text, ExistingMID.Text, addToExisting, false, documentation);
                }
                else if (updateType == Domain.Models.MerchantUpdateType.REPORT_VIEWER)
                {
                    addToExisting = bool.Parse(ReportOption.SelectedValue);
                    iManager.SubmitMerchantUpdate(Username, updateType, Comment.Text, RequestingBranch.SelectedValue, Department.SelectedValue, MerchantId.Text, merchantDetails.MerchantName, merchantDetails.SettlementAccount, NewSettlementAccount.Text, AccountName.Text, string.Empty, string.Empty, CurrentEmailAddress.Text, NewEmailAddress.Text, CurrentUsername.Text, NewUsername.Text, NewViewerEmailAddress.Text, ExistingMID.Text, addToExisting, false, documentation);
                }
                else if (updateType == Domain.Models.MerchantUpdateType.SETTLEMENT)
                {
                    iManager.SubmitMerchantUpdate(Username, updateType, Comment.Text, RequestingBranch.SelectedValue, Department.SelectedValue, MerchantId.Text, merchantDetails.MerchantName, merchantDetails.SettlementAccount, NewAccountNo.Text, AccountName.Text, string.Empty, string.Empty, CurrentEmailAddress.Text, NewEmailAddress.Text, CurrentUsername.Text, NewUsername.Text, NewEmailAddress.Text, ExistingMID.Text, addToExisting, false, documentation);
                }
                else if (updateType == Domain.Models.MerchantUpdateType.TERMINAL)
                {
                    
                    iManager.SubmitMerchantUpdate(Username, updateType, Comment.Text, RequestingBranch.SelectedValue, Department.SelectedValue, MerchantId.Text, merchantDetails.MerchantName, merchantDetails.SettlementAccount, NewSettlementAccount.Text, AccountName.Text, OldTerminals.Value, NewTerminals.Value, CurrentEmailAddress.Text, NewEmailAddress.Text, CurrentUsername.Text, NewUsername.Text, NewViewerEmailAddress.Text, ExistingMID.Text, addToExisting, bool.Parse(NewMID.SelectedValue), documentation);
                }
                Redirect("MerchantUpdates.aspx");
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }
    }
}
