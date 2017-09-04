using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestCustomer : ICustomer
    {
        private Settings _settings;
        public Models.Customer GetCustomer(string accountNumber)
        {
            return new Customer
            {
                AccountType = "Current",
                EmailAddress1 = "e@t.v",
                EmailAddress2 = "e@f.c",
                MobileNumber = "3498495",
                Name = "Ibraheem Bello",
                NatureOfBusiness = "Software Development",
                Address = "Ikeja",
                RCNumber = "02987498",
                RIMNO = 1,
                RSMName = "Dolapo",
                Sector = "IT",
                Status = "Active",
                PhoneNumber = "8473098770"
            };
        }

        public Settings Settings
        {
            set { this._settings = value; }
        }
    }
}
