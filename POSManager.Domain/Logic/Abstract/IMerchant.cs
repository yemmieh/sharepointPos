using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IMerchant: ISettings
    {
        string ExistingMerchant(string accountNumber);
        string ExistingMerchant(List<string> relateAccounts);
        TerminalDetails GetLoadMerchantDetails(string merchantId);
        /// <summary>
        /// check if any of relate account already has a merchant Id, if so update with new information or else create new entry
        /// </summary>
        /// <param name="posRequest">POS Request to assign merchant id to</param>
        /// <param name="assignedBy">The user assigning the request its merchantID</param>
        /// <returns></returns>
        string AssignMerchant(POSRequest posRequest, string assignedBy, string branch);
        /// <summary>
        /// Get unassigned terminal and update terminal Id as assigned
        /// </summary>
        /// <param name="posRequest"></param>
        /// <param name="assignedBy"></param>
        /// <returns></returns>
        List<string> AssignTerminals(POSRequest posRequest, string assignedBy, string branch);
        KeyValuePair<int, string> CreateMerchantInformation(string merchantId, string businessName, string address, string contactName, string contactPhone, string industry, string branch, string approvedBy, int tran_code = 1);
        KeyValuePair<int, string> CreateTerminalInformation(Terminal terminal, Location location, string approvedBy, string branch);
        void InsertNotification(string merchantId, string username, string account, string bizName, string email);
        void ReassignTerminal(MerchantUpdate update, string user, string branch);
        bool UpdateNotification(MerchantUpdate update, string user);
        void UpdateReportView(MerchantUpdate update, string user);
        bool UpdateSettlementAccount(MerchantUpdate update, string user);
        string GetUsername(string merchantId);

    }
}
