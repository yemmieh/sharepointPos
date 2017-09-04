using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Logic;
using System.Web.Services;
using System.Reflection;
using POSManager.Domain.Models;

namespace POSManager.SharePoint.Layouts.POSManager.SharePoint
{
    public partial class AwaitingApproval : POSManagerPageBase
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

        [WebMethod]
        public static List<string> GetApproval(int requestId)
        {
            IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
            var posRequest = iManager.Get(requestId);
            var names = new List<string>();
            if (posRequest != null)
            {
                if (posRequest.Status == Domain.Models.Status.AWAITING_APPROVAL)
                {
                    if (posRequest.Pending == Domain.Models.RequestStage.BRANCH_OR_HEAD_OFFICE(posRequest.RequestingBranch))
                    {
                        IEmployee emp = (IEmployee)Assembly.GetAssembly(typeof(IEmployee)).CreateInstance(string.Format("{0}.{1}Employee", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                        emp.Settings = POSSettings;
                        string query = string.Format("select * from vw_employeeinfo where branch_code = '{0}' and (line_number = 'LL2' or line_number = 'LL1')", posRequest.RequestingBranch);
                        var employees = emp.GetEmployees(query);
                        foreach (var employee in employees)
                        {
                            names.Add(employee.Name);
                        }
                    }
                    else if (posRequest.Pending == Domain.Models.RequestStage.E_BUSINESS)
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
            }
            return names;
        }

        private void Bind()
        {
            IManager iManager = new POSFlowManager(POSSettings, SharePointWeb);
            POSRequests.DataSource = iManager.All.ToList();
            POSRequests.DataBind();
        }
    }
}
