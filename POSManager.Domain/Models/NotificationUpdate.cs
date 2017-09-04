﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class NotificationUpdate
    {
        public int Id { get; set; }
        public int POSRequestId { get; set; }
        public string RequestingBranch { get; set; }
        public string Department { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string CurrentEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
        public bool AddToExisting { get; set; }
        public string CustomerRequest { get; set; }
        public string Comment { get; set; }
    }
}