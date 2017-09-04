using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;
using Newtonsoft.Json;
using System.IO;
using POSManager.Domain.Models;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class POSDetails : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void Bind()
        {
            if (POSRequestID.HasValue)
            {
                IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
                Domain.Models.POSRequest pos = iManager.Get(POSRequestID.Value);
                if (pos != null)
                {
                    CommunicationMode.DataSource = Domain.Models.CommunicationMode.GetAll();
                    CommunicationMode.DataBind();
                    AuthorizeView(pos);
                    AuthorizeUser(pos);
                    RequestingBranch.Text = GetBranchName(pos.RequestingBranch);
                    Department.Text = pos.Department;
                    AccountNumber.Text = pos.AccountNumber;
                    AccountName.Text = pos.AccountName;
                    AccountOpeningDate.Text = pos.AccountOpeningDate;
                    RCNumber.Text = pos.RCNumber;
                    RegisteredAddress.Text = pos.RegisteredAddress;
                    Sector.Text = pos.Sector;
                    LastMonthTurnover.Text = pos.LastMonthTurnover.ToString();
                    PhoneNumber.Text = pos.PhoneNumber;
                    Email.Text = pos.Email;
                    Documentation.NavigateUrl = Path.Combine("POSDocumentation", pos.POSFormURL);
                    Documentation.Text = pos.POSFormURL;
                    AccountOfficerPhone.Text = pos.AccountOfficerPhone;
                    AccountStatus.Text = pos.AccountStatus;
                    MerchantTradeName.Text = pos.MerchantTradeName;
                    MerchantBusinessClassification.Text = pos.MerchantBusinessClassification;
                    MerchantCategory.Text = pos.MerchantCatergory;
                    DescriptionOfBusiness.Text = pos.DescriptionOfBusiness;
                    BuinessHours.Text = pos.BusinessTime;
                    IKEDCAgentAccountNumber.Text = pos.IKEDCAgentAccountNumber;
                    POSType.Text = pos.POSType;
                    InternalAccountNumber.Text = pos.InternalAccountNumber;
                    InternalAccountStatus.Text = pos.InternalAccountStatus;
                    InternalAccountName.Text = pos.AccountName;
                    RequestedBy.Text = GetUser(pos.InitiatedBy).Name;
                    RequestStage.Text = pos.Status == Domain.Models.Status.AWAITING_APPROVAL ? string.Format("Pending with {0}", pos.Pending) : pos.Status;
                    if (!string.IsNullOrEmpty(pos.InternalAccountNumber))
                    {
                        var internalAccount = IAccount.GetAccountDetails(pos.InternalAccountNumber);
                        if (internalAccount != null)
                        {
                            InternalAccountCreatedOn.Text = internalAccount.CreatedDate;
                            InternalAccountName.Text = internalAccount.AccountName;
                        }
                    }
                    LastMonthTurnOverLabel.Text = string.Format("{0:MMMM} Turnover", pos.InitiatedOn.AddMonths(-1));
                    LocationJSON.Value = JsonConvert.SerializeObject(pos.Locations, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    TerminalModelJSON.Value = JsonConvert.SerializeObject(POSCache.GetPOSOption(Domain.Models.POSOption.TERMINAL_MODELS));
                    TerminalNetworkDeviceJSON.Value = JsonConvert.SerializeObject(POSCache.GetPOSOption(Domain.Models.POSOption.TERMINAL_NETWORK_DEVICE));
                    TerminalProvidersJSON.Value = JsonConvert.SerializeObject(POSCache.GetPOSOption(Domain.Models.POSOption.TERMINAL_PROVIDERS));
                    TerminalRoutesJSON.Value = JsonConvert.SerializeObject(POSCache.GetPOSOption(Domain.Models.POSOption.TERMINAL_ROUTES));
                    //Locations.DataSource = pos.Locations;
                    //Locations.DataBind();
                    History.DataSource = pos.Histories;
                    History.DataBind();
                }
            }
        }

        private void AuthorizeUser(Domain.Models.POSRequest pos)
        {
            if (pos.Status == Domain.Models.Status.AWAITING_APPROVAL)
            {

                if (pos.Pending == Domain.Models.RequestStage.BRANCH_OR_HEAD_OFFICE(pos.RequestingBranch))
                {
                    if (pos.RequestingBranch == "001")
                    {

                    }
                    else
                    {
                        var employee = GetEmployee(Username);
                        commentDisplay.Visible = employee.Branch == pos.RequestingBranch && (employee.Line == "LL1" || employee.Line == "LL2");
                    }
                }
                if (pos.Pending == Domain.Models.RequestStage.E_BUSINESS)
                {
                    commentDisplay.Visible = InGroup(Domain.Models.POSGroup.E_BUSINESS);
                }
                if (pos.Pending == Domain.Models.RequestStage.POS_UNIT_CONFIGURATION)
                {
                    commentDisplay.Visible = InGroup(Domain.Models.POSGroup.POS_UNIT_CONFIGURATION);
                }
                if (pos.Pending == Domain.Models.RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD)
                {
                    commentDisplay.Visible = InGroup(Domain.Models.POSGroup.POS_UNIT_INTERSWITCH_VALUE_CARD);
                }
            }
        }

        private void AuthorizeView(Domain.Models.POSRequest pos)
        {
            if (pos.Pending == Domain.Models.RequestStage.E_BUSINESS)
            {
                EBusinessSection.Visible = true;
            }
            if (pos.Pending == Domain.Models.RequestStage.POS_UNIT_CONFIGURATION)
            {
                CommunicationMode.SelectedValue = pos.CommunicationMode;
                CommunicationMode.Enabled = false;
                EBusinessSection.Visible = POSConfiguration.Visible = true;
            }
            if (pos.Pending == Domain.Models.RequestStage.COMPLETED || pos.Pending == Domain.Models.RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD)
            {
                CommunicationMode.SelectedValue = pos.CommunicationMode;
                CommunicationMode.Enabled = false;
                PTSP.SelectedValue = pos.PTSP;
                ISOType.SelectedValue = pos.ISOType;
                KeyType.SelectedValue = pos.KeyType;
                PTSP.Enabled = ISOType.Enabled = KeyType.Enabled = false;
                EBusinessSection.Visible = POSConfiguration.Visible = true;
            }
        }

        protected void Decline_Click(object sender, EventArgs e)
        {

        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                if (POSRequestID.HasValue)
                {
                    IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
                    Domain.Models.POSRequest posRequest = iManager.Get(POSRequestID.Value);
                    switch (posRequest.Pending)
                    {
                        case "Unit/Department Head":
                        case "HOP/Branch Head": posRequest = iManager.Approve(POSRequestID.Value, Username, Comment.Text); break;
                        case Domain.Models.RequestStage.E_BUSINESS: posRequest = iManager.EBusinessApproval(POSRequestID.Value, Username, CommunicationMode.SelectedValue, Comment.Text); break;
                        case Domain.Models.RequestStage.POS_UNIT_CONFIGURATION:
                            posRequest = POSUnitConfiguration(iManager, posRequest);
                            break;
                        case Domain.Models.RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD:
                            var locations = JsonConvert.DeserializeObject<List<Domain.Models.Location>>(LocationJSON.Value);
                            posRequest = iManager.POSUnitInterswitchOrVerveCard(POSRequestID.Value, Username, locations, Comment.Text); break;
                    }
                    ApprovalTitle.Value = posRequest.Status == Domain.Models.Status.AWAITING_APPROVAL ? string.Format("POS Request Submitted successfully. Pending with {0}, To be approved by any of the following \n\n", posRequest.Pending) : "POS Request Approved successfully. POS Deployed";
                    var names = new List<string>();
                    if (posRequest.Status == Domain.Models.Status.AWAITING_APPROVAL)
                    {
                        if (posRequest.Pending == Domain.Models.RequestStage.E_BUSINESS)
                        {
                            foreach (SPUser item in POSCache.GetUsersInGroup(POSGroup.E_BUSINESS))
                            {
                                names.Add(item.Name);
                            }
                        }
                        else if (posRequest.Pending == Domain.Models.RequestStage.POS_UNIT_CONFIGURATION)
                        {
                            foreach (SPUser item in POSCache.GetUsersInGroup(POSGroup.POS_UNIT_CONFIGURATION))
                            {
                                names.Add(item.Name);
                            }
                        }
                        else if (posRequest.Pending == Domain.Models.RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD)
                        {
                            foreach (SPUser item in POSCache.GetUsersInGroup(POSGroup.POS_UNIT_INTERSWITCH_VALUE_CARD))
                            {
                                names.Add(item.Name);
                            }
                        }
                    }
                    ApprovalJSON.Value = JsonConvert.SerializeObject(names);
                }

            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private Domain.Models.POSRequest POSUnitConfiguration(IManager iManager, Domain.Models.POSRequest posRequest)
        {
            if (string.IsNullOrEmpty(PTSP.SelectedValue) || string.IsNullOrEmpty(ISOType.SelectedValue) || string.IsNullOrEmpty(KeyType.SelectedValue))
            {
                throw new ArgumentException("Please select PTSP, ISOType and Key Type to proceed.");
            }
            var locations = JsonConvert.DeserializeObject<List<Domain.Models.Location>>(LocationJSON.Value);
            posRequest = iManager.POSUnitConfiguration(POSRequestID.Value, Username, locations, PTSP.SelectedValue, ISOType.SelectedValue, KeyType.SelectedValue, Comment.Text);
            return posRequest;
        }
    }
}
