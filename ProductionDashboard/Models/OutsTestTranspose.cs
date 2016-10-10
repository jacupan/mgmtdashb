using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace ProductionDashboard.Models
{    

    [Table("vwOutsTestTranspose_summaryperMonth")]      //OLD VIEW FIXED FOR MONTH OF JULY (FOR TESTING ONLY)
    public class OutsTestTranspose_summaryperMonth
    {
        [Key]
        public string GUID { get; set; }
        public int WorkWeek { get; set; }
        public string Groupings { get; set; }
        public string Classification { get; set; }
        public string FMonth { get; set; }
        public string WorkShift { get; set; }
        public string Operations { get; set; }
        public int WW27 { get; set; }
        public int WW28 { get; set; }
        public int WW29 { get; set; }
        public int WW30 { get; set; }
        public int TotalQty { get; set; }
    }


    [Table("vwOutsTestTranspose_workDayWTD_parentClass")]           // for each classification per workweek
    public class outsTestTransposeWorkDayWTDParentClass
    {

       
        //public string GUID { get; set; }
        public int WorkWeek { get; set; }
        [Key]
        public string OpClass { get; set; }
        public string FYrWk { get; set; }
        public string Operations { get; set; }
        public string Groupings { get; set; }
        public string Classification { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
    }

    [Table("vwOutsTestTranspose_workShiftWTD_childClass")]         // for each classification per workweek with night/day
    public class outsTestTransposeWorkShiftWTDChildClass
    {
       
        //public string GUID { get; set; }
        public int WorkWeek { get; set; }
        [Key]
        public string OpClass { get; set; }
        public string FYrWk { get; set; }
        public string WorkShift { get; set; }
        public string Operations { get; set; }
        public string Groupings { get; set; }
        public string Classification { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
    }

    [Table("vwOutsTestTranspose_workDayWTD_parentGroup")]           // for each grouping per workweek
    public class outsTestTransposeWorkDayWTDParentGroup
    {


        //public string GUID { get; set; }
        public int WorkWeek { get; set; }
        [Key]
        public string OpGroups { get; set; }
        public string FYrWk { get; set; }
        public string Operations { get; set; }
        public string Groupings { get; set; }        
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
    }

    [Table("vwOutsTestTranspose_workShiftWTD_childGroup")]         // for each grouping per workweek with night/day
    public class outsTestTransposeWorkShiftWTDChildGroup
    {

        //public string GUID { get; set; }
        public int WorkWeek { get; set; }
        [Key]
        public string OpGroups { get; set; }
        public string FYrWk { get; set; }
        public string WorkShift { get; set; }
        public string Operations { get; set; }
        public string Groupings { get; set; }        
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
    }

}