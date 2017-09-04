using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class TerminalDetails
    {
        public string MerchantName { get; set; }
        public string SettlementAccount { get; set; }
        public string Email { get; set; }
        public string TerminalId { get; set; }
        public string UserId { get; set; }
    }
}
