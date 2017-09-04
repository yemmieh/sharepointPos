using POSManager.Domain.Logic.Abstract;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class ZenithMailer : IMessagingService
    {
        public bool Send(string to, string subject, string body)
        {
            return SPUtility.SendEmail(SPContext.Current.Web, true, false, to, subject, body, true);
        }
    }
}
