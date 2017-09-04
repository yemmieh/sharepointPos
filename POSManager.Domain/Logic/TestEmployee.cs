using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestEmployee: IEmployee
    {
        private Settings _settings;
        public Settings Settings
        {
            set { _settings = value; }
        }

        public Employee GetEmployee(string username)
        {
            List<Employee> employees = Employees();
            return employees.FirstOrDefault(c => c.Username.ToUpper() == username.ToUpper());
        }

        private static List<Employee> Employees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee()
                {
                    Branch = "003",
                    Department = "Customer Service",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "12345",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Name = "Ibraheem Bello",
                    Line = "LL2",
                    Username = "dev\\Ibraheem"
                },
                new Employee()
                {
                    Branch = "002",
                    Department = "Customer Service Unit",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "123",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Name = "User1 Bello",
                    Line = "LL2",
                    Username = "dev\\User1"
                },
                new Employee()
                {
                    Branch = "003",
                    Department = "Customer Service",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "2345",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Line = "LL2",
                    Name = "User2 Kolawole",
                    Username = "dev\\User2"
                }
            };
            return employees;
        }

        public Employee GetEmployeeStaffNo(string staffNo)
        {
            var employees = Employees();
            return employees.FirstOrDefault(c => c.StaffNo.ToUpper() == staffNo.ToUpper());
        }

        public List<Employee> GetEmployees(string query)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee()
                {
                    Branch = "001",
                    Department = "HR",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "123",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Name = "Ibraheem Bello",
                    Username = "dev\\Ibraheem"
                },
                new Employee()
                {
                    Branch = "002",
                    Department = "Customer Service Unit",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "123",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Name = "User1 Bello",
                    Username = "dev\\User1"
                },
                new Employee()
                {
                    Branch = "003",
                    Department = "Customer Service Unit",
                    JobFunction = "Customer Service Unit Office",
                    StaffNo = "123",
                    Gender = "Male",
                    Grade = "Senior Developer",
                    Name = "User2 Bello",
                    Username = "dev\\User2"
                }
            };
            return employees;
        }
    }
}
