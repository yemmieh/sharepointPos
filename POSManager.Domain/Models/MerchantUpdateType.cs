using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class MerchantUpdateType
    {
        public const string SETTLEMENT = "Settlement";
        public const string TERMINAL = "Terminal";
        public const string NOTIFICATION = "Notification";
        public const string REPORT_VIEWER = "ReportViewer";

        public static List<string> GetAll()
        {
            return new List<string> { SETTLEMENT, TERMINAL, NOTIFICATION, REPORT_VIEWER };
        }
    }
}
