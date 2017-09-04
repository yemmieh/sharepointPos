using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IEmployee : ISettings
    {
        Employee GetEmployee(string username);
        Employee GetEmployeeStaffNo(string staffNo);
        List<Employee> GetEmployees(string query);
    }
}
