using Microsoft.SharePoint;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class POSFlowManager : IManager
    {
        private Settings _settings;
        private SPWeb _web;
        private POSDBContext _dbContext;
        private IAccount _iAccount;
        private IMerchant _iMerchant;

        public POSFlowManager(Settings settings, SPWeb web)
        {
            _settings = settings;
            _iAccount = (IAccount)Assembly.GetAssembly(typeof(IAccount)).CreateInstance(string.Format("{0}.{1}Account", Constants.Namespace, _settings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            _iMerchant = (IMerchant)Assembly.GetAssembly(typeof(IMerchant)).CreateInstance(string.Format("{0}.{1}Merchant", Constants.Namespace, _settings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            _iAccount.Settings = _iMerchant.Settings = settings;
            _web = web;
            _dbContext = new POSDBContext(settings.DBConnectionString);
        }

        public Models.POSRequest Add(string accountNumber, string phoneNumber, string email, string initiatedBy, string merchantId, string requestingBranch, string department, string accountOfficerPhoneNumber, string merchantTradeName, string merchantBusinessClasssification, string merchantCategory, string descriptionOfBusiness, string businessTime, string IKEDCAgentAccountNumber, string posType, string posFormURL, ICollection<Models.Location> locations)
        {
            POSRequest posRequest = new POSRequest();
            ICustomer iCustomer = (ICustomer)Assembly.GetAssembly(typeof(ICustomer)).CreateInstance(string.Format("{0}.{1}Customer", Constants.Namespace, _settings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            iCustomer.Settings = _settings;
            Customer customer = iCustomer.GetCustomer(accountNumber);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found");
            }
            if (string.IsNullOrEmpty(merchantId))
            {
                merchantId = _iMerchant.ExistingMerchant(accountNumber);
                if (string.IsNullOrEmpty(merchantId))
                {
                    merchantId = _iMerchant.ExistingMerchant(_iAccount.GetRelatedAccounts(accountNumber).Select(c => c.AccountNumber).ToList());
                }
            }
            posRequest.AccountName = customer.Name;
            posRequest.AccountNumber = accountNumber;
            posRequest.AccountOfficer = customer.RSMName;
            posRequest.AccountOfficerPhone = accountOfficerPhoneNumber;
            posRequest.AccountOpeningDate = customer.AccountOpeningDate;
            posRequest.AccountStatus = customer.Status;
            posRequest.BusinessTime = businessTime;
            posRequest.Department = department;
            posRequest.DescriptionOfBusiness = descriptionOfBusiness;
            posRequest.Email = email;
            posRequest.LastMonthTurnover = _iAccount.GetTurnOver(accountNumber);
            posRequest.IKEDCAgentAccountNumber = IKEDCAgentAccountNumber;
            posRequest.InitiatedBy = initiatedBy;
            posRequest.InitiatedOn = DateTime.Now;
            posRequest.MerchantBusinessClassification = merchantBusinessClasssification;
            posRequest.MerchantCatergory = merchantCategory;
            posRequest.MerchantTradeName = merchantTradeName;
            posRequest.MerchantId = merchantId;
            posRequest.PhoneNumber = phoneNumber;
            posRequest.POSFormURL = posFormURL;
            posRequest.POSType = posType;
            posRequest.RCNumber = customer.RCNumber;
            posRequest.RegisteredAddress = customer.Address;
            posRequest.RequestingBranch = requestingBranch;
            posRequest.RIMNO = customer.RIMNO;
            posRequest.Sector = customer.Sector;
            posRequest.Pending = RequestStage.BRANCH_OR_HEAD_OFFICE(requestingBranch);
            posRequest.Status = Status.AWAITING_APPROVAL;
            foreach (var location in locations)
            {
                posRequest.Locations.Add(location);
            }
            Account internalAccount = _iAccount.GetInternalAccountDetails(accountNumber);
            if (internalAccount != null)
            {
                if (internalAccount.Status != "Active")
                {
                    throw new ArgumentException("Please activate internal account, before proceeding.");
                }
                posRequest.InternalAccountNumber = internalAccount.AccountNumber;
                posRequest.InternalAccountStatus = internalAccount.Status;
            }
            posRequest.Histories.Add(new History
            {
                PerformedBy = initiatedBy,
                PerformedOn = DateTime.Now,
                Comment = "None",
                RequestStage = "Make Request",
                Action = "New Request"
            });
            _dbContext.POSRequest.Add(posRequest);
            Save();
            //TODO: Send mail notification to HOP and Branch Head that they should approve request
            return posRequest;
        }

        public IQueryable<Models.POSRequest> All
        {
            get { return _dbContext.POSRequest.Where(c => !c.Purged).OrderByDescending(d => d.Id); }
        }

        public Models.POSRequest Get(int requestId)
        {
            return _dbContext.POSRequest.Find(requestId);
        }

        public IQueryable<Models.POSRequest> Get(string accountNumber, string merchantId, string initiator, string branch, string department, string stage, string status, string customerName, string merchantName, Expression<Func<Models.POSRequest, bool>> filter = null)
        {
            var query = All;
            if (!string.IsNullOrEmpty(accountNumber))
            {
                query = query.Where(c => c.AccountNumber == accountNumber);
            }
            if (!string.IsNullOrEmpty(merchantId))
            {
                query = query.Where(c => c.MerchantId.ToUpper() == merchantId.ToUpper());
            }
            if (!string.IsNullOrEmpty(branch))
            {
                query = query.Where(c => c.RequestingBranch == branch);
            }
            if (!string.IsNullOrEmpty(initiator))
            {
                query = query.Where(c => c.InitiatedBy == initiator);
            }
            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(c => c.Department == department);
            }
            if (!string.IsNullOrEmpty(stage))
            {
                query = query.Where(c => c.Pending == stage);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(c => c.Status == status);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        public Models.POSRequest Approve(int requestId, string user, string comment)
        {
            POSRequest posRequest = Get(requestId);
            string requestStage = posRequest.Pending;
            posRequest.Pending = RequestStage.E_BUSINESS;
            posRequest.Histories.Add(new History
            {
                Action = "Approve",
                RequestStage = requestStage,
                PerformedBy = user,
                PerformedOn = DateTime.Now,
                Comment = comment
            });
            Save();
            return posRequest;
        }

        public Models.POSRequest EBusinessApproval(int requestId, string user, string communicationMode, string comment)
        {
            //Assign merchant and terminal id... create internal account if not available
            POSRequest posRequest = Get(requestId);
            string currentStage = posRequest.Pending;
            ValidateInternalAccount(posRequest);

            //if merchant is not available assign merchant
            if (string.IsNullOrEmpty(posRequest.MerchantId))
            {
                _iMerchant.AssignMerchant(posRequest, posRequest.InitiatedBy, GetBranchName(posRequest.RequestingBranch));
            }
            _iMerchant.AssignTerminals(posRequest, posRequest.InitiatedBy, GetBranchName(posRequest.RequestingBranch));

            posRequest.CommunicationMode = communicationMode;
            posRequest.Pending = RequestStage.POS_UNIT_CONFIGURATION;
            posRequest.Histories.Add(new History
            {
                Comment = comment,
                PerformedBy = user,
                PerformedOn = DateTime.Now,
                Action = "Approve",
                RequestStage = currentStage
            });
            Save();
            return posRequest;
        }

        private void ValidateInternalAccount(POSRequest posRequest)
        {
            Account internalAccount = _iAccount.GetInternalAccountDetails(posRequest.AccountNumber);
            if (internalAccount == null)
            {
                _iAccount.CreateInternalAccount(posRequest.AccountNumber, GetEmployee(posRequest.InitiatedBy).StaffNo, posRequest.RIMNO);
                internalAccount = _iAccount.GetInternalAccountDetails(posRequest.AccountNumber);
                if (internalAccount == null)
                {
                    throw new ArgumentException("Internal account was not created successfully, please try again.");
                }
                posRequest.InternalAccountNumber = internalAccount.AccountNumber;
                posRequest.InternalAccountStatus = internalAccount.Status;
            }
            else if (internalAccount.Status != "Active")
            {
                throw new ArgumentException("Please activate internal account, before proceeding.");
            }
        }

        public Models.POSRequest POSUnitConfiguration(int requestId, string user, List<Models.Location> locations, string ptsp, string isoType, string keyType, string comment)
        {
            POSRequest posRequest = Get(requestId);
            string currentStage = posRequest.Pending;
            ValidateInternalAccount(posRequest);
            posRequest.PTSP = ptsp;
            posRequest.KeyType = keyType;
            posRequest.ISOType = isoType;
            foreach (var location in locations)
            {
                var newLocation = locations.FirstOrDefault(c => c.Id == location.Id);
                foreach (var terminal in location.Terminals)
                {
                    //attaching the terminal to the entity and set the state as modified
                    _dbContext.Entry(terminal).State = System.Data.Entity.EntityState.Modified;

                    if (terminal.Deployed && !terminal.DeployedOn.HasValue)
                    {
                        _iMerchant.CreateTerminalInformation(terminal, newLocation, GetUser(user).Name, GetBranchName(posRequest.RequestingBranch));
                    }
                }
                _iMerchant.InsertNotification(posRequest.MerchantId, location.ViewerUsername, location.SettlementAccountNumber, posRequest.MerchantTradeName, location.ViewerEmail);
            }
            Location loca = locations.FirstOrDefault();
            _iMerchant.CreateMerchantInformation(posRequest.MerchantId, posRequest.MerchantTradeName, string.Format("{0} {1} {2}", loca.Address, loca.City, loca.State), loca.ContactName, loca.Phone, posRequest.MerchantBusinessClassification, GetBranchName(posRequest.RequestingBranch), GetUser(user).Name, 1);
            
            if (locations.All(c => c.Terminals.All(d => d.DeployedOn.HasValue && d.Deployed)))
            {
                posRequest.Pending = RequestStage.COMPLETED;
                posRequest.Status = Status.APPROVED_DEPLOYED;
            }
            else
                posRequest.Pending = RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD;
            posRequest.Histories.Add(new History
            {
                RequestStage = currentStage,
                Action = "Approve",
                Comment = comment,
                PerformedBy = user,
                PerformedOn = DateTime.Now
            });

            Save();
            //TODO: Send email Notification to Inititator if completed
            return posRequest;
        }

        public Models.POSRequest POSUnitInterswitchOrVerveCard(int requestId, string user, List<Models.Location> locations, string comment)
        {
            POSRequest posRequest = Get(requestId);
            string currentStage = posRequest.Pending;
            ValidateInternalAccount(posRequest);
            foreach (var location in locations)
            {
                var newLocation = locations.FirstOrDefault(c => c.Id == location.Id);
                foreach (var terminal in location.Terminals)
                {
                    //attaching the terminal to the entity and set the state as modified
                    _dbContext.Entry(terminal).State = System.Data.Entity.EntityState.Modified;

                    if (terminal.Deployed && !terminal.DeployedOn.HasValue)
                    {
                        _iMerchant.CreateTerminalInformation(terminal, newLocation, GetUser(user).Name, GetBranchName(posRequest.RequestingBranch));
                    }
                }
            }

            if (locations.All(c => c.Terminals.All(d => d.DeployedOn.HasValue && d.Deployed)))
            {
                posRequest.Pending = RequestStage.COMPLETED;
                posRequest.Status = Status.APPROVED_DEPLOYED;
            }
            else
                posRequest.Pending = RequestStage.POS_UNIT_INTERSWITCH_VALUE_CARD;

            posRequest.Histories.Add(new History
            {
                RequestStage = currentStage,
                Action = "Approve",
                Comment = comment,
                PerformedBy = user,
                PerformedOn = DateTime.Now
            });
            Save();
            return posRequest;
        }

        public Models.POSRequest Decline(int requestId, string user, string comment)
        {
            POSRequest posRequest = Get(requestId);
            string currentStage = posRequest.Pending;
            posRequest.Pending = RequestStage.COMPLETED;
            posRequest.Status = Status.DECLINED;
            posRequest.Histories.Add(new History
            {
                RequestStage = currentStage,
                Action = "Decline",
                Comment = comment,
                PerformedBy = user,
                PerformedOn = DateTime.Now
            });
            Save();
            return posRequest;
        }

        public void ReRoute(int requestId, string user, string newStage, string comment)
        {
            throw new NotImplementedException();
        }

        public void BullkReRoute(List<int> requestIds, string user, string comment)
        {
            throw new NotImplementedException();
        }

        public void PurgeRequest(int requestId, string user, string comment)
        {
            throw new NotImplementedException();
        }

        private Employee GetEmployee(string username)
        {
            IEmployee emp = (IEmployee)Assembly.GetAssembly(typeof(IEmployee)).CreateInstance(string.Format("{0}.{1}Employee", Constants.Namespace, _settings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
            emp.Settings = _settings;
            return emp.GetEmployee(username);
        }

        public bool Save()
        {
            if (_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        private SPUser GetUser(string logonName)
        {
            if (_settings.AuthenticationMode == "Forms")
            {
                return _web.EnsureUser(logonName.Split('\\')[1]);
            }
            return _web.EnsureUser(logonName);
        }

        private string GetBranchName(string code)
        {
            return new POSCache(_web).GetBranchList().FirstOrDefault(c => c.Code == code).Name;
        }

        public MerchantUpdate SubmitMerchantUpdate(string user, string updateType, string comment, string requestingBranch, string department, string merchantId, string merchantName, string currentSettlementAcct, string newSettlementAccount, string accountName, string currentTerminals, string reAssignedTerminals, string currentEmailAddress, string newEmailAddress, string currentUsername, string newUsername, string newEmail, string newMerchantId, bool addToExisting, bool newMID, string customerRequest)
        {
            MerchantUpdate merchantUpdate = new MerchantUpdate();
            merchantUpdate.MerchantId = merchantId;
            merchantUpdate.MerchantName = merchantName;
            merchantUpdate.RequestingBranch = requestingBranch;
            merchantUpdate.Department = department;
            if (updateType == MerchantUpdateType.SETTLEMENT)
            {
                var account = _iAccount.GetAccountDetails(newSettlementAccount);
                if (account == null)
                {
                    throw new ArgumentException("Please provide a valid internal account");
                }
                merchantUpdate.UpdateType = MerchantUpdateType.SETTLEMENT;
                merchantUpdate.NewSettlementAccount = newSettlementAccount;
                merchantUpdate.CurrentSettlementAccount = currentSettlementAcct;
                merchantUpdate.AccountName = account.AccountName;
            }
            else if (updateType == MerchantUpdateType.NOTIFICATION)
            {
                merchantUpdate.UpdateType = MerchantUpdateType.NOTIFICATION;
                merchantUpdate.CurrentEmailAddress = currentEmailAddress;
                merchantUpdate.NewEmailAddress = newEmailAddress;
                merchantUpdate.AddToExisting = addToExisting;
            }
            else if (updateType == MerchantUpdateType.REPORT_VIEWER)
            {
                merchantUpdate.UpdateType = MerchantUpdateType.REPORT_VIEWER;
                merchantUpdate.CurrentUsername = currentUsername;
                merchantUpdate.NewUsername = newUsername;
                merchantUpdate.NewEmail = newEmail;
                merchantUpdate.CurrentSettlementAccount = currentSettlementAcct;
                
            }
            else if (updateType == MerchantUpdateType.TERMINAL)
            {
                merchantUpdate.UpdateType = MerchantUpdateType.TERMINAL;
                merchantUpdate.NewMerchantId = newMerchantId;
                merchantUpdate.CurrentTerminals = currentTerminals;
                merchantUpdate.ReAssignedTerminals = reAssignedTerminals;
                merchantUpdate.NewSettlementAccount = newSettlementAccount;
                merchantUpdate.CurrentSettlementAccount = currentSettlementAcct;
                merchantUpdate.AddToExisting = newMID;
                merchantUpdate.AccountName = accountName;
            }
            merchantUpdate.Stage = RequestStage.BRANCH_OR_HEAD_OFFICE(requestingBranch);
            merchantUpdate.RequestedBy = user;
            merchantUpdate.RequestedOn = DateTime.Now;
            merchantUpdate.Status = Status.AWAITING_APPROVAL;
            merchantUpdate.Comment = comment;
            merchantUpdate.MerchantUpdateHistories.Add(new MerchantUpdateHistory
            {
                PerformedBy = user,
                PerformedOn = DateTime.Now,
                Comment = comment,
                Action = "Initiate Request",
                RequestStage = "Update Request"
            });

            _dbContext.MerchantUpdate.Add(merchantUpdate);
            Save();
            return merchantUpdate;
        }

        public MerchantUpdate ApproveMerchantUpdate(int requestId, string user, string updateType, string comment)
        {
            var update = GetUpdates().FirstOrDefault(c => c.Id == requestId);

            string currentStage = update.Stage;
            if (update.Status == Status.AWAITING_APPROVAL)
            {
                if (currentStage == RequestStage.BRANCH_OR_HEAD_OFFICE(update.RequestingBranch))
                {
                    if (update.UpdateType == MerchantUpdateType.NOTIFICATION || update.UpdateType == MerchantUpdateType.REPORT_VIEWER)
                    {
                        DoMerchantUpdate(update, user);
                    }
                    else if (update.UpdateType == MerchantUpdateType.SETTLEMENT)
                    {   
                        update.Stage = RequestStage.NIBSS;
                    }
                    else if (update.UpdateType == MerchantUpdateType.TERMINAL)
                    {
                        update.Stage = RequestStage.POS_UNIT_CONFIGURATION;
                    }
                }
                else
                {
                    DoMerchantUpdate(update, user);
                }
            }
            update.MerchantUpdateHistories.Add(new MerchantUpdateHistory
            {
                Action = "Approved",
                Comment = comment,
                PerformedBy = user,
                PerformedOn = DateTime.Now,
                RequestStage = currentStage
            });
            Save();
            return update;

        }

        private MerchantUpdate DoMerchantUpdate(MerchantUpdate update, string user)
        {
            switch (update.UpdateType)
            {
                case MerchantUpdateType.NOTIFICATION:
                    _iMerchant.UpdateNotification(update, user);
                    break;
                case MerchantUpdateType.REPORT_VIEWER:
                    _iMerchant.UpdateReportView(update, user);
                    break;
                case MerchantUpdateType.SETTLEMENT:
                    _iMerchant.UpdateSettlementAccount(update, user);
                    break;
                case MerchantUpdateType.TERMINAL:
                    _iMerchant.ReassignTerminal(update, user, GetBranchName(update.RequestingBranch));
                    break;
                default:
                    break;
            }
            update.Stage = RequestStage.COMPLETED;
            update.Status = Status.APPROVED;
            return update;
        }


        public IQueryable<MerchantUpdate> GetUpdates()
        {
            return _dbContext.MerchantUpdate.OrderByDescending(c => c.Id).AsQueryable();
        }
    }
}
