using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class POS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected SPWeb SharePointWeb
        {
            get
            {
                var web = SPContext.Current.Web;
                web.AllowUnsafeUpdates = true;
                return web;
            }
        }
    }
}
