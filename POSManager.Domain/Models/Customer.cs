using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public int RIMNO { get; set; }
        public string Sector { get; set; }
        public string AccountOpeningDate { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string RCNumber { get; set; }
        public string RSMName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress1 { get; set; }
        public string EmailAddress2 { get; set; }
        public string AccountType { get; set; }
        public string NatureOfBusiness { get; set; }
        public string LastMonthTurnOver { get; set; }
        public string InternalAccount { get; set; }
        public string InternalAccountStatus { get; set; }
        public string MerchantId { get; set; }
        public string GroupID { get; set; }
        public string RelatedAccounts { get; set; }

    }
}
