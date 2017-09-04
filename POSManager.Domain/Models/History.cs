﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSManager.Domain.Models
{
    public class History
    {
        public int Id { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
        [Column(TypeName = "VarChar"), StringLength(50)]
        public string RequestStage { get; set; }
        [Column(TypeName = "VarChar"), StringLength(150)]
        public string Action { get; set; }
        [Column(TypeName = "VarChar"), StringLength(500)]
        public string Comment { get; set; }
        [ForeignKey("POSRequest")]
        public int POSRequestId { get; set; }
        public virtual POSRequest POSRequest { get; set; }
    }
}
