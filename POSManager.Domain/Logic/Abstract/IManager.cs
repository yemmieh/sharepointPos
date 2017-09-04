using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IManager
    {
        POSRequest Add(string accountNumber, string phoneNumber, string email, string initiatedBy, string merchantId, string requestingBranch, string department, string accountOfficerPhoneNumber, string merchantTradeName, string merchantBusinessClasssification, string merchantCategory, string descriptionOfBusiness, string businessTime, string IKEDCAgentAccountNumber, string posType, string posFormURL, ICollection<Location> locations);
        POSRequest Approve(int requestId, string user, string comment);
        IQueryable<POSRequest> All { get; }
        POSRequest Get(int requestId);
        IQueryable<POSRequest> Get(string accountNumber, string merchantId, string initiator, string branch, string department, string stage, string status, string customerName, string merchantName, Expression<Func<POSRequest, bool>> filter = null);
        /// <summary>
        /// Create internal account if customer doesn't have.... assign terminal ID and merchant ID... approve and move to POS Unit Configuratino Stage
        /// </summary>
        /// <param name="requestId">POS request ID</param>
        /// <param name="user">Username of user performion action</param>
        /// <param name="communicationMode">Communication mode selected by E-Business</param>
        /// <param name="comment">Optiona comment from user performing the action</param>
        /// <returns></returns>
        POSRequest EBusinessApproval(int requestId, string user, string communicationMode, string comment);
        /// <summary>
        /// Check if internal account is active... if not throw exception... call store procedure to create merchant information...
        /// call store procedure to insert notification information
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="user"></param>
        /// <param name="comment"></param>
        /// <param name="locations"></param>
        /// <returns></returns>
        POSRequest POSUnitConfiguration(int requestId, string user, List<Location> locations, string ptsp, string isoType, string keyType, string comment);
        /// <summary>
        /// Check for internal account again(Well this logic doesn't make any sense to me... because i expect it shouldnt even get here without internal account. OPEL!!!)
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="user"></param>
        /// <param name="comment"></param>
        /// <param name="terminalDeployed"></param> 
        /// <returns></returns>
        POSRequest POSUnitInterswitchOrVerveCard(int requestId, string user, List<Location> locations, string comment);
        POSRequest Decline(int requestId, string user, string comment);
        void ReRoute(int requestId, string user, string newStage, string comment);
        MerchantUpdate SubmitMerchantUpdate(string user, string updateType, string comment, string requestingBranch, string department, string merchantId, string merchantName, string currentSettlementAcct, string newSettlementAccount, string accountName, string currentTerminals, string reAssignedTerminals, string currentEmailAddress, string newEmailAddress, string currentUsername, string newUsername, string newEmail, string newMerchantId, bool addToExisting, bool newMID, string customerRequest);
        MerchantUpdate ApproveMerchantUpdate(int requestId, string user, string updateType, string comment);
        IQueryable<MerchantUpdate> GetUpdates();

        void BullkReRoute(List<int> requestIds, string user, string comment);
        /// <summary>
        /// Flag request as removed
        /// </summary>
        /// <param name="requestId">POS request Id to be removed</param>
        /// <param name="user">user performing the action</param>
        void PurgeRequest(int requestId, string user, string comment);
        bool Save();
    }
}
