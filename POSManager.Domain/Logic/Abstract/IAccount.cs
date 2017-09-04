using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IAccount: ISettings
    {
        decimal GetTurnOver(string accountNumber);
        List<Account> GetRelatedAccounts(string accountNumber); //I dont know why i cant get this with rim
        /// <summary>
        /// Used to get account details
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Settlement Account Information</returns>
        Account GetAccountDetails(string accountNumber);
        /// <summary>
        /// Return a tuple of internal account details Name, Status, Class Code
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Account information</returns>
        Account GetInternalAccountDetails(string accountNumber); //Get internal accounts with account
        Account GetInternalAccountDetails(int rimNo); //overload method to get internal account details with rim no
        void CreateInternalAccount(string accountNumber, string staffNumber, int rimNumber); //TODO: Dont know what this guy will return for now
    }
}
