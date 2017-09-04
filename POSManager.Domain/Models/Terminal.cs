using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50), Required]
        public string TerminalId { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string SerialNo { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string TerminalModel { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Route { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Provider { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string NetworkDevice { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Status { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string Switch { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string SIM { get; set; }
        public DateTime? DeployedOn { get; set; }
        public bool Deployed { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
