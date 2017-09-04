using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestMerchant : IMerchant
    {
        public string AssignMerchant(Models.POSRequest posRequest, string assignedBy, string branch)
        {
            posRequest.MerchantId = Guid.NewGuid().ToString().Replace("-", "").Substring(10).ToUpper();
            return posRequest.MerchantId;
        }

        public List<string> AssignTerminals(Models.POSRequest posRequest, string assignedBy, string branch)
        {
            List<string> terminls = new List<string>();
            foreach (var loc in posRequest.Locations)
            {
                for (int i = 0; i < loc.NoOfTerminal; i++)
                {
                    Terminal newTerminal = new Terminal();
                    string terminal = Guid.NewGuid().ToString().Replace("-", "").Substring(10).ToUpper();
                    newTerminal.TerminalId = terminal;
                    newTerminal.Switch = "INTERSWITCH";
                    newTerminal.SerialNo = "NA";
                    newTerminal.SIM = "NA";
                    loc.Terminals.Add(newTerminal);
                    terminls.Add(terminal);
                }
            }
            return terminls;
        }

        public KeyValuePair<int, string> CreateMerchantInformation(string merchantId, string businessName, string address, string contactName, string contactPhone, string industry, string branch, string approvedBy, int tran_code)
        {
            return new KeyValuePair<int, string>(0, "");
        }

        public KeyValuePair<int, string> CreateTerminalInformation(Models.Terminal terminal, Location location, string approvedBy, string branch)
        {
            terminal.DeployedOn = DateTime.Now;
            return new KeyValuePair<int, string>(0, "");
        }

        public void InsertNotification(string merchantId, string username, string account, string bizName, string email)
        {
            
        }

        public Settings Settings
        {
            set { }
        }

        public string ExistingMerchant(string accountNumber)
        {
            return string.Empty;
        }


        public string ExistingMerchant(List<string> relateAccounts)
        {
            return string.Empty;
        }


        public TerminalDetails GetLoadMerchantDetails(string merchantId)
        {
            return new TerminalDetails
            {
                Email = "ay@zenithbank.com\nib@zenith.org",
                MerchantName = "AY",
                SettlementAccount = "8374387788",
                TerminalId = "<option>HGJHGTYT</option><option>4749749087</option><option>JDKJHKJH89</option><option>JHKHUUI</option>",
                UserId = "ay.bode\nmorufat.akanni"
            };
        }


        public string GetUsername(string merchantId)
        {
            return "ibrheem.kbelo\nmorufat.akkani";
        }


        public void ReassignTerminal(MerchantUpdate update, string user, string branch)
        {
            
        }

        public bool UpdateNotification(MerchantUpdate update, string user)
        {
            return true;
        }

        public void UpdateReportView(MerchantUpdate update, string user)
        {
            
        }

        public bool UpdateSettlementAccount(MerchantUpdate update, string user)
        {
            return true;
        }
    }
}
