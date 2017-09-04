using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; }
        public string ClassCode { get; set; }
        public string CreatedDate { get; set; }
    }
}
