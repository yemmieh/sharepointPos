using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint.Administration.Claims;

namespace POSManager.SharePoint
{
    public class POSManagerPageBase : LayoutsPageBase
    {


        protected static Settings POSSettings
        {
            get
            {
                return POSCache.POSSettings;
            }
        }

        protected int? POSRequestID
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["posRequestId"]))
                {
                    int leaveId;
                    bool confirm = int.TryParse(HttpContext.Current.Request.QueryString["posRequestId"], out leaveId);
                    if (confirm)
                    {
                        return leaveId;
                    }
                }
                return null;
            }
        }
        protected int? UpdateID
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["updateId"]))
                {
                    int leaveId;
                    bool confirm = int.TryParse(HttpContext.Current.Request.QueryString["updateId"], out leaveId);
                    if (confirm)
                    {
                        return leaveId;
                    }
                }
                return null;
            }
        }
        protected static SPWeb SharePointWeb
        {
            get
            {
                var web = SPContext.Current.Web;
                web.AllowUnsafeUpdates = true;
                return web;
            }
        }

        protected static POSCache POSCache
        {
            get
            {
                return new POSCache(SharePointWeb);
            }
        }

        protected string Username
        {
            get
            {
                if (POSSettings.AuthenticationMode == "Forms")
                {
                    return string.Format("Africa\\{0}", HttpContext.Current.User.Identity.Name.Split('|')[2]);
                }
                return HttpContext.Current.User.Identity.Name.Split('|')[1];
            }
        }

        protected SPUser GetUser(string logonName)
        {
            if (POSSettings.AuthenticationMode == "Forms")
            {
                return SharePointWeb.EnsureUser(logonName.Split('\\')[1]);
            }
            return SharePointWeb.EnsureUser(logonName);
        }


        protected static IAccount IAccount
        {
            get
            {
                Domain.Logic.Abstract.IAccount iAccount = (Domain.Logic.Abstract.IAccount)Assembly.GetAssembly(typeof(IAccount)).CreateInstance(string.Format("{0}.{1}Account", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                iAccount.Settings = POSSettings;
                return iAccount;
            }
        }

        protected static IMerchant IMerchant
        {
            get
            {
                Domain.Logic.Abstract.IMerchant iMerchant =  (Domain.Logic.Abstract.IMerchant)Assembly.GetAssembly(typeof(IMerchant)).CreateInstance(string.Format("{0}.{1}Merchant", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                iMerchant.Settings = POSSettings;
                return iMerchant;
            }
        }

        protected static IListing IListing
        {
            get
            {
                return (Domain.Logic.Abstract.IListing)Assembly.GetAssembly(typeof(IListing)).CreateInstance(string.Format("{0}.{1}Listing", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            }
        }

        protected Employee GetEmployee(string username)
        {
            IEmployee emp = (IEmployee)Assembly.GetAssembly(typeof(IEmployee)).CreateInstance(string.Format("{0}.{1}Employee", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            emp.Settings = POSSettings;
            return emp.GetEmployee(username);
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
                status.Attributes.Add("style", "display: block");
                status.InnerHtml = message;
            }
        }

        protected bool InGroup(string group)
        {
            SPUserCollection spUsers = POSCache.GetUsersInGroup(group);
            SPClaimProviderManager mgr = SPClaimProviderManager.Local;
            //TODO: Please clean up all this authentication shit.... Please if you reading the code below... abeg no mind me.. just wanted it to work
            //This just feels so wrong
            foreach (SPUser item in spUsers)
            {
                if (item.LoginName.Contains("|"))
                {
                    if (item.LoginName.Count(c => c == '|') == 2)
                    {
                        if (item.LoginName.Split('|')[2].ToUpper() == Username.Split('\\')[1].ToUpper())
                        {
                            return true;
                        }
                    }
                    else if (item.LoginName.Count(c => c == '|') == 1)
                    {
                        if (item.LoginName.Split('|')[1].ToUpper() == Username.ToUpper())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }



        protected string GetBranchName(string code)
        {
            return POSCache.GetBranchList().FirstOrDefault(c => c.Code == code).Name;
        }

        protected void Redirect(string page)
        {
            SPUtility.Redirect(string.Format("POSManager.Sharepoint/{0}", page), SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current);
        }

        protected string DetailsLink(string pageName, string queryString)
        {
            return string.Format("{0}?{1}", pageName, queryString);
        }
    }
}
