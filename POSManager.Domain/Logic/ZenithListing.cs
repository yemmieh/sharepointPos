using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class ZenithListing : IListing
    {
        public List<KeyValuePair<string, string>> GetState(string countryCode)
        {
            List<KeyValuePair<string, string>> state = new List<KeyValuePair<string, string>>();
            using (SqlConnection con = new SqlConnection(_settings.KDBConnectionString))
            {
                string query = "SELECT STATE_ID, STATE_NAME FROM STATE_LISTING WHERE COUNTRY_CODE='" + countryCode + "' ORDER BY STATE_NAME ASC";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    state.Add(new KeyValuePair<string, string>(result["STATE_ID"].ToString().Trim(), result["STATE_NAME"].ToString().Trim()));
                }
            }
            return state;
        }

        public List<string> GetLGA(string stateCode)
        {
            List<string> state = new List<string>();
            using (SqlConnection con = new SqlConnection(_settings.KDBConnectionString))
            {
                string query = "SELECT LGA_ID, LGA_NAME FROM LGA_LISTING WHERE STATE_CODE='" + stateCode + "' ORDER BY LGA_NAME ASC";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    state.Add(result["LGA_NAME"].ToString().Trim());
                }
            }
            return state;
        }

        private Settings _settings;

        public Models.Settings Settings
        {
            set { _settings = value; }
        }
    }
}
