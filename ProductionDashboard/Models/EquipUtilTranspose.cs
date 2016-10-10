using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProductionDashboard.Models
{
    [Table("vwEquipUtilTranspose_workDayWTD_parentClass")]              // for each classification per workweek
    public class equipUtilTransposeWorkDayWTDParentClass
    {
        [Key]
        public string EquipClass { get; set; }
        public string FYrWk { get; set; }
        public string Classification { get; set; }
        public string EquipmentPackageFamily { get; set; }
        public string EquipmentFamily { get; set; }
        public string Equipment { get; set; }
        public string ParentVendorModel { get; set; }
        public string FromStatusGroup { get; set; }
        public int WorkWeek { get; set; }        
        public decimal Target { get; set; }
        public decimal Saturday { get; set; }
        public decimal Sunday { get; set; }
        public decimal Monday { get; set; }
        public decimal Tuesday { get; set; }
        public decimal Wednesday { get; set; }
        public decimal Thursday { get; set; }
        public decimal Friday { get; set; }
        public decimal TotalHours { get; set; }
    }

    [Table("vwEquipUtilTranspose_workShiftWTD_childClass")]             // for each classification per workweek with night/day
    public class equipUtilTransposeWorkShiftWTDChildClass
    {
        [Key]
        public string EquipClass { get; set; }
        public string FYrWk { get; set; }
        public string Classification { get; set; }
        public string EquipmentPackageFamily { get; set; }
        public string EquipmentFamily { get; set; }
        public string Equipment { get; set; }
        public string ParentVendorModel { get; set; }
        public string FromStatusGroup { get; set; }
        public int WorkWeek { get; set; }
        public string WorkShift { get; set; }
        public decimal Target { get; set; }
        public decimal Saturday { get; set; }
        public decimal Sunday { get; set; }
        public decimal Monday { get; set; }
        public decimal Tuesday { get; set; }
        public decimal Wednesday { get; set; }
        public decimal Thursday { get; set; }
        public decimal Friday { get; set; }
        public decimal TotalHours { get; set; }
    }

    [Table("vwEquipUtilTranspose_workDayWTD_parentGroup")]              // for each grouping per workweek
    public class equipUtilTransposeWorkDayWTDParentGroup
    {
        [Key]
        public string EquipGroups { get; set; }
        public string FYrWk { get; set; }
        public string Groupings { get; set; }
        public string EquipmentPackageFamily { get; set; }
        public string EquipmentFamily { get; set; }
        public string Equipment { get; set; }
        public string ParentVendorModel { get; set; }
        public string FromStatusGroup { get; set; }
        public int WorkWeek { get; set; }
        public decimal Target { get; set; }
        public decimal Saturday { get; set; }
        public decimal Sunday { get; set; }
        public decimal Monday { get; set; }
        public decimal Tuesday { get; set; }
        public decimal Wednesday { get; set; }
        public decimal Thursday { get; set; }
        public decimal Friday { get; set; }
        public decimal TotalHours { get; set; }
    }

    [Table("vwEquipUtilTranspose_workShiftWTD_childGroup")]             // for each grouping per workweek with night/day
    public class equipUtilTransposeWorkShiftWTDChildGroup
    {
        [Key]
        public string EquipGroups { get; set; }
        public string FYrWk { get; set; }
        public string Groupings { get; set; }
        public string EquipmentPackageFamily { get; set; }
        public string EquipmentFamily { get; set; }
        public string Equipment { get; set; }
        public string ParentVendorModel { get; set; }
        public string FromStatusGroup { get; set; }
        public int WorkWeek { get; set; }
        public string WorkShift { get; set; }
        public decimal Target { get; set; }
        public decimal Saturday { get; set; }
        public decimal Sunday { get; set; }
        public decimal Monday { get; set; }
        public decimal Tuesday { get; set; }
        public decimal Wednesday { get; set; }
        public decimal Thursday { get; set; }
        public decimal Friday { get; set; }
        public decimal TotalHours { get; set; }
    }
}