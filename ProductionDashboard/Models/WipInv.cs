using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    [Table("vwINV_Class")]
    public class invClass
    {
        [Key]
        public string GUID { get; set; }
        public string Groupings { get; set; }
        public string Classification { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }

    [Table("vwINV_Groups")]
    public class invGroups
    {
        [Key]
        public string GUID { get; set; }
        public string Groupings { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }

    [Table("vwWIP_Class")]
    public class wipClass
    {
        [Key]
        public string GUID { get; set; }
        public string Classification { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }

    [Table("vwWIP_Groups")]
    public class wipGroups
    {
        [Key]
        public string GUID { get; set; }
        public string Groupings { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }

    [Table("vwAssemblyWIP_Class")]
    public class wipAssyClass
    {
        [Key]
        public string GUID { get; set; }
        public string Classification { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }

    [Table("vwAssemblyWIP_Groups")]
    public class wipAssyGroups
    {
        [Key]
        public string GUID { get; set; }
        public string Groupings { get; set; }
        public string Operations { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public DateTime DateUpload { get; set; }
    }



    [Table("vwASMBLDPRD_ProductGroupParentNEW")]
    public class invParents
    {
        [Key]
        public string Connector { get; set; }
        public string ProductGroup { get; set; }
        public string PackageGroup1 { get; set; }      
        public int Qty { get; set; }
        public DateTime AsOf { get; set; }
    }

    [Table("vwASMBLDPRD_ProductGroupChildNEW")]
    public class invChild
    {
        [Key]
        public string Connector     { get; set; }
        public string ProductGroup  { get; set; }
        public string PackageGroup1 { get; set; }
        public string PackageGroup2 { get; set; }
        public string PackageGroup3 { get; set; }
        public int    Qty           { get; set; }
        //   public DateTime DateUpload { get; set; }
    }

    [Table("vwWIPTest_ProductGroupNEW")]
    public class wipProductGroupNEW
    {
        [Key]
        public string ProductGroup { get; set; }
        public string Operation { get; set; }        
        public int Qty { get; set; }
        public DateTime AsOf { get; set; }
    }

    [Table("vwWIPTest_PackageGroup1NEW")]
    public class wipPackageGroup1NEW
    {
        [Key]
        public string PackageGroup1 { get; set; }
        public string PackageGroup2 { get; set; }
        public string PackageGroup3 { get; set; }
        public string Operation { get; set; }        
        public int Qty { get; set; }
        public DateTime AsOf { get; set; }
    }


    [Table("vwINVSTOCK_ProductGroupParentNEW")]
    public class invStocksProductGroupParentNew
    {
        [Key]
        public string Connector { get; set; }
        public string ProductGroup { get; set; }
        public string PackageGroup1 { get; set; }
        public int Qty { get; set; }
        public DateTime AsOf { get; set; }
    }

    [Table("vwINVSTOCK_ProductGroupChildNEW")]
    public class invStocksProductGroupChildNEW
    {
        [Key]
        public string Connector { get; set; }
        public string ProductGroup { get; set; }
        public string PackageGroup1 { get; set; }
        public string PackageGroup2 { get; set; }
        public string PackageGroup3 { get; set; }
        public int Qty { get; set; }        
    }


}