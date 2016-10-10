using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    [Table("vwTreeViewLookUp_Assy")]
    public class OutsAssyLookUp
    {
        [Key]
        public string ProductGroup { get; set; }
    }
}