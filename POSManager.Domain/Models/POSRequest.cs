using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class POSRequest
    {

        public POSRequest()
        {
            Histories = new List<History>();
            Locations = new List<Location>();
        }
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public Guid UniqueNo { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string InitiatedBy { get; set; }
        public DateTime InitiatedOn { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string Status { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string Pending { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string MerchantId { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string RequestingBranch { get; set; }
        [Column(TypeName = "VarChar"), StringLength(100)]
        public string Department { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string AccountNumber { get; set; }
        public int RIMNO { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150), Required]
        public string AccountName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string AccountOpeningDate { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string RCNumber { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string RegisteredAddress { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Sector { get; set; }
        public decimal LastMonthTurnover { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string AccountOfficer { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string AccountOfficerPhone { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string AccountStatus { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string InternalAccountNumber { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string InternalAccountStatus { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string MerchantTradeName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string MerchantBusinessClassification { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string MerchantCatergory { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string DescriptionOfBusiness { get; set; }
        [Column(TypeName = "VarChar"), StringLength(40)]
        public string BusinessTime { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string IKEDCAgentAccountNumber { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string CommunicationMode { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string PTSP { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string ISOType { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string KeyType { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string POSType { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string POSFormURL { get; set; }
        public bool Purged { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<History> Histories { get; set; }

    }
}
