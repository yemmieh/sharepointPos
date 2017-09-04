using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;
using System.Reflection;
using POSManager.Domain.Models;
using System.Web.Services;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class UpdateDetails : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (UpdateID.HasValue)
                    {
                        LoadView();
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void LoadView()
        {
            IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);

            var update = iManager.GetUpdates().FirstOrDefault(c => c.Id == UpdateID.Value);
            AuthorizeUser(update);
            if (update != null)
            {
                string updateType = update.UpdateType;
                RequestingBranch.Text = GetBranchName(update.RequestingBranch);
                Department.Text = update.Department;
                var merchantDetails = IMerchant.GetLoadMerchantDetails(update.MerchantId);
                MerchantId.Text = update.MerchantId;
                MerchantName.Text = update.MerchantName;
                History.DataSource = update.MerchantUpdateHistories;
                History.DataBind();
                if (updateType == Domain.Models.MerchantUpdateType.SETTLEMENT)
                {
                    CurrentAccountNo.Text = update.CurrentSettlementAccount;
                    NewAccountNo.Text = update.NewSettlementAccount;
                    AccountName.Text = update.AccountName;
                    settlementAccountView.Visible = true;
                    notificationView.Visible = false;
                    reportViewerView.Visible = false;
                    terminalView.Visible = false;
                }
                else if (updateType == Domain.Models.MerchantUpdateType.TERMINAL)
                {
                    NewTerminals.Text = update.ReAssignedTerminals;
                    OldTerminals.Text = update.CurrentTerminals;
                    CurrentSettlementAccount.Text = update.CurrentSettlementAccount;
                    NewSettlementAccount.Text = update.NewSettlementAccount;
                    tAccountName.Text = update.AccountName;
                    ExistingMID.Text = update.NewMerchantId;
                    settlementAccountView.Visible = false;
                    notificationView.Visible = false;
                    reportViewerView.Visible = false;
                    terminalView.Visible = true;
                }
                else if (updateType == Domain.Models.MerchantUpdateType.REPORT_VIEWER)
                {
                    CurrentUsername.Text = update.CurrentUsername;
                    NewUsername.Text = update.NewUsername;
                    ReportOption.SelectedValue = update.AddToExisting.ToString();
                    NewViewerEmailAddress.Text = update.NewEmail;
                    settlementAccountView.Visible = false;
                    notificationView.Visible = false;
                    reportViewerView.Visible = true;
                    terminalView.Visible = false;
                }
                else if (updateType == Domain.Models.MerchantUpdateType.NOTIFICATION)
                {
                    CurrentEmailAddress.Text = update.CurrentEmailAddress;
                    NewEmailAddress.Text = update.NewEmailAddress;
                    NotificationOption.SelectedValue = update.AddToExisting.ToString();
                    settlementAccountView.Visible = false;
                    notificationView.Visible = true;
                    reportViewerView.Visible = false;
                    terminalView.Visible = false;
                }
            }
        }



       
        protected void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpdateID.HasValue)
                {
                    IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
                    iManager.ApproveMerchantUpdate(UpdateID.Value, Username, string.Empty, Comment.Text);
                    Redirect("MerchantUpdates.aspx");
                }
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        protected void Decline_Click(object sender, EventArgs e)
        {

        }

        private void AuthorizeUser(Domain.Models.MerchantUpdate pos)
        {
            if (pos.Status == Domain.Models.Status.AWAITING_APPROVAL)
            {

                if (pos.Stage == Domain.Models.RequestStage.BRANCH_OR_HEAD_OFFICE(pos.RequestingBranch))
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
                if (pos.Stage == Domain.Models.RequestStage.NIBSS)
                {
                    commentDisplay.Visible = InGroup(Domain.Models.POSGroup.E_BUSINESS);
                }
                if (pos.Stage == Domain.Models.RequestStage.POS_UNIT_CONFIGURATION)
                {
                    commentDisplay.Visible = InGroup(Domain.Models.POSGroup.POS_UNIT_CONFIGURATION);
                }
            }
        }

    }
}
