using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ProductionDashboard.Models;
using GatePassApplication.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProductionDashboard.Controllers
{
    public partial class GridController : Controller
    {
        public ProductionDashboardDBContext pdDBContext = new ProductionDashboardDBContext();

        public ActionResult OutsSummaryWW_Read([DataSourceRequest]DataSourceRequest request, string fYrWk, string classification, string groupings)
        {
            DataSourceResult result = new DataSourceResult();

            if (classification.Contains("Summary"))
            {
                using (var appDB = new ProductionDashboardDBContext())
                {

                    try
                    {
                        var outs = (from o in appDB.OutsTestTransposeWorkDayWTDg
                                    where o.FYrWk == fYrWk && o.Groupings == groupings
                                    orderby o.Operations
                                    select new 
                                    {
                                        OpGroups = o.OpGroups,
                                        Operations = o.Operations,
                                        WorkWeek = o.WorkWeek,                                                                                       
                                        Saturday = o.Saturday,
                                        Sunday = o.Sunday,
                                        Monday = o.Monday,
                                        Tuesday = o.Tuesday,
                                        Wednesday = o.Wednesday,
                                        Thursday = o.Thursday,
                                        Friday = o.Friday,
                                        TotalQty = o.TotalQty,                                                                                   
                                    }
                            ).ToList(); 

                        result = outs.ToDataSourceResult(request);
                    }
                    catch (Exception ex)
                    {
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            else
            {
                using (var appDB = new ProductionDashboardDBContext())
                {

                    try
                    {
                       var outs = (from o in appDB.OutsTestTransposeWorkDayWTDc
                                    where o.FYrWk == fYrWk && o.Classification == classification
                                    orderby o.Operations
                                    select new
                                    {
                                        OpClass = o.OpClass,
                                        Operations = o.Operations,
                                        WorkWeek = o.WorkWeek,
                                        Saturday = o.Saturday,
                                        Sunday = o.Sunday,
                                        Monday = o.Monday,
                                        Tuesday = o.Tuesday,
                                        Wednesday = o.Wednesday,
                                        Thursday = o.Thursday,
                                        Friday = o.Friday,
                                        TotalQty = o.TotalQty,
                                    }
                            ).ToList(); 

                        result = outs.ToDataSourceResult(request);
                    }
                    catch (Exception ex)
                    {
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }

        }

        public class PseudoWorkShiftChildClass : outsTestTransposeWorkShiftWTDChildClass { }

        public ActionResult OutsSummaryWorkShift_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = new DataSourceResult();

            using (var appDB = new ProductionDashboardDBContext())
            {

                try
                {
                    IQueryable<outsTestTransposeWorkShiftWTDChildClass> outs = (from o in appDB.OutsTestTransposeWorkShiftWTDc
                                                                                where o.OpClass == id
                                                                                orderby o.Operations
                                                                                select new PseudoWorkShiftChildClass()
                                                                                {
                                                                                    OpClass = o.OpClass,
                                                                                    Operations = o.Operations,
                                                                                    WorkWeek = o.WorkWeek,
                                                                                    WorkShift = o.WorkShift,
                                                                                    Saturday = o.Saturday,
                                                                                    Sunday = o.Sunday,
                                                                                    Monday = o.Monday,
                                                                                    Tuesday = o.Tuesday,
                                                                                    Wednesday = o.Wednesday,
                                                                                    Thursday = o.Thursday,
                                                                                    Friday = o.Friday,
                                                                                    TotalQty = o.TotalQty,

                                                                                });

                    result = outs.ToDataSourceResult(request);
                }
                catch (Exception ex)
                {

                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }


        public class PseudoWorkShiftWTDgChildGroup : outsTestTransposeWorkShiftWTDChildGroup { }

        public ActionResult OutsSummaryWorkShiftGroups_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = new DataSourceResult();

            using (var appDB = new ProductionDashboardDBContext())
            {

                try
                {
                    IQueryable<outsTestTransposeWorkShiftWTDChildGroup> outs = (from o in appDB.OutsTestTransposeWorkShiftWTDg
                                                                                where o.OpGroups == id
                                                                                orderby o.Operations
                                                                                select new PseudoWorkShiftWTDgChildGroup()
                                                                                {
                                                                                    OpGroups = o.OpGroups,
                                                                                    Operations = o.Operations,
                                                                                    WorkWeek = o.WorkWeek,
                                                                                    WorkShift = o.WorkShift,
                                                                                    Saturday = o.Saturday,
                                                                                    Sunday = o.Sunday,
                                                                                    Monday = o.Monday,
                                                                                    Tuesday = o.Tuesday,
                                                                                    Wednesday = o.Wednesday,
                                                                                    Thursday = o.Thursday,
                                                                                    Friday = o.Friday,
                                                                                    TotalQty = o.TotalQty,

                                                                                });

                    result = outs.ToDataSourceResult(request);
                }
                catch (Exception ex)
                {

                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }


        public ActionResult DashBoardFGBalWW_Read([DataSourceRequest]DataSourceRequest fgRequest, string fYrWk, string classification, string groupings)
        {            
            if (classification.Contains("Summary"))
            {                    
                var fgResult = (from a in pdDBContext.fgActualsVsTargetWwSummaryProductGroupNEW
                                where a.FYrWeek == fYrWk && a.ProductGroup == groupings
                                select new
                                {
                                    WWTotalActual = a.WWTotalActual,
                                    WWTotalTarget = a.WWTotalTarget,
                                    Balance_To_Target = a.Balance_To_Target,
                                    Attainment = a.Attainment

                                }).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }

            else
            {                    
                var fgResult = (from a in pdDBContext.fgActualsVsTargetWwSummaryPackageGroup1NEW
                                where a.FYrWeek == fYrWk && a.PackageGroup1 == classification
                                select new
                                {
                                    WWTotalActual = a.WWTotalActual,
                                    WWTotalTarget = a.WWTotalTarget,
                                    Balance_To_Target = a.Balance_To_Target,
                                    Attainment = a.Attainment

                                }).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult DashBoardFGBalMo_Read([DataSourceRequest]DataSourceRequest fgRequest, string fYrMo, string classification, string groupings)
        {
            if (classification.Contains("Summary"))
            {

                var fgResult = (from a in pdDBContext.fgActualsVsTargetMonthlySummaryProductGroupNEW
                                where a.FYrMonth == fYrMo && a.ProductGroup == groupings
                                select new
                                {
                                    MonthlyTotalActual = a.MonthlyTotalActual,
                                    MonthlyTotalTarget = a.MonthlyTotalTarget,
                                    Balance_To_Target = a.Balance_To_Target,
                                    Attainment = a.Attainment

                                }
                                ).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }

            else
            {
                var fgResult = (from a in pdDBContext.fgActualsVsTargetMonthlySummaryPackageGroup1NEW
                                where a.FYrMonth == fYrMo && a.PackageGroup1 == classification
                                select new
                                {
                                    MonthlyTotalActual = a.MonthlyTotalActual,
                                    MonthlyTotalTarget = a.MonthlyTotalTarget,
                                    Balance_To_Target = a.Balance_To_Target,
                                    Attainment = a.Attainment

                                }).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }        
           
        }


        public ActionResult DashBoardFG_Read([DataSourceRequest]DataSourceRequest fgRequest, string fYrWk, string classification, string groupings)
        {

            if (classification.Contains("Summary"))
            {
                var fgResult = (from a in pdDBContext.FGTransposesGroups
                                where a.FYrWk == fYrWk && a.Groupings == groupings
                                select new
                                {
                                    Guid = Guid.NewGuid(),
                                    WorkWeek = a.WorkWeek,
                                    WorkShift = a.WorkShift,
                                    Saturday = a.Saturday,
                                    Sunday = a.Sunday,
                                    Monday = a.Monday,
                                    Tuesday = a.Tuesday,
                                    Wednesday = a.Wednesday,
                                    Thursday = a.Thursday,
                                    Friday = a.Friday,
                                    TotalQty = a.TotalQty,
                                    TargetQty = a.TargetQty

                                }).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fgResult = (from a in pdDBContext.FGTransposesClass
                                where a.FYrWk == fYrWk && a.Classification == classification
                                select new
                                {
                                    Guid = Guid.NewGuid(),
                                    WorkWeek = a.WorkWeek,
                                    WorkShift = a.WorkShift,
                                    Saturday = a.Saturday,
                                    Sunday = a.Sunday,
                                    Monday = a.Monday,
                                    Tuesday = a.Tuesday,
                                    Wednesday = a.Wednesday,
                                    Thursday = a.Thursday,
                                    Friday = a.Friday,
                                    TotalQty = a.TotalQty,
                                    TargetQty = a.TargetQty

                                }).ToList();

                return Json(fgResult.ToDataSourceResult(fgRequest), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EquipUtilSummary_Read([DataSourceRequest]DataSourceRequest request, string fYrWk, string classification, string groupings)
        {
            DataSourceResult result = new DataSourceResult();

            if (classification.Contains("Summary"))
            {
                using (var appDB = new ProductionDashboardDBContext())
                {

                    try
                    {
                        var equipUtil = (from o in appDB.equipUtilWorkDayWTDParentGroup
                                         where o.FYrWk == fYrWk && o.Groupings == groupings
                                         orderby o.Equipment
                                         select new
                                         {
                                             EquipGroups = o.EquipGroups,
                                             EquipmentPackageFamily = o.EquipmentPackageFamily,
                                             EquipmentFamily = o.EquipmentFamily,
                                             Equipment = o.Equipment,
                                             ParentVendorModel = o.ParentVendorModel,
                                             Target = o.Target,
                                             WorkWeek = o.WorkWeek,
                                             Saturday = o.Saturday / 24,
                                             Sunday = o.Sunday / 24,
                                             Monday = o.Monday / 24,
                                             Tuesday = o.Tuesday / 24,
                                             Wednesday = o.Wednesday / 24,
                                             Thursday = o.Thursday / 24,
                                             Friday = o.Friday / 24,
                                             TotalHours = o.TotalHours / 168
                                         }).ToList();

                        result = equipUtil.ToDataSourceResult(request);
                    }
                    catch (Exception ex)
                    {
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            else
            {
                using (var appDB = new ProductionDashboardDBContext())
                {

                    try
                    {
                        var equipUtil = (from o in appDB.equipUtilWorkDayWTDParentClass
                                         where o.FYrWk == fYrWk && o.Classification == classification
                                         orderby o.Equipment
                                         select new
                                         {
                                             EquipClass = o.EquipClass,
                                             EquipmentPackageFamily = o.EquipmentPackageFamily,
                                             EquipmentFamily = o.EquipmentFamily,
                                             Equipment = o.Equipment,
                                             ParentVendorModel = o.ParentVendorModel,
                                             Target = o.Target,
                                             WorkWeek = o.WorkWeek,
                                             Saturday = o.Saturday / 24,
                                             Sunday = o.Sunday / 24,
                                             Monday = o.Monday / 24,
                                             Tuesday = o.Tuesday / 24,
                                             Wednesday = o.Wednesday / 24,
                                             Thursday = o.Thursday / 24,
                                             Friday = o.Friday / 24,
                                             TotalHours = o.TotalHours / 168
                                         }).ToList();

                        result = equipUtil.ToDataSourceResult(request);
                    }
                    catch (Exception ex)
                    {
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }

        }

        public class PseudoEUChildClass : equipUtilTransposeWorkShiftWTDChildClass { }

        public ActionResult EquipUtilSummaryWorkShiftClass_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = new DataSourceResult();

            using (var appDB = new ProductionDashboardDBContext())
            {

                try
                {
                    IQueryable<equipUtilTransposeWorkShiftWTDChildClass> equipUtil = (from o in appDB.equipUtilWorkShiftWTDChildClass
                                                                                      where o.EquipClass == id
                                                                                      orderby o.Equipment
                                                                                      select new PseudoEUChildClass()
                                                                                      {
                                                                                          EquipClass = o.EquipClass,
                                                                                          EquipmentPackageFamily = o.EquipmentPackageFamily,
                                                                                          EquipmentFamily = o.EquipmentFamily,
                                                                                          Equipment = o.Equipment,
                                                                                          ParentVendorModel = o.ParentVendorModel,
                                                                                          WorkWeek = o.WorkWeek,
                                                                                          WorkShift = o.WorkShift,
                                                                                          Target = o.Target,
                                                                                          Saturday = o.Saturday / 12,
                                                                                          Sunday = o.Sunday / 12,
                                                                                          Monday = o.Monday / 12,
                                                                                          Tuesday = o.Tuesday / 12,
                                                                                          Wednesday = o.Wednesday / 12,
                                                                                          Thursday = o.Thursday / 12,
                                                                                          Friday = o.Friday / 12,
                                                                                          TotalHours = o.TotalHours / 84,
                                                                                      });

                    result = equipUtil.ToDataSourceResult(request);
                }
                catch (Exception ex)
                {
                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        public class PseudoEUChildGroup : equipUtilTransposeWorkShiftWTDChildGroup { }

        public ActionResult EquipUtilSummaryWorkShiftGroups_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = new DataSourceResult();

            using (var appDB = new ProductionDashboardDBContext())
            {

                try
                {
                    IQueryable<equipUtilTransposeWorkShiftWTDChildGroup> equipUtil = (from o in appDB.equipUtilWorkShiftWTDChildGroup
                                                                                      where o.EquipGroups == id
                                                                                      orderby o.Equipment
                                                                                      select new PseudoEUChildGroup()
                                                                                      {
                                                                                          EquipGroups = o.EquipGroups,
                                                                                          EquipmentPackageFamily = o.EquipmentPackageFamily,
                                                                                          EquipmentFamily = o.EquipmentFamily,
                                                                                          Equipment = o.Equipment,
                                                                                          ParentVendorModel = o.ParentVendorModel,
                                                                                          WorkWeek = o.WorkWeek,
                                                                                          WorkShift = o.WorkShift,
                                                                                          Target = o.Target,
                                                                                          Saturday = o.Saturday / 12,
                                                                                          Sunday = o.Sunday / 12,
                                                                                          Monday = o.Monday / 12,
                                                                                          Tuesday = o.Tuesday / 12,
                                                                                          Wednesday = o.Wednesday / 12,
                                                                                          Thursday = o.Thursday / 12,
                                                                                          Friday = o.Friday / 12,
                                                                                          TotalHours = o.TotalHours / 84,
                                                                                      });

                    result = equipUtil.ToDataSourceResult(request);
                }
                catch (Exception ex)
                {
                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult DashBoardWip_Read([DataSourceRequest]DataSourceRequest wipRequest, string classification, string groupings)
        {
            if (classification.Contains("Summary"))
            {

                var wipResult = (from a in pdDBContext.wipProductGroupNEW
                             where a.ProductGroup == groupings
                             select new
                             {                                 
                                 Operation = a.Operation,
                                 Qty = a.Qty,
                                 AsOf = a.AsOf

                             }).ToList();

            return Json(wipResult.ToDataSourceResult(wipRequest), JsonRequestBehavior.AllowGet);

            }

            else
            {

                var wipResult = (from a in pdDBContext.wipPackageGroup1NEW
                                 where a.PackageGroup1 == classification

                                 select new
                                 {    
                                     PackageGroup2 = a.PackageGroup2,
                                     PackageGroup3 = a.PackageGroup3, 
                                     Operation = a.Operation,
                                     Qty = a.Qty,
                                     AsOf = a.AsOf

                                 }).ToList();

                return Json(wipResult.ToDataSourceResult(wipRequest), JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult DashBoardInvParent_Read([DataSourceRequest]DataSourceRequest invRequest, string wipInv)
        {

            var invParentResult = (from a in pdDBContext.invParents
                             where a.ProductGroup == wipInv
                             select new
                             {

                                 Connector = a.Connector,
                                 ProductGroup = a.ProductGroup,
                                 PackageGroup1 = a.PackageGroup1,
                                 Qty = a.Qty,
                                 AsOf = a.AsOf

                             }).ToList();

            return Json(invParentResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashBoardInvChildren_Read([DataSourceRequest]DataSourceRequest invRequest, string wipParentConnector)
        {

            var invChidlrenResult = (from c in pdDBContext.invChildren
                                     where c.Connector == wipParentConnector
                                     select new 
                                     {
                                         GUID = Guid.NewGuid(),
                                         Connector  = c.Connector,  
                                         ProductGroup = c.ProductGroup,
                                         PackageGroup1 = c.PackageGroup1,
                                         PackageGroup2 = c.PackageGroup2,
                                         PackageGroup3 = c.PackageGroup3,
                                         Qty           = c.Qty
                                     }
                                    ).ToList();

            return Json(invChidlrenResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashBoardInv_Read([DataSourceRequest]DataSourceRequest invRequest, string wipInv)
        {

            //if (classification.Contains("Summary"))
            //{
            var invResult = (from a in pdDBContext.invClass
                             where a.Groupings == wipInv
                             select new
                             {

                                 Guid = Guid.NewGuid(),
                                 Classification = a.Classification,
                                 Operations = a.Operations,
                                 Qty = a.Qty,
                                 DateUpload = a.DateUpload

                             }).ToList();

            return Json(invResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var invResult = (from a in pdDBContext.invClass
            //                     where a.Classification == classification
            //                     select new
            //                     {

            //                         Guid = Guid.NewGuid(),
            //                         Operations = a.Operations,
            //                         Qty = a.Qty,
            //                         DateUpload = a.DateUpload

            //                     }).ToList();

            //    return Json(invResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
            //}

        }

        public ActionResult DashBoardInvStocksParent_Read([DataSourceRequest]DataSourceRequest invRequest, string wipInv)
        {

            var invStocksParentResult = (from a in pdDBContext.invStocksProductGroupParentNew
                                   where a.ProductGroup == wipInv
                                   select new
                                   {

                                       Connector = a.Connector,
                                       ProductGroup = a.ProductGroup,
                                       PackageGroup1 = a.PackageGroup1,
                                       Qty = a.Qty,
                                       AsOf = a.AsOf

                                   }).ToList();

            return Json(invStocksParentResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashBoardInvStocksChildren_Read([DataSourceRequest]DataSourceRequest invRequest, string invStocksParentConnector)
        {

            var invStocksChidlrenResult = (from c in pdDBContext.invStocksProductGroupChildNEW
                                           where c.Connector == invStocksParentConnector
                                           select new
                                           {
                                                GUID = Guid.NewGuid(),
                                                Connector = c.Connector,
                                                ProductGroup = c.ProductGroup,
                                                PackageGroup1 = c.PackageGroup1,
                                                PackageGroup2 = c.PackageGroup2,
                                                PackageGroup3 = c.PackageGroup3,
                                                Qty = c.Qty
                                           }
                                        ).ToList();

            return Json(invStocksChidlrenResult.ToDataSourceResult(invRequest), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashBoardWipAssy_Read([DataSourceRequest]DataSourceRequest wipAssyRequest) // , string wipAssy)
        {            
            var wipAssyResult = (from a in pdDBContext.wipAssyGroups
                                 group a by new
                                 {
                                     a.Operations,
                                     a.DateUpload
                                 } into b
                                 select new
                                 {
                                     Guid = Guid.NewGuid(),
                                     Operations = b.Key.Operations,
                                     Qty = b.Sum(a => a.Qty),
                                     DateUpload = b.Key.DateUpload
                                 }).ToList();

            return Json(wipAssyResult.ToDataSourceResult(wipAssyRequest), JsonRequestBehavior.AllowGet);          

        }

        public ActionResult GetRemarkID()
        {
            var currentYear = DateTime.Now.Year.ToString();

            var id = (from c in pdDBContext.Remarks
                      where c.TestOutputID.StartsWith(currentYear)
                      select new
                      {

                          ID = c.TestOutputID

                      }).ToList();


            return Json(id, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAssemblyID(string operation) 
        {

            var currentYear = DateTime.Now.Year.ToString();

            var id = (from c in pdDBContext.AssemblyRemarks
                      where c.AssemblyOutsID.StartsWith(currentYear) && c.Operation == operation
                      select new
                      {

                          ID = c.AssemblyOutsID

                      }).ToList();


            return Json(id, JsonRequestBehavior.AllowGet);
        
        }

        public ActionResult GetAssyComments(string id, string operation) 
        {

            var results = (from c in pdDBContext.AssemblyRemarks
                           where c.AssemblyOutsID == id && c.Operation == operation
                           select new
                           {

                               Remarks = c.Remarks

                           }).ToList();

            if (results.Count > 0)
            {
                results = results;

                return Json(results, JsonRequestBehavior.AllowGet);
            }

            else
            {

                var result2 = "null";

                return Json(result2, JsonRequestBehavior.AllowGet);
            }
        
        }

        public ActionResult AddAssyOutsComments1(AssemblyOutputRemarks assyRemarks)
        {

            var isExistAssyRemarks = (from assyRemarks1 in pdDBContext.AssemblyRemarks
                                      where assyRemarks1.AssemblyOutsID == assyRemarks.AssemblyOutsID
                                      select new
                                      {
                                          Remarks = assyRemarks1.Remarks

                                      }).ToList();



            if (ModelState.IsValid)
            {
                if (isExistAssyRemarks.Count == 0)
                {

                    AssemblyOutputRemarks addAssyRemarks = new AssemblyOutputRemarks();

                    addAssyRemarks.AssemblyOutsID = assyRemarks.AssemblyOutsID;
                    addAssyRemarks.Remarks = assyRemarks.Remarks;
                    addAssyRemarks.Operation = assyRemarks.Operation;
                    addAssyRemarks.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                    addAssyRemarks.DateCreated = DateTime.Now;
                    addAssyRemarks.ModifiedBy = null;
                    addAssyRemarks.DateModified = null;

                    pdDBContext.AssemblyRemarks.Attach(addAssyRemarks);
                    pdDBContext.AssemblyRemarks.Add(addAssyRemarks);
                    pdDBContext.SaveChanges();


                }

                else
                {

                    AssemblyOutputRemarks updateAssyRemarks = pdDBContext.AssemblyRemarks.FirstOrDefault(assy => assy.AssemblyOutsID == assyRemarks.AssemblyOutsID);

                    updateAssyRemarks.Remarks = assyRemarks.Remarks;
                    updateAssyRemarks.Operation = assyRemarks.Operation;
                    updateAssyRemarks.ModifiedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                    updateAssyRemarks.DateModified = DateTime.Now;

                    pdDBContext.Entry(updateAssyRemarks).State = System.Data.EntityState.Modified;
                    pdDBContext.SaveChanges();

                }



            }


            //if (addAssyRemarks == null)
            //{

            //}

            //else
            //{

            //   // return RedirectToAction("UpdateComments", details);
            //}


            return View();

        }

        public ActionResult GetComments(string id)
        {

            var results = (from c in pdDBContext.Remarks
                           where c.TestOutputID == id
                           select new
                           {

                               Remarks = c.Remarks

                           }).ToList();

            if (results.Count > 0)
            {
               // results = results;

                return Json(results, JsonRequestBehavior.AllowGet);
            }

            else
            {

                var result2 = "null";

                return Json(result2, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult AddTestOutComments(TestOutputRemarks details)
        {

          
            TestOutputRemarks saveComments = pdDBContext.Remarks.FirstOrDefault(d => d.TestOutputID == details.TestOutputID);

            //var chckRemarks = saveComments[0].Remarks;

            HistoricalRemarks addRemarksLogs = new HistoricalRemarks();

            if (details.Remarks != null)
            {

                addRemarksLogs.GUID = System.Guid.NewGuid().ToString().ToUpper();
                addRemarksLogs.Department = "TEST";
                addRemarksLogs.Operation = "MovesToFG";
                addRemarksLogs.ConnectorID = details.TestOutputID;
                addRemarksLogs.OriginalRemarks = details.Remarks;
                addRemarksLogs.UpdatedRemarks = null;
                addRemarksLogs.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                addRemarksLogs.DateAdded = DateTime.Now;

                pdDBContext.RemarksLogs.Attach(addRemarksLogs);
                pdDBContext.RemarksLogs.Add(addRemarksLogs);
                pdDBContext.SaveChanges();

            }

            if (saveComments == null)
            {
                if (ModelState.IsValid)
                {

                    details.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                    details.DateCreated = DateTime.Now;

                    pdDBContext.Remarks.Attach(details);
                    pdDBContext.Remarks.Add(details);
                    pdDBContext.SaveChanges();


                }


            }

            else
            {

                return RedirectToAction("UpdateComments", details);
            }


            return View();
        }

        public ActionResult AddAssyOutComments(AssemblyOutputRemarks detailsAssy)
        {


            AssemblyOutputRemarks saveAssyComments = pdDBContext.AssemblyRemarks.FirstOrDefault(d => d.AssemblyOutsID == detailsAssy.AssemblyOutsID && d.Operation == detailsAssy.Operation);


            if (saveAssyComments == null)
            {

                HistoricalRemarks addAssyRemarksLogs = new HistoricalRemarks();

                if (detailsAssy.Remarks != null)
                {

                    addAssyRemarksLogs.GUID = System.Guid.NewGuid().ToString().ToUpper();
                    addAssyRemarksLogs.Department = "ASSEMBLY";
                    addAssyRemarksLogs.Operation = detailsAssy.Operation;
                    addAssyRemarksLogs.ConnectorID = detailsAssy.AssemblyOutsID;
                    addAssyRemarksLogs.OriginalRemarks = detailsAssy.Remarks;
                    addAssyRemarksLogs.UpdatedRemarks = null;
                    addAssyRemarksLogs.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                    addAssyRemarksLogs.DateAdded = DateTime.Now;

                    pdDBContext.RemarksLogs.Attach(addAssyRemarksLogs);
                    pdDBContext.RemarksLogs.Add(addAssyRemarksLogs);
                    pdDBContext.SaveChanges();

                }
           


                detailsAssy.GUID = System.Guid.NewGuid().ToString().ToUpper();

                if (ModelState.IsValid)
                {

                    pdDBContext.AssemblyRemarks.Attach(detailsAssy);
                    pdDBContext.AssemblyRemarks.Add(detailsAssy);
                    pdDBContext.SaveChanges();


                }

                return Json("addSuccess", JsonRequestBehavior.AllowGet);
            }

            else
            {

                HistoricalRemarks addAssyUpdateRemarksLogs = new HistoricalRemarks();

                //HistoricalRemarks checkAssyUpdateRemarksLogs = pdDBContext.RemarksLogs.FirstOrDefault(d => d.ConnectorID == detailsAssy.AssemblyOutsID && d.Department == "ASSEMBLY");

                //if (checkAssyUpdateRemarksLogs == null)
                //{
                //    addAssyUpdateRemarksLogs.OriginalRemarks = saveAssyComments.Remarks;

                //}
                //else {

                //    addAssyUpdateRemarksLogs.OriginalRemarks = saveAssyComments.Remarks;
                //}

                

                if (detailsAssy.Remarks != null)
                {

             

                    addAssyUpdateRemarksLogs.GUID = System.Guid.NewGuid().ToString().ToUpper();
                    addAssyUpdateRemarksLogs.Department = "ASSEMBLY";
                    addAssyUpdateRemarksLogs.Operation = detailsAssy.Operation;
                    addAssyUpdateRemarksLogs.ConnectorID = detailsAssy.AssemblyOutsID;
                    addAssyUpdateRemarksLogs.UpdatedRemarks = detailsAssy.Remarks;
                    addAssyUpdateRemarksLogs.OriginalRemarks = saveAssyComments.Remarks;
                    addAssyUpdateRemarksLogs.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                    addAssyUpdateRemarksLogs.DateAdded = DateTime.Now;

                    pdDBContext.RemarksLogs.Attach(addAssyUpdateRemarksLogs);
                    pdDBContext.RemarksLogs.Add(addAssyUpdateRemarksLogs);
                    pdDBContext.SaveChanges();

                }
            

                saveAssyComments.Remarks = detailsAssy.Remarks;
                saveAssyComments.ModifiedBy = detailsAssy.CreatedBy;
                saveAssyComments.DateModified = DateTime.Now;
                saveAssyComments.Operation = detailsAssy.Operation;

                pdDBContext.Entry(saveAssyComments).State = System.Data.EntityState.Modified;
                pdDBContext.SaveChanges();

                return Json("updateSuccess", JsonRequestBehavior.AllowGet);
                
            }

            
           // return View();
        }
	 
        public ActionResult UpdateComments(TestOutputRemarks comments)
        {

            TestOutputRemarks updateComments = pdDBContext.Remarks.FirstOrDefault(c => c.TestOutputID == comments.TestOutputID);

            HistoricalRemarks addRemarksLogs = new HistoricalRemarks();

            if (comments.Remarks != null) {

                addRemarksLogs.GUID = System.Guid.NewGuid().ToString().ToUpper();
                addRemarksLogs.Department = "TEST";
                addRemarksLogs.Operation = "MovesToFG";
                addRemarksLogs.ConnectorID = comments.TestOutputID;
                addRemarksLogs.UpdatedRemarks = comments.Remarks;
                addRemarksLogs.OriginalRemarks = updateComments.Remarks;
                addRemarksLogs.CreatedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                addRemarksLogs.DateAdded = DateTime.Now;

                pdDBContext.RemarksLogs.Attach(addRemarksLogs);
                pdDBContext.RemarksLogs.Add(addRemarksLogs);
                pdDBContext.SaveChanges();    
            
            }
            

            if (updateComments != null)
            {
                updateComments.ModifiedBy = Common.GetWebCurrentUser(Common.WebUserInformation.Username);
                updateComments.DateModified = DateTime.Now;
                updateComments.Remarks = comments.Remarks;
               
            }

            using (var dbContext = new ProductionDashboardDBContext())
            {

                dbContext.Entry(updateComments).State = System.Data.EntityState.Modified;
                dbContext.SaveChanges();

            }

            return View();
        }

        public ActionResult GetUserAcess(string userAccount) 
        {

            var userAccess = (from ua in pdDBContext.Users
                              where ua.Account == userAccount
                              select new
                              {

                                  UserAcess = ua.UserAccess

                              }).FirstOrDefault();

            return Json(userAccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteComments(string ID) 
        {
            TestOutputRemarks delRemarks = pdDBContext.Remarks.Find(ID); //.Movies.Find(id);
            if (delRemarks == null)
            {
                return HttpNotFound();
            }

            pdDBContext.Remarks.Remove(delRemarks);
            pdDBContext.SaveChanges();

            var r = "success";
            
            return Json(r, JsonRequestBehavior.AllowGet);
        }
 
       
    }
}