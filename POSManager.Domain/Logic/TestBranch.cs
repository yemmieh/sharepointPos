using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestBranch : IBranch
    {
        private Settings _settings;

        public List<Branch> GetBranches()
        {
            return new List<Models.Branch>{
                new Branch{
                    Code = "001",
                    Name = "Victoria Island"
                },
                new Branch{
                    Code = "002",
                    Name = "Maryland"
                },
                new Branch{
                    Code = "003",
                    Name = "Ikotun"
                },
                new Branch{
                    Code = "004",
                    Name = "Ogbomoso"
                }
            };
        }

        public Settings Settings
        {
            set { _settings = value; }
        }
    }
}
