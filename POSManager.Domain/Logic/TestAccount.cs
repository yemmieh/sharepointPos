using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    class TestAccount : IAccount
    {
        public decimal GetTurnOver(string accountNumber)
        {
            return 35005000.00M;
        }

        public List<Models.Account> GetRelatedAccounts(string accountNumber)
        {
            return new List<Account>{
                new Account{
                    AccountNumber = "27358776",
                    AccountName = "Ibraheem",
                    Status = "Active",
                    CreatedDate = new DateTime(2014,3,12).ToString(),
                },
                new Account{
                    AccountNumber = "847589245",
                    AccountName = "Kolawole",
                    Status = "Active",
                    CreatedDate = new DateTime(2014,5,12).ToString(),
                },
                new Account{
                    AccountNumber = "2735843476",
                    AccountName = "Dolapo",
                    Status = "Active",
                    CreatedDate = new DateTime(2014,3,12).ToString(),
                }
            };
        }

        public Models.Account GetAccountDetails(string accountNumber)
        {
            return new Account
            {
                AccountNumber = "27358776",
                AccountName = "Ibraheem",
                Status = "Active",
                CreatedDate = new DateTime(2014, 3, 12).ToString(),
            };
        }

        public Models.Account GetInternalAccountDetails(string accountNumber)
        {
            return new Account
            {
                AccountNumber = "27358776",
                AccountName = "Ibraheem",
                Status = "Active",
                CreatedDate = new DateTime(2014, 3, 12).ToString(),
            };
        }

        public Models.Account GetInternalAccountDetails(int rimNo)
        {
            return new Account
            {
                AccountNumber = "27358776",
                AccountName = "Ibraheem",
                Status = "Active",
                CreatedDate = new DateTime(2014, 3, 12).ToString(),
            };
        }

        public void CreateInternalAccount(string accountNumber, string staffNumber, int rimNumber)
        {
            
        }

        private Settings _settings;

        public Settings Settings
        {
            set { _settings = value; }
        }
    }
}
