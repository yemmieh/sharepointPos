using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestDepartment: IDepartment
    {
        public List<string> GetDepartments(string query)
        {
            return new List<string> { "Customer", "Info Tech" };
        }

        public Settings Settings
        {
            set {  }
        }
    }
}
