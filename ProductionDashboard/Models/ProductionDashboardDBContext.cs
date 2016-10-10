using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProductionDashboard.Models
{
    public class ProductionDashboardDBContext : DbContext
    {

        public DbSet<DashboardFG> DashBoardFGS { get; set; }

        public DbSet<fgTransposeClass> FGTransposesClass { get; set; }

        public DbSet<fgTransposeGroups> FGTransposesGroups { get; set; }       

        public DbSet<DashboardFGDynamic> DashBoardsDynamics { get; set; }

        public DbSet<fgActualsVsTargetWwSummaryPackageGroup1NEW> fgActualsVsTargetWwSummaryPackageGroup1NEW { get; set; }

        public DbSet<fgActualsVsTargetWwSummaryProductGroupNEW> fgActualsVsTargetWwSummaryProductGroupNEW { get; set; }

        public DbSet<fgActualsVsTargetMonthlySummaryPackageGroup1NEW> fgActualsVsTargetMonthlySummaryPackageGroup1NEW { get; set; }

        public DbSet<fgActualsVsTargetMonthlySummaryProductGroupNEW> fgActualsVsTargetMonthlySummaryProductGroupNEW { get; set; }

        public DbSet<invStocksProductGroupParentNew> invStocksProductGroupParentNew { get; set; }

        public DbSet<invStocksProductGroupChildNEW> invStocksProductGroupChildNEW { get; set; }

        public DbSet<WorkWeek> WorkWeeks { get; set; }   

        public DbSet<OutsTestTranspose_summaryperMonth> OutsTests_summaryperMonth { get; set; }

        public DbSet<outsTestTransposeWorkDayWTDParentClass> OutsTestTransposeWorkDayWTDc { get; set; }

        public DbSet<outsTestTransposeWorkShiftWTDChildClass> OutsTestTransposeWorkShiftWTDc { get; set; }

        public DbSet<outsTestTransposeWorkDayWTDParentGroup> OutsTestTransposeWorkDayWTDg { get; set; }

        public DbSet<outsTestTransposeWorkShiftWTDChildGroup> OutsTestTransposeWorkShiftWTDg { get; set; }

        public DbSet<invParents> invParents { get; set; }

        public DbSet<invChild> invChildren { get; set; }

        public DbSet<invClass> invClass { get; set; }

        public DbSet<invGroups> invGroups { get; set; }

        public DbSet<wipClass> wipClass { get; set; }

        public DbSet<wipGroups> wipGroups { get; set; }

        public DbSet<wipPackageGroup1NEW> wipPackageGroup1NEW { get; set; }

        public DbSet<wipProductGroupNEW> wipProductGroupNEW { get; set; }  
   
        public DbSet<wipAssyClass> wipAssyClass { get; set; }

        public DbSet<wipAssyGroups> wipAssyGroups { get; set; } 

        public DbSet<equipUtilTransposeWorkDayWTDParentClass> equipUtilWorkDayWTDParentClass { get; set; }

        public DbSet<equipUtilTransposeWorkShiftWTDChildClass> equipUtilWorkShiftWTDChildClass { get; set; }

        public DbSet<equipUtilTransposeWorkDayWTDParentGroup> equipUtilWorkDayWTDParentGroup { get; set; }

        public DbSet<equipUtilTransposeWorkShiftWTDChildGroup> equipUtilWorkShiftWTDChildGroup { get; set; }

        public DbSet<OutsTestLookUp> OutsTestLookUp { get; set; }

        public DbSet<OutsTestLookUp_1> OutsTestLookUp_1 { get; set; }

        public DbSet<OutsAssyLookUp> OutsAssyLookUp { get; set; }

        public DbSet<TestUser> Users { get; set; }

        public DbSet<TestOutputRemarks> Remarks { get; set; }

        public DbSet<AssemblyOutputRemarks> AssemblyRemarks { get; set; }

        public DbSet<HistoricalRemarks> RemarksLogs { get; set; }

        public ProductionDashboardDBContext() : base("name=PDDBContextPROD") { }
    }
}