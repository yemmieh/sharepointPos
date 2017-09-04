using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Settings
    {
        public string ApplicationMode { get; set; }
        public string DBConnectionString { get; set; }
        public string XDBConnectionString { get; set; }
        public string AccountConnectionString { get; set; }
        public string MerchantConnectionString { get; set; }
        public string KDBConnectionString { get; set; }
        public string NotificationConnectionString { get; set; }
        public string AuthenticationMode { get; set; }
    }

    public class SettingsKey
    {
        public Dictionary<string, string> GetKeys()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Application Mode", "ApplicationMode");
            dic.Add("Database Connection String", "DBConnectionString");
            dic.Add("X Database Connection String", "XDBConnectionString");
            dic.Add("Account Connection String", "AccountConnectionString");
            dic.Add("Merchant Connection String", "MerchantConnectionString");
            dic.Add("K Database Connection String", "KDBConnectionString");
            dic.Add("Notification Connection String", "NotificationConnectionString");
            dic.Add("Authentication Mode", "AuthenticationMode");
            return dic;
        }
    }
}
