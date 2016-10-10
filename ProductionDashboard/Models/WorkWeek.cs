using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    [Table("vwWorkWeek")]
    public class WorkWeek
    {
        
        public int FISCALMONTHINT { get; set; }
        public string FISCALMONTHNAME { get; set; }
        [Key]
        public string WORKWEEKINT { get; set; }
    }


}