using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    [Table("vwOutsTestLookUp")]
    public class OutsTestLookUp_1
    {        
        public string TestGrouping { get; set; }
        [Key]
        public string TestClassification { get; set; }       
    }

    [Table("vwTreeViewLookUp_Test")]
    public class OutsTestLookUp
    {

        public string ProductGroup { get; set; }
        [Key]
        public string PackageGroup1 { get; set; }
       
    }
}