using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IListing: ISettings
    {
        List<KeyValuePair<string, string>> GetState(string countryCode);
        List<string> GetLGA(string stateCode);
    }
}
