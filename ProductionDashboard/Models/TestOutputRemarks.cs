using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{

    [Table("TestOutputRemarks")]
    public class TestOutputRemarks
    {
        [Key]
        public string TestOutputID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }

    }

    [Table("RemarksLogs")]
    public class HistoricalRemarks
    {
        [Key]
        public string GUID { get; set; }
        public string ConnectorID { get; set; }
        public string Department { get; set; }
        public string Operation { get; set; }
        public string OriginalRemarks { get; set; }
        public string UpdatedRemarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateAdded { get; set; }


    }



    [Table("AssemblyOutputRemarks")]
    public class AssemblyOutputRemarks
    {
        [Key]
        public string GUID { get; set; }
        public string AssemblyOutsID { get; set; }
        public string Remarks { get; set; }
        public string Operation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }

    }

    [Table("User")]
    public class TestUser
    {
        [Key]
        public string UserID { get; set; }
        public string UserAccess { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
    
    }

}