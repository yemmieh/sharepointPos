using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class MerchantUpdate
    {
        public MerchantUpdate()
        {
            MerchantUpdateHistories = new List<MerchantUpdateHistory>();
        }
        public int Id { get; set; }
        public int? POSRequestId { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string RequestingBranch { get; set; }

        [Column(TypeName = "VarChar"), StringLength(100)]
        public string Department { get; set; }
            
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string MerchantId { get; set; }
        
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string MerchantName { get; set; }

        [Column(TypeName = "VarChar"), StringLength(500)]
        public string CustomerRequest { get; set; }

        [Column(TypeName = "VarChar")]
        public string Comment { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Stage { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string UpdateType { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string CurrentSettlementAccount { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NewSettlementAccount { get; set; }

        [Column(TypeName = "VarChar")]
        public string CurrentTerminals { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NewMerchantId { get; set; }

        [Column(TypeName = "VarChar"), StringLength(150)]
        public string AccountName { get; set; }
        [Column(TypeName = "VarChar")]
        public string ReAssignedTerminals { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        //Report
        public string CurrentUsername { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NewUsername { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NewEmail { get; set; }
        public bool AddToExisting { get; set; }
        //notification
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string CurrentEmailAddress { get; set; }

        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NewEmailAddress { get; set; }
        public virtual ICollection<MerchantUpdateHistory> MerchantUpdateHistories { get; set; }
    }
}
