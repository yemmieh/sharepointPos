using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class ReportDashboard : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindDropDown();
                Bind();
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void BindDropDown()
        {
            SNRequestingBranch.DataSource = POSCache.GetBranchList();
            SNRequestingBranch.DataTextField = "Name";
            SNRequestingBranch.DataValueField = "Code";
            SNRequestingBranch.DataBind();
            SNRequestingBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Branch", ""));
            SNRequestingBranch.SelectedIndex = -1;


            SNDeparment.DataSource = POSCache.GetDeparmentList();
            SNDeparment.DataBind();
            SNDeparment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Department", ""));
            SNDeparment.SelectedIndex = -1;



        }

        private void Bind()
        {
            throw new NotImplementedException();
        }
    }
}
