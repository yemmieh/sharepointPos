using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Location
    {
        public Location()
        {
            Terminals = new List<Terminal>();
        }
        public int Id { get; set; }
        public int NoOfTerminal { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string SettlementAccountNumber { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string SettlementAcccountName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string Address { get; set; }
        [Column(TypeName = "VarChar"), StringLength(100)]
        public string City { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string LGA { get; set; }
        [Column(TypeName = "VarChar"), StringLength(5)]
        public string StateCode { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string State { get; set; }
        [Column(TypeName = "VarChar"), StringLength(100)]
        public string ContactName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Phone { get; set; }
        [Column(TypeName = "VarChar"), StringLength(70)]
        public string Email { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string ViewerFirstName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string ViewerLastName { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string ViewerUsername { get; set; }
        [Column(TypeName = "VarChar"), StringLength(70)]
        public string ViewerEmail { get; set; }
        [ForeignKey("POSRequest")]
        public int POSRequestId { get; set; }
        public virtual POSRequest POSRequest { get; set; }
        public virtual ICollection<Terminal> Terminals { get; set; }
    }
}
