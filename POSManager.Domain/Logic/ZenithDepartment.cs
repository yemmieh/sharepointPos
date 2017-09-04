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
    public class ZenithDepartment : IDepartment
    {
        public List<string> GetDepartments(string query)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Please set X Database Connection String");
            }
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Please provide query to be excuted");
            }
            List<string> department = new List<string>();
            SqlConnection con = new SqlConnection(_connectionString);//TODO: Insert query here
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
                department.Add(result["description"].ToString());
            }
            con.Close();
            return department.OrderBy( c => c).ToList();
        }

        private string _connectionString;

        public Settings Settings
        {
            set { _connectionString = value.XDBConnectionString; }
        }
    }
}
