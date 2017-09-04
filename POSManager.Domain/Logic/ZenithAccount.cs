using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using Sybase.Data.AseClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    class ZenithAccount : IAccount
    {
        public decimal GetTurnOver(string accountNumber)
        {
            DateTime month = DateTime.Now.AddMonths(-1);
            decimal amount = 0.0M;
            string firstDay = string.Format("{0:yyyy-MM-dd}", new DateTime(month.Year, month.Month, 1));
            string lastDay = string.Format("{0:yyyy-MM-dd}", new DateTime(month.Year, month.Month, 1).AddMonths(1).AddDays(-1));
            string query = "select convert(varchar, cast(round(sum(amt),2) as money ) )as amount from phoenix..dp_history  where acct_no = '" + accountNumber + "' and create_dt >= '" + firstDay + "'  and create_dt <='" + lastDay + "'  and tran_code < 150 and reversal = 0 group by acct_no";
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    amount = Convert.ToDecimal(result["amount"]);
                }
            }
            return amount;
        }

        public List<Models.Account> GetRelatedAccounts(string accountNumber)
        {
            List<Account> accounts = new List<Account>();
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = "select d.acct_no, d.status, c.first_name, c.last_name from phoenix..dp_display d, phoenix..rm_acct c where d.rim_no = c.rim_no and d.rim_no in ( select rim_no from phoenix..dp_acct where acct_no='" + accountNumber + "' ) and d.class_code in (100,101,102,217,110,109,156,170,171,207,206,201,202,203,204,205,200) ";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    accounts.Add(new Account
                    {
                        AccountNumber = result["acct_no"].ToString().Trim(),
                        AccountName = string.Format("{0} {1}", result["first_name"].ToString().Trim(), result["last_name"].ToString().Trim()),
                        Status = result["status"].ToString().Trim()
                    });
                }
            }
            return accounts;
        }

        public Models.Account GetAccountDetails(string accountNumber)
        {
            Account account = null;
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = "select c.status , a.title_1, c.class_code, a.acct_no from phoenix..rm_acct c, phoenix..dp_acct a  where a.acct_no = '" + accountNumber + "' and a.rim_no=c.rim_no";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    account = new Account
                    {
                        AccountNumber = result["acct_no"].ToString().Trim(),
                        AccountName = result["title_1"].ToString().Trim(),
                        Status = result["status"].ToString().Trim(),
                        ClassCode = result["class_code"].ToString().Trim()
                    };
                }
            }
            return account;
        }

        public Models.Account GetInternalAccountDetails(string accountNumber)
        {
            Account account = null;
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = "SELECT top 1 d.acct_no, convert(varchar,d.create_dt) as create_dt, d.status, a.title_1 FROM phoenix..dp_acct a , phoenix..dp_display d  WHERE a.acct_no = '" + accountNumber + "'  and a.rim_no = d.rim_no and d.class_code=235";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    account = new Account
                    {
                        AccountNumber = result["acct_no"].ToString().Trim(),
                        AccountName = result["title_1"].ToString().Trim(),
                        Status = result["status"].ToString().Trim(),
                        CreatedDate = result["create_dt"].ToString().Trim()
                    };
                }
            }
            return account;
        }

        public Models.Account GetInternalAccountDetails(int rimNo)
        {
            Account account = null;
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = "SELECT TOP 1 c.acct_no , convert(varchar,c.create_dt) as create_dt, c.status FROM phoenix..dp_display c WHERE c.rim_no = " + rimNo + " and c.class_code = 235";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    account = new Account
                    {
                        AccountNumber = result["acct_no"].ToString().Trim(),
                        Status = result["status"].ToString().Trim(),
                        CreatedDate = result["create_dt"].ToString().Trim()
                    };
                }
            }
            return account;
        }

        public void CreateInternalAccount(string accountNumber, string staffNumber, int rimNumber)
        {
            Account account = new Account();
            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = string.Format("zenbase..zsp_acct_creation_others '{0}', '{1}', 235, ''", accountNumber, staffNumber);
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private Settings _settings;

        public Settings Settings
        {
            set { _settings = value; }
        }
    }
}
