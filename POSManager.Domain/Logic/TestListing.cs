using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestListing: IListing
    {
        public List<KeyValuePair<string, string>> GetState(string countryCode)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("LA", "Lagos"),
                new KeyValuePair<string, string>("AB", "Abia"),
                new KeyValuePair<string,string>("OY", "Oyo")
            };
        }

        public List<string> GetLGA(string stateCode)
        {
            return new List<string>()
            {
                "Alimosho","Ikeja"
            };
        }
        private Settings _settings;
        public Models.Settings Settings
        {
            set { _settings = value; }
        }
    }
}
