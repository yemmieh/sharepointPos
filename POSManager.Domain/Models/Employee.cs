using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Employee
    {
        public string Username { get; set; }
        public string StaffNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string JobFunction { get; set; }
        public string Line { get; set; }
        public string Grade { get; set; }
    }
}
