using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Status
    {
        public const string AWAITING_APPROVAL = "Awaiting Approval";
        public const string APPROVED_DEPLOYED = "Approved, Deployed";
        public const string APPROVED_NOT_DEPLOYED = "Approved, Not Deployed";
        public const string DECLINED = "Declined";
        public const string APPROVED = "Approved";

        public static List<string> GetAll()
        {
            return new List<string> {  AWAITING_APPROVAL, APPROVED_DEPLOYED, APPROVED_NOT_DEPLOYED, DECLINED };
        }

    }
}
