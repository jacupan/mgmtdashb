using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    [Table("vwFGTranspose_Class")]
    public class fgTransposeClass
    {
        [Key]
        public string GUID { get; set; }
        public string FYrWk { get; set; }
        public string Classification { get; set; }
        public int WorkWeek {get; set;}
        public string WorkShift { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
        public int TargetQty { get; set; }

    }

    [Table("vwFGTranspose_Groups")]
    public class fgTransposeGroups
    {
        [Key]
        public string GUID { get; set; }
        public string FYrWk { get; set; }
        public string Groupings { get; set; }
        public int WorkWeek { get; set; }
        public string WorkShift { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int TotalQty { get; set; }
        public int TargetQty { get; set; }

    }

    [Table("vwFGActualsVsTarget_WWSummary_ProductGroupNEW")]
    public class fgActualsVsTargetWwSummaryProductGroupNEW
    {
        public string ProductGroup { get; set; }
        [Key]
        public string FYrWeek { get; set; }
        public int WWTotalActual { get; set; }
        public double WWTotalTarget { get; set; }
        public double Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_WWSummary_PackageGroup1NEW")]
    public class fgActualsVsTargetWwSummaryPackageGroup1NEW
    {
        public string PackageGroup1 { get; set; }
        [Key]
        public string FYrWeek { get; set; }
        public int WWTotalActual { get; set; }
        public double WWTotalTarget { get; set; }
        public double Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_MonthlySummary_ProductGroupNEW")]
    public class fgActualsVsTargetMonthlySummaryProductGroupNEW
    {
        [Key]
        public string FYrMonth { get; set; }
        public string ProductGroup { get; set; }
        public int MonthlyTotalActual { get; set; }
        public double MonthlyTotalTarget { get; set; }
        public double Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_MonthlySummary_PackageGroup1NEW")]
    public class fgActualsVsTargetMonthlySummaryPackageGroup1NEW
    {
        [Key]
        public string FYrMonth { get; set; }
        public string PackageGroup1 { get; set; }
        public int MonthlyTotalActual { get; set; }
        public double MonthlyTotalTarget { get; set; }
        public double Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_WWSummary_Classification")]                 // old view
    public class fgActualsVsTargetWwSummaryClass
    {
        public string Classification { get; set; }
        [Key]
        public string FYrWeek { get; set; }
        public int WWTotalActual { get; set; }
        public int WWTotalTarget { get; set; }
        public int Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_WWSummary_Groupings")]                      // old view
    public class fgActualsVsTargetWwSummaryGroups
    {
        public string Groupings { get; set; }
        [Key]
        public string FYrWeek { get; set; }
        public int WWTotalActual { get; set; }
        public int WWTotalTarget { get; set; }
        public int Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_MonthlySummary_Classification")]            // old view
    public class fgActualsVsTargetMonthlySummaryClass
    {
        [Key]
        public string FYrMonth { get; set; }
        public string Classification { get; set; }
        public int MonthlyTotalActual { get; set; }
        public int MonthlyTotalTarget { get; set; }
        public int Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }

    [Table("vwFGActualsVsTarget_MonthlySummary_Group")]                     // old view
    public class fgActualsVsTargetMonthlySummaryGroups
    {
        [Key]
        public string FYrMonth { get; set; }
        public string Groupings { get; set; }
        public int MonthlyTotalActual { get; set; }
        public int MonthlyTotalTarget { get; set; }
        public int Balance_To_Target { get; set; }
        public int Attainment { get; set; }

    }



}