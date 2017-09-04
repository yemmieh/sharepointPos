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
    public class ZenithCustomer : ICustomer
    {
        public Models.Customer GetCustomer(string accountNumber)
        {
            Customer customer = new Customer();
            if (string.IsNullOrEmpty(_settings.AccountConnectionString))
            {
                throw new ArgumentException("Settings cannot be found.");
            }

            using (AseConnection con = new AseConnection(_settings.AccountConnectionString))
            {
                string query = "select a.acct_no as acct_no , convert(varchar,c.rim_no) as rim_no" +
                                " , convert(varchar,c.create_dt,103) as create_dt" +
                                " , convert(varchar,b.sector)" +
                                " , c.last_name+' '+ c.first_name as title_1 " +
                                " , c.status as status" +
                                " , str_replace(isnull(b.office_Address,''),CHAR(13)+CHAR(10),' ') as office_Address" +
                                " , isnull(b.rc_number,'') as rc_number" +
                                " , isnull(b.email_address1,'') as email_address1" +
                                " , isnull(b.mobile_no,'') as mobile_no" +
                                " , isnull(b.nature_of_business,'') as nature_of_business" +
                                " , isnull((select sector from zib_kyc_sectors " +
                                " where convert(varchar(11),sector_id) = b.sector),'')  as sector" +
                                    " , convert(varchar,isnull((select group_id from zib_loans_group_acct where acct_no = a.acct_no),0) ) as group_id" +
                                    " , isnull((select name from phoenix..ad_gb_rsm where employee_id = c.rsm_id),'') as rsm_name" +
                                    " , isnull(b.email_address2,'') as email_address2" +
                                    " , isnull(b.email_address3,'') as email_address3" +
                                    " , isnull(b.email_address4,'') as email_address4" +
                                    " , isnull(b.telephone_no,'') as telephone_no " +
                                    " , a.acct_type as acct_type " + //" , isnull(d.status ,'') as  internalacctstatus " +
                                    " from " +
                                    " zib_kyc_cust_information b, " +
                                    " phoenix..rm_acct c, " +
                                    " phoenix..dp_acct a  " + //" phoenix..dp_display d " +
                                " where b.acct_no = a.acct_no " +
                                    " and a.acct_no = '" + accountNumber + "' " +
                                    " and a.rim_no=c.rim_no " +
                                    " and c.status='Active'" +
                                    " and a.class_code in (100,101,102,217,110,109,156,170,171,207,206,201,202,203,204,205,200)";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    customer.AccountOpeningDate = result["create_dt"].ToString().Trim();
                    customer.RIMNO = int.Parse(result["rim_no"].ToString().Trim());
                    customer.Sector = result["sector"].ToString().Trim();
                    customer.Name = result["title_1"].ToString().Trim();
                    customer.Status = result["status"].ToString().Trim();
                    customer.RCNumber = result["rc_number"].ToString().Trim();
                    customer.EmailAddress1 = result["email_address1"].ToString().Trim();
                    customer.RSMName = result["rsm_name"].ToString().Trim();
                    customer.MobileNumber = result["mobile_no"].ToString().Trim();
                    customer.Address = result["office_Address"].ToString().Trim();
                }
            }

            return customer;
        }

        private Settings _settings;

        public Settings Settings
        {
            set { _settings = value; }
        }
    }
}
