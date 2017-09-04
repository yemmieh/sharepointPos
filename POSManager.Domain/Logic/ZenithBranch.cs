using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class ZenithBranch: IBranch
    {
        public List<Models.Branch> GetBranches()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Please provide connection string");
            }
            List<Branch> branches = new List<Branch>();
            SqlConnection con = new SqlConnection(_connectionString);
            var query = "select * from vw_branch_analysis where org_id = 1"; //TODO: Insert query here
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
                branches.Add(new Branch
                {
                    //TODO: 
                    Code = result["analysis_det_code"].ToString(),
                    Name = result["description"].ToString(),
                });
            }
            con.Close();
            return branches.OrderBy(c => c.Name).ToList();
        }

        private string _connectionString;
        public Settings Settings
        {
            set { _connectionString = value.XDBConnectionString; }
        }
    }
}
