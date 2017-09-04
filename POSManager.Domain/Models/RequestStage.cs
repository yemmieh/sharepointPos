using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class RequestStage
    {
        public static Func<string, string> BRANCH_OR_HEAD_OFFICE = (c => c == "001" ? "Unit/Department Head" : "HOP/Branch Head");
        public const string E_BUSINESS = "E-Business";
        public const string POS_UNIT_CONFIGURATION = "POS Unit Configuration";
        public const string POS_UNIT_INTERSWITCH_VALUE_CARD = "POS Unit Interswitch/Value";
        public const string COMPLETED = "Completed";
        public const string NIBSS = "Nibss";

        public static List<string> GetAll(string branchCode)
        {
            return new List<string> { BRANCH_OR_HEAD_OFFICE(branchCode), POS_UNIT_CONFIGURATION, POS_UNIT_INTERSWITCH_VALUE_CARD, };
        }
    }
}
