using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class CommunicationMode
    {
        public const string GRPS = "GPRS";
        public const string CDMA = "CDMA";
        public const string LAN = "LAN";

        public static List<string> GetAll()
        {
            return new List<string> { GRPS, CDMA, LAN };
        }
    }
}
