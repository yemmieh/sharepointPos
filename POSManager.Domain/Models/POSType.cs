using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class POSType
    {
        public const string M_POS = "mPOS";
        public const string PAY_ATTITUDE = "Pay Attitude";
        public const string TRADITIONAL_POS = "Traditional POS";

        public static List<string> GetAll()
        {
            return new List<string> { M_POS, PAY_ATTITUDE, TRADITIONAL_POS };
        }
    }
}
