using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using Sybase.Data.AseClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POSManager.Domain.Logic
{
    public class ZenithMerchant : IMerchant
    {
        public string AssignMerchant(Models.POSRequest posRequest, string assignedBy, string branch)
        {
            string merchantId = string.Empty;
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                Location location = posRequest.Locations.FirstOrDefault();
                string MID_STATE_FORMAT = "2057" + location.StateCode;
                string query = "SELECT TOP 1 Merchant_Id FROM infosys..zib_multicard_mid_sourcelist WHERE Status='Unassigned' AND Merchant_Id LIKE '" + MID_STATE_FORMAT + "%' ORDER BY Merchant_Id DESC";
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    merchantId = result["Merchant_Id"].ToString().Trim();
                    posRequest.MerchantId = merchantId.ToUpper();
                }
                string address = string.Format("{0} {1} {2}", location.Address, location.City, location.State);
                query = "UPDATE infosys..zib_multicard_mid_sourcelist SET Status='Assigned', Address='" + address.ToUpper() + "', Requesting_Branch='" + branch.ToUpper() + "', Reg_Date=GETDATE(), Merchant_Name='" + posRequest.MerchantTradeName.ToUpper() + "', Assigned_By='" + assignedBy.ToUpper() + "' WHERE Merchant_Id = '" + merchantId.ToUpper() + "'";
                cmd = new AseCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            return merchantId;
        }

        public List<string> AssignTerminals(Models.POSRequest posRequest, string assignedBy, string branch)
        {
            List<string> terminalIds = new List<string>();
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                con.Open();
                foreach (var location in posRequest.Locations)
                {
                    for (int i = 0; i < location.NoOfTerminal; i++)
                    {
                        Terminal newTerminal = new Terminal();
                        string query = "SELECT TOP 1 Terminal_Id FROM infosys..zib_multicard_tid_sourcelist WHERE Status='Unassigned' ORDER BY Terminal_Id DESC";
                        AseCommand cmd = new AseCommand(query, con);
                        var result = cmd.ExecuteReader();
                        string terminalId = string.Empty;
                        while (result.Read())
                        {
                            terminalId = result["Terminal_Id"].ToString().ToUpper().Trim();

                        }
                        string address = string.Format("{0} {1} {2}", location.Address, location.City, location.State);
                        string updateQuery = "UPDATE infosys..zib_multicard_tid_sourcelist SET Status='Assigned', Merchant_Id='" + posRequest.MerchantId + "', Acct_No='" + posRequest.AccountNumber + "', Settlement_Acct_No='" + location.SettlementAccountNumber + "', Address='" + address.ToUpper() + "', Requesting_Branch='" + branch.ToUpper() + "', Reg_Date=GETDATE() WHERE Terminal_Id = '" + terminalId.ToUpper() + "'";
                        AseCommand cmdUpdate = new AseCommand(updateQuery, con);
                        cmdUpdate.ExecuteNonQuery();
                        newTerminal.TerminalId = terminalId;
                        newTerminal.Switch = "INTERSWITCH";
                        newTerminal.SerialNo = "NA";
                        newTerminal.SIM = "NA";
                        terminalIds.Add(terminalId);
                        location.Terminals.Add(newTerminal);
                    }
                }
            }
            return terminalIds;
        }

        public KeyValuePair<int, string> CreateMerchantInformation(string merchantId, string businessName, string address, string contactName, string contactPhone, string industry, string branch, string approvedBy, int tran_code = 1)
        {
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                con.Open();
                string query = "Select Merchant_id from infosys..zib_multicard_pos_merchants where LTRIM(RTRIM(UPPER(Merchant_id))) = '" + merchantId.ToUpper() + "'";
                AseCommand queryCMD = new AseCommand(query, con);
                var result = queryCMD.ExecuteReader();
                while (result.Read())
                {
                    tran_code = 2;
                }
                string updateQuery = string.Format("infosys..zsp_multicard_pos_merchant '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 'Created by {7}', {8} , 0, ''", merchantId, businessName, address, contactName, contactPhone, industry, branch, approvedBy, tran_code);
                AseCommand cmd = new AseCommand(updateQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return new KeyValuePair<int, string>(0, "");
        }

        public KeyValuePair<int, string> CreateTerminalInformation(Models.Terminal terminal, Location location, string approvedBy, string branch)
        {
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                int tran_code = 1;
                con.Open();
                string query = "Select Terminal_id from infosys..zib_multicard_pos_terminals where LTRIM(RTRIM(UPPER(Terminal_id))) = '" + terminal.TerminalId.ToUpper() + "'";
                AseCommand queryCMD = new AseCommand(query, con);
                var result = queryCMD.ExecuteReader();
                while (result.Read())
                {
                    tran_code = 2;
                }

                string updateQuery = string.Format("infosys..zsp_multicard_pos_terminal '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', {20}, 0.00, 0, ''",
                    terminal.Id, terminal.TerminalId, location.POSRequest.InitiatedOn.ToString("yyyyMMdd"),
                    DateTime.Now.ToString("yyyyMMdd"), location.POSRequest.MerchantId, string.Format("{0} {1} {2}", location.Address, location.City, location.State).ToUpper(),
                    terminal.Route, terminal.Switch, "GPRS", terminal.SerialNo, terminal.SIM, location.POSRequest.ISOType, location.POSRequest.AccountNumber, location.POSRequest.AccountOpeningDate,
                    location.POSRequest.AccountOfficer, branch, "POS TERMINAL DEPLOYED", DateTime.Now.ToString("yyyyMMdd"), location.Email, location.SettlementAccountNumber, tran_code);

                AseCommand cmd = new AseCommand(updateQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
                terminal.DeployedOn = DateTime.Now;
            }
            return new KeyValuePair<int, string>(0, "");
        }

        public void InsertNotification(string merchantId, string username, string account, string bizName, string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_settings.NotificationConnectionString))
                {
                    string query = string.Format("Select UPPER(LTRIM(RTRIM(UserID))) AS UserID from tblMerchantDetails where UserID = '" + username + "'");
                    SqlCommand cmd = new SqlCommand(query, con);
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        throw new ArgumentException(string.Format("The User ID {0} already exists in the notification table. Please notify the Initiator or RSM.", username));
                    }
                    query = string.Format("dbo.Sproc_SubmitMerchantDetails('{0}','{1},'{2}','{3}','{4:yyyy-MM-dd}'", username, account, bizName, merchantId, DateTime.Now);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    query = string.Format("Select UPPER(LTRIM(RTRIM(UserID))) AS UserID from tblUser where RTRIM(LTRIM(LOWER(UserID))) = '" + username + "'");
                    cmd.CommandText = query;
                    var newResult = cmd.ExecuteReader();
                    if (newResult.Read())
                    {
                        throw new ArgumentException(string.Format("The User ID {0} already exists in the notification table. Please notify the Initiator or RSM.", username));
                    }
                    query = string.Format("dbo.Sproc_CreateUser('{0}','{1}','{2}','{3}','{4}','{5}','{6:yyyy-MM-dd}')", username, "Merchant", email, "password", "Y", "Active", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

            }
        }

        public string ExistingMerchant(string accountNumber)
        {
            string merchantId = string.Empty;
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                string query = string.Format("SELECT TOP 1 Settlement_Acct_No , Merchant_Id FROM infosys..zib_multicard_pos_terminals WHERE acct_no='" + accountNumber + "'");
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    merchantId = result["Merchant_Id"].ToString().Trim();
                }
            }
            return merchantId;
        }

        public string ExistingMerchant(List<string> relateAccounts)
        {
            string query = string.Empty;
            string relatedAccount = string.Empty;
            string merchantId = string.Empty;
            foreach (var acc in relateAccounts)
            {
                relatedAccount += string.Format(",'{0}'", acc);
            }
            relatedAccount = relatedAccount.Remove(0, 1);
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                query = string.Format("SELECT TOP 1 Merchant_Id FROM infosys..zib_multicard_pos_terminals WHERE Settlement_Acct_No in ({0})", relatedAccount);
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    merchantId = result["Merchant_Id"].ToString();
                }
            }
            return merchantId;
        }

        private Settings _settings;

        public Models.Settings Settings
        {
            set { _settings = value; }
        }

        public TerminalDetails GetLoadMerchantDetails(string merchantId)
        {
            string query = string.Empty;
            TerminalDetails terminalDetails = null;
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                query = string.Format("SELECT DISTINCT m.merchant_name AS Merchant_Name , t.Terminal_Id AS Terminal_Id, t.Settlement_Acct_No AS Settlement_Acct_No, isnull(t.Email_address,'') AS Email_Address FROM infosys..zib_multicard_pos_merchants m , infosys..zib_multicard_pos_terminals t WHERE LTRIM(RTRIM(UPPER(m.merchant_id))) = LTRIM(RTRIM(UPPER(t.merchant_id))) AND LTRIM(RTRIM(UPPER(m.merchant_id))) = '{0}' ORDER BY t.Terminal_Id", merchantId.ToUpper());
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    terminalDetails = new TerminalDetails();
                    terminalDetails.MerchantName = result["Merchant_Name"].ToString();
                    terminalDetails.SettlementAccount = result["Settlement_Acct_No"].ToString();
                    terminalDetails.Email += (result["Email_Address"].ToString() + "\n");
                    terminalDetails.TerminalId += string.Format("<option>{0}</option>", result["Terminal_Id"].ToString());
                }
            }
            return terminalDetails;
        }

        public string GetUsername(string merchantId)
        {
            string userId = string.Empty;
            using (SqlConnection con = new SqlConnection(_settings.NotificationConnectionString))
            {
                var query = string.Format("SELECT DISTINCT UserID AS UserID FROM tblMerchantDetails WHERE LTRIM(RTRIM(UPPER(MerchantID))) = '{0}'", merchantId);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    userId += string.Format("{0}\n", result["UserID"].ToString().Trim());
                }
            }
            return userId;
        }


        public void ReassignTerminal(MerchantUpdate update, string user, string branch)
        {
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                con.Open();
                AseCommand cmd = null;
                if (update.AddToExisting)
                {
                    string queryNewMID = string.Format("SELECT TOP 1 Merchant_Id " +
                                " FROM infosys..zib_multicard_mid_sourcelist " +
                                " WHERE Status='Unassigned'" +
                        //" AND Merchant_Id LIKE '"+MID_STATE_FORMAT+"%' " +
                                " AND Merchant_Id LIKE '2057LA%' " +
                                " ORDER BY Merchant_Id");
                    cmd = new AseCommand(queryNewMID, con);
                    var resultNewMID = cmd.ExecuteReader();
                    string newMid = string.Empty;
                    while (resultNewMID.Read())
                    {
                        newMid += resultNewMID["Merchant_Id"].ToString().Trim();
                    }
                    string mechantParameterOldMId = "SELECT TOP 1 m.Merchant_Name AS Merchant_Name" +
                                ", m.Address AS Address" +
                                ", m.Contact_Person AS Contact_Person" +
                                ", m.Telephone AS Telephone" +
                                ", m.Industry AS Industry" +
                                ", m.Branch AS Branch" +
                                " FROM infosys..zib_multicard_pos_merchants m " +
                                " WHERE m.Merchant_ID='" + update.MerchantId + "'";
                    cmd = new AseCommand(mechantParameterOldMId, con);
                    var resultOldMIdParameter = cmd.ExecuteReader();
                    string branchName = string.Empty;
                    string merchantName = string.Empty;
                    string address = string.Empty;
                    string contactPerson = string.Empty;
                    string telephone = string.Empty;
                    string industry = string.Empty;
                    while (resultOldMIdParameter.Read())
                    {
                        branchName = resultOldMIdParameter["Branch"].ToString().Trim();
                        merchantName = resultOldMIdParameter["Merchant_Name"].ToString().Trim();
                        address = resultOldMIdParameter["Address"].ToString().Trim();
                        contactPerson = resultOldMIdParameter["Contact_Person"].ToString();
                        telephone = resultOldMIdParameter["Telephone"].ToString();
                        industry = resultOldMIdParameter["Industry"].ToString();
                    }

                    string queryCreateNewMerchantInformation = string.Format("infosys..zsp_multicard_pos_merchant '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 'Created by {7}', {8} , 0, ''", newMid, merchantName, address, contactPerson, telephone, industry, branch, user, 1);
                    cmd = new AseCommand(queryCreateNewMerchantInformation, con);
                    var resultCreateNewMerchant = cmd.ExecuteNonQuery();
                    string queryUpdateMIDSourceList = "UPDATE infosys..zib_multicard_mid_sourcelist " +
                                " SET Status='Assigned' " +
                                    ", Address='" + address + "'" +
                                    ", Requesting_Branch='" + branch + "'" +
                                    ", Reg_Date = Reg_Date" +
                                    ", Merchant_Name='" + merchantName + "'" +
                                    ", Assigned_By='" + user + "'" +
                                " WHERE LTRIM(RTRIM(Merchant_Id)) = '" + newMid + "'";

                    cmd = new AseCommand(queryUpdateMIDSourceList, con);
                    var result = cmd.ExecuteNonQuery();
                    string tidSourceList = "";
                    string terminals = "";
                    foreach (var item in update.ReAssignedTerminals.Split('\n'))
                    {
                        tidSourceList += " UPDATE infosys..zib_multicard_tid_sourcelist " +
                                    " SET Merchant_Id='" + newMid + "' " +
                                    ", Settlement_Acct_No='" + update.NewSettlementAccount + "'" +
                                    ", Address='" + address + "'" +
                                    ", Requesting_Branch='" + branch + "'" +
                                    ", Reg_Date = Reg_Date" +
                                    " WHERE LTRIM(RTRIM(Terminal_Id)) = '" + item.ToUpper() + "' " +
                                    " ";
                        terminals += " UPDATE infosys..zib_multicard_pos_terminals " +
                                    " SET Merchant_Id='" + newMid + "' " +
                                    ", Settlement_Acct_No='" + update.NewSettlementAccount + "'" +
                                    " WHERE LTRIM(RTRIM(Terminal_Id)) = '" + item.ToUpper() + "' " +
                                    " ";
                    }
                    tidSourceList += terminals;
                    cmd = new AseCommand(tidSourceList, con);
                    int conResult = cmd.ExecuteNonQuery();
                    if (conResult > 0)
                    {

                    }
                    update.NewMerchantId = newMid;

                }
                else
                {
                    string query = string.Empty;
                    foreach (var terminal in update.ReAssignedTerminals.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(terminal))
                        {
                            query += string.Format("UPDATE infosys..zib_multicard_tid_sourcelist SET Merchant_Id='{0}' , Settlement_Acct_No='{1}', Requesting_Branch='{2}', Reg_Date = Reg_Date WHERE Terminal_Id = '{3}'    " +
                                               "UPDATE infosys..zib_multicard_pos_terminals SET Merchant_Id='{4}' , Settlement_Acct_No='{5}' WHERE Terminal_Id = '{6}' ", update.NewMerchantId, update.NewSettlementAccount, branch, terminal, update.NewMerchantId, update.NewSettlementAccount, terminal); 
                        }
                    }
                    cmd = new AseCommand(query, con);
                    var result = cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UpdateNotification(MerchantUpdate update, string user)
        {
            string query = string.Empty;
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                if (update.AddToExisting)
                {
                    query = string.Format("UPDATE infosys..zib_multicard_pos_terminals  SET Email_address='{2}\n{0}'  WHERE merchant_id='{1}'", update.NewEmailAddress, update.MerchantId, update.CurrentEmailAddress);
                }
                else
                {
                    query = string.Format("UPDATE infosys..zib_multicard_pos_terminals  SET Email_address='{0}'  WHERE merchant_id='{1}'", update.NewEmail, update.MerchantId);
                }
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            return false;

        }

        public void UpdateReportView(MerchantUpdate update, string user)
        {
            using (SqlConnection con = new SqlConnection(_settings.NotificationConnectionString))
            {
                con.Open();
                if (update.AddToExisting)
                {
                    string query = string.Format("Select UPPER(LTRIM(RTRIM(UserID))) AS UserID from tblMerchantDetails where UserID = '" + update.NewUsername + "'");
                    SqlCommand cmd = new SqlCommand(query, con);
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        throw new ArgumentException(string.Format("The User ID {0} already exists in the notification table. Please notify the Initiator or RSM.", update.NewUsername));
                    }
                    query = string.Format("dbo.Sproc_SubmitMerchantDetails('{0}','{1},'{2}','{3}','{4:yyyy-MM-dd}'", update.NewUsername, update.CurrentSettlementAccount, update.MerchantName, update.MerchantId, DateTime.Now);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    query = string.Format("Select UPPER(LTRIM(RTRIM(UserID))) AS UserID from tblUser where RTRIM(LTRIM(LOWER(UserID))) = '" + update.NewUsername + "'");
                    cmd.CommandText = query;
                    var newResult = cmd.ExecuteReader();
                    if (newResult.Read())
                    {
                        throw new ArgumentException(string.Format("The User ID {0} already exists in the notification table. Please notify the Initiator or RSM.", update.NewUsername));
                    }
                    query = string.Format("dbo.Sproc_CreateUser('{0}','{1}','{2}','{3}','{4}','{5}','{6:yyyy-MM-dd}')", update.NewUsername, "Merchant", update.NewEmail, "password", "Y", "Active", DateTime.Now);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    string query = string.Format("UPDATE tblMerchantDetails SET UserID='{0}'  WHERE ltrim(rtrim(merchantid))='{1}'", update.NewUsername, update.MerchantId);
                    /* +      " UPDATE tblUser " +
                              " SET UserID='"+new_username+"' " +
                              " , Email='"+doc.getItemValueString("new_email_1").trim().toLowerCase()+"' " +
                              " WHERE ltrim(rtrim(UserID))='"+new_username+"'" +
                              " ";*/
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UpdateSettlementAccount(MerchantUpdate update, string user)
        {
            using (AseConnection con = new AseConnection(_settings.MerchantConnectionString))
            {
                string query = string.Format("UPDATE infosys..zib_multicard_pos_terminals SET settlement_acct_no='{0}' WHERE merchant_id='{1}'", update.NewSettlementAccount, update.MerchantId);
                AseCommand cmd = new AseCommand(query, con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
