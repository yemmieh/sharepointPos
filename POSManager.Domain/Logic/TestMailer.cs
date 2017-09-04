using POSManager.Domain.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Logic
{
    public class TestMailer : IMessagingService
    {
        public bool Send(string to, string subject, string body)
        {
            return true;
        }
    }
}
