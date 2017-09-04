using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class MyRequest : POSManagerPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
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
            IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
            POSRequests.DataSource = iManager.All.Where(c => c.InitiatedBy == Username).ToList();
            POSRequests.DataBind();
        }
    }
}
