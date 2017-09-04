using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic.Abstract
{
    public interface IMessagingService
    {
        bool Send(string to, string Subject, string body);
    }
}
