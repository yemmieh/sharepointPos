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
    public class ZenithEmployee : IEmployee
    {

        private string _connectionString;

        public Settings Settings
        {
            set
            {
                _connectionString = value.XDBConnectionString;
            }
        }

        public Models.Employee GetEmployee(string username)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Please set X Database Connection String");
            }
            Employee employee = new Employee();
            SqlConnection con = new SqlConnection(_connectionString);
            string query = string.Format("select * from vw_EmployeeInfo where logon_name = '{0}'", username);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
                employee.Branch = result["branch_code"].ToString();
                employee.Department = result["dept"].ToString();
                employee.JobFunction = result["jobtitle"].ToString();
                employee.Name = result["name"].ToString();
                employee.Username = result["logon_name"].ToString();
                employee.Line = result["line_number"].ToString().Trim();
                employee.StaffNo = result["employee_number"].ToString();
                employee.Grade = result["grade_code"].ToString();
            }
            con.Close();
            return employee;
        }

        public List<Employee> GetEmployees(string query)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Please set X Database Connection String");
            }
            List<Employee> employees = new List<Employee>();

            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
                Employee employee = new Employee();
                employee.Branch = result["branch_code"].ToString();
                employee.Department = result["dept"].ToString();
                employee.JobFunction = result["jobtitle"].ToString();
                employee.Name = result["name"].ToString();
                employee.Username = result["logon_name"].ToString();
                employee.Grade = result["grade_code"].ToString();
                employee.Line = result["line_number"].ToString().Trim();
                employee.StaffNo = result["employee_number"].ToString();
                employees.Add(employee);
            }
            con.Close();
            return employees;
        }

        public Employee GetEmployeeStaffNo(string staffNo)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Please set X Database Connection String");
            }
            Employee employee = new Employee();
            SqlConnection con = new SqlConnection(_connectionString);
            string query = string.Format("select * from vw_EmployeeInfo where employee_number = '{0}'", staffNo);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
                employee.Branch = result["branch_code"].ToString();
                employee.Department = result["dept"].ToString();
                employee.JobFunction = result["jobtitle"].ToString();
                employee.Name = result["name"].ToString();
                employee.Username = result["logon_name"].ToString();
                employee.Line = result["line_number"].ToString().Trim();
                employee.Grade = result["grade_code"].ToString();
                employee.StaffNo = result["employee_number"].ToString();
            }
            con.Close();
            return employee;
        }
    }
}
