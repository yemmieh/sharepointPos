using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using POSManager.Domain.Models;
using Microsoft.SharePoint.IdentityModel;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class Login : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateData();
                bool result = SPClaimsUtility.AuthenticateFormsUser(Context.Request.UrlReferrer, UserName.Text, Password.Text);

                if (!result)
                {
                    throw new ArgumentException("Username or password incorrect, please try again later");
                }
                Response.Redirect("/sites/POSManager/_layouts/15/POSManager.SharePoint/MyRequest.aspx");
            }
            catch (Exception ex)
            {
                Show(ex, msg);
            }
        }

        private void ValidateData()
        {
            if (string.IsNullOrEmpty(UserName.Text))
            {
                throw new ArgumentException("Please provide username");
            }
            if (string.IsNullOrEmpty(Password.Text))
            {
                throw new ArgumentException("Please provide password");
            }
        }

        protected void Show(Exception ex, HtmlGenericControl status)
        {
            Show(ex.Message, MessageClass.Error, status);
        }

        protected void Show(string message, string messageClass, HtmlGenericControl status)
        {
            if (status != null)
            {
                status.Visible = true;
                status.Attributes.Add("class", messageClass);
                status.InnerHtml = message;
            }
        }
    }
}
