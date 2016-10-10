using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ProductionDashboard.Models;
using System.Data.Objects.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;
using System.Dynamic;


namespace ProductionDashboard.Controllers
{
    public static class DataTableExtensions
    {
        public static List<dynamic> ToDynamic(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }
    }

    public class HomeController : Controller
    {

        public ProductionDashboardDBContext pdDBContext = new ProductionDashboardDBContext();

        public ActionResult Index(string fYrMo, string groupings, string classification, string equipGroups, string equipClass, string opGroups, string opClass, string connector)
        {            
            fYrMo = "";
            groupings = "";
            classification = "";
            equipGroups = "";
            equipClass = "";
            opGroups = "";
            opClass = "";
            connector = "";

            //DataSet ds = new DataSet("dataSets");

            //ds.Clear();

            //DataTable dtTestOutput_parent = DashboardTestOutput_parent();
            //DataTable dtTestOutput_child = DashboardTestOutput_child(connector);
            //DataTable dtTestYield = DashboardTestYield();
            //DataTable dtAssyOutput3001 = DashboardAssyOutput3001();
            //DataTable dtAssyOutput3995 = DashboardAssyOutput3995();
            //DataTable dtAssyOutput6000 = DashboardAssyOutput6000();

            //DataTable dtEUPGroups = DashboardEUMonthPGroups(fYrMo, groupings);
            //DataTable dtEUPClass = DashboardEUMonthPClass(fYrMo, groupings, classification);

            //DataTable dtEUCGroups = DashboardEUMonthCGroups(fYrMo, groupings, connector);
            //DataTable dtEUCClass = DashboardEUMonthCClass(fYrMo, groupings, classification, connector);          

            //DataTable dtOutsPGroups = DashboardOutsMonthPGroups(fYrMo, groupings);
            //DataTable dtOutsCGroups = DashboardoutsMonthCGroups(fYrMo, groupings, opClass);

            //DataTable dtOutsPClass = DashboardOutsMonthPClass(fYrMo, groupings, classification);
            //DataTable dtOutsCClass = DashboardOutsMonthCClass(fYrMo, groupings, opClass);           

            //DataTable dtFgGroups = DashboardFgMonthGroups(fYrMo, groupings);           
            //DataTable dtFgClass = DashboardFgMonthClass(fYrMo, classification);

            //DataTable dtAssyWip = DashboardAssyWIP(groupings);
            

            //ds.Tables.Add(dtTestOutput_parent);            //0

            //ds.Tables.Add(dtEUPGroups);             //1                       
            //ds.Tables.Add(dtEUPClass);              //2

            //ds.Tables.Add(dtOutsPGroups);           //3           
            //ds.Tables.Add(dtOutsPClass);            //4
            

            //ds.Tables.Add(dtFgGroups);              //5           
            //ds.Tables.Add(dtFgClass);               //6            

            //ds.Tables.Add(dtEUCGroups);             //7           
            //ds.Tables.Add(dtEUCClass);              //8            

            //ds.Tables.Add(dtOutsCGroups);           //9           
            //ds.Tables.Add(dtOutsCClass);            //10

            //ds.Tables.Add(dtTestYield);             //11

            //ds.Tables.Add(dtAssyOutput3001);            //12
            //ds.Tables.Add(dtAssyOutput3995);            //13
            //ds.Tables.Add(dtAssyOutput6000);            //14

            //ds.Tables.Add(dtTestOutput_child);     //15

            //ds.Tables.Add(dtAssyWip);     //16

            // Treeview groupings         
            ViewData["testgroups"] = "GTS:SOHED:CURRENT SENSOR:IC:SEN-QFN:SENSOR-SIP";
            ViewData["assemblygroups"] = "GTS:SOHED:CURRENT SENSOR:IC:SEN-QFN:SENSOR-SIP";

            var wWeeks = (from a in pdDBContext.WorkWeeks
                          select a).ToList();

            string[] months = new string[] {"January", "February", "March", "April", "May", "June", "July", "August",
                                              "September",
                                              "October",
                                              "November",
                                              "December"};

            foreach(string month in months)
            {
                ViewData[month.Substring(0,3)] = wWeeks.Where(j => j.FISCALMONTHNAME == month).ToList();
            }
            
            ViewData["Year"] = wWeeks[0].FISCALMONTHINT.ToString();

            var testClass = (from a in pdDBContext.OutsTestLookUp
                              select a).ToList();

            ViewData["GTS"] = testClass.Where(j => j.ProductGroup == "GTS").ToList();
            ViewData["CURSEN"] = testClass.Where(j => j.ProductGroup == "CURSEN").ToList();
            ViewData["IC"] = testClass.Where(j => j.ProductGroup == "IC").ToList();
            ViewData["SEN-QFN"] = testClass.Where(j => j.ProductGroup == "SEN-QFN").ToList();
            ViewData["SENSOR SIP"] = testClass.Where(j => j.ProductGroup == "SENSOR SIP").ToList();
            ViewData["SOHED"] = testClass.Where(j => j.ProductGroup == "SOHED").ToList();
            ViewData["USB"] = testClass.Where(j => j.ProductGroup == "USB").ToList();

            var assyProductGroup = (from a in pdDBContext.OutsAssyLookUp
                                    select a).ToList();
            ViewData["ProductGroup"] = assyProductGroup.ToList();


            //var testDimension = (from a in pdDBContext.OutsTestLookUp_1
            //                 select a).ToList();

            //ViewData["QFN-NX"] = testDimension.Where(d => d.TestClassification == "QFN-NX").Where(d => d.Dimension != "").ToList();

            //return View(ds);
            return View();
        }              
   
        public ActionResult SpDashboardTestOutput_parent([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtOuts = DashboardTestOutput_parent(); //, classification);


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtOuts.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtOuts.ToDataSourceResult(request));

        }

        public ActionResult SpDashboardTestOutput_child([DataSourceRequest]DataSourceRequest request, string connector)
        {            
            DataTable dtOuts_child = DashboardTestOutput_child(connector);

            return Json(dtOuts_child.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpDashboardTestYield_parent([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtYieldP = DashboardTestYield_parent(); //, classification);


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtYieldP.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtYieldP.ToDataSourceResult(request));

        }

        public ActionResult SpDashboardTestYield_child([DataSourceRequest]DataSourceRequest request, string connector)
        {
            DataTable dtYieldC = DashboardTestYield_child(connector);

            return Json(dtYieldC.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpDashboardTestYield([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtYield = DashboardTestYield(); //, classification);


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtYield.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtYield.ToDataSourceResult(request));

        }

        public ActionResult SpDashboardAssyOutput3001([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtAssyOutput3001 = DashboardAssyOutput3001(); 


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtAssyOutput3001.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtAssyOutput3001.ToDataSourceResult(request));

        }

        public ActionResult SpDashboardAssyOutput3995([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtAssyOutput3995 = DashboardAssyOutput3995();


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtAssyOutput3995.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtAssyOutput3995.ToDataSourceResult(request));

        }

        public ActionResult SpDashboardAssyOutput6000([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtAssyOutput6000 = DashboardAssyOutput6000();


            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtAssyOutput6000.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtAssyOutput6000.ToDataSourceResult(request));

        }


        public ActionResult SpDashboardFGInventory([DataSourceRequest]DataSourceRequest request) 
        {

            DataTable dtFGInv = DashboardFGInv();

            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtFGInv.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtFGInv.ToDataSourceResult(request));
        
        }

        public ActionResult SpDashboardAsmbldGoodsInv([DataSourceRequest]DataSourceRequest request)
        {

            DataTable dtAsmbldGoodsInv = DashboardAsmbldGoodsInv();

            if (request.Aggregates.Any())
            {
                request.Aggregates.Each(a =>
                {

                    a.Aggregates.Each(aa =>
                    {
                        aa.MemberType = dtAsmbldGoodsInv.Columns[a.Member].DataType;
                    });

                });
            }

            return Json(dtAsmbldGoodsInv.ToDataSourceResult(request));

        }        

        public ActionResult SpDashboardAssyWIP([DataSourceRequest]DataSourceRequest request, string groupings)
        {
            DataTable dtAssyWip = DashboardAssyWIP(groupings);

            return Json(dtAssyWip.ToDataSourceResult(request));
        }
             

        /// <Action Result for Equipment Utilization Parent Groups Months>  

        public ActionResult SpDashboardEUMonthPGroups([DataSourceRequest]DataSourceRequest euMonthPGroups, string fYrMo, string groupings)
        {
            DataTable dtEUPGroups = DashboardEUMonthPGroups(fYrMo, groupings);

            return Json(dtEUPGroups.ToDataSourceResult(euMonthPGroups), JsonRequestBehavior.AllowGet);
        }


        /// <Action Result for Equipment Utilization Parent Class Months>  

        public ActionResult SpDashboardEUMonthPClass([DataSourceRequest]DataSourceRequest euMonthPClass, string fYrMo, string groupings, string classification)
        {
            DataTable dtEUPClass = DashboardEUMonthPClass(fYrMo, groupings, classification);
            
            return Json(dtEUPClass.ToDataSourceResult(euMonthPClass), JsonRequestBehavior.AllowGet);
        }      


        /// <Action Result for Equipment Utilization Child Groups Months>  

        public ActionResult SpDashboardEUMonthCGroups([DataSourceRequest]DataSourceRequest euMonthCGroups,string fYrMo, string groupings, string connector)
        {
            DataTable dtEUCGroups = DashboardEUMonthCGroups(fYrMo, groupings, connector);

            return Json(dtEUCGroups.ToDataSourceResult(euMonthCGroups), JsonRequestBehavior.AllowGet);
        }


        /// <Action Result for Equipment Utilization Child Class Months>  

        public ActionResult SpDashboardEUMonthCClass([DataSourceRequest]DataSourceRequest euMonthCClass, string fYrMo, string groupings, string classification, string connector)
        {
            DataTable dtEUCClass = DashboardEUMonthCClass(fYrMo, groupings, classification, connector);

            return Json(dtEUCClass.ToDataSourceResult(euMonthCClass), JsonRequestBehavior.AllowGet);
        }


        /// <Action Result for Outs per Operation Parent Groups Months>

        public ActionResult SpDashboardOutsMonthPGroups([DataSourceRequest]DataSourceRequest outsMonthPGroups, string fYrMo, string groupings)
        {
            DataTable dtOutsPGroups = DashboardOutsMonthPGroups(fYrMo, groupings);

            return Json(dtOutsPGroups.ToDataSourceResult(outsMonthPGroups));
        }      


        /// <Action Result for Outs per Operation Child Groups Months>

        public ActionResult SpDashboardoutsMonthCGroups([DataSourceRequest]DataSourceRequest outsMonthCGroups, string fYrMo, string groupings, string opClass)
        {
            DataTable dtOutsCGroups = DashboardoutsMonthCGroups(fYrMo, groupings, opClass);

            return Json(dtOutsCGroups.ToDataSourceResult(outsMonthCGroups));
        }
      


        /// <Action Result for Outs per Operation Parent Class Months>

        public ActionResult SpDashboardOutsMonthPClass([DataSourceRequest]DataSourceRequest outsMonthPClass, string fYrMo, string groupings, string classification)
        {
            DataTable dataTableOutsPClass = DashboardOutsMonthPClass(fYrMo, groupings, classification);

            return Json(dataTableOutsPClass.ToDataSourceResult(outsMonthPClass));
        }      


        /// <Action Result for Outs per Operation Child Class Months>

        public ActionResult SpDashboardOutsMonthCClass([DataSourceRequest]DataSourceRequest outsMonthCClass, string fYrMo, string groupings, string opClass)
        {
            DataTable dataTableOutsCClass = DashboardOutsMonthCClass(fYrMo, groupings, opClass);

            return Json(dataTableOutsCClass.ToDataSourceResult(outsMonthCClass));
        }    


        /// <Action Result for Finished Goods Groups Months>

        public ActionResult SpDashboardFgMonthGroups([DataSourceRequest]DataSourceRequest fgGroups, string fYrMo, string groupings)
        {
            DataTable dtFgGroups = DashboardFgMonthGroups(fYrMo, groupings);

            return Json(dtFgGroups.ToDataSourceResult(fgGroups));
        }
       

        /// <Action Result for Finished Goods Class Months>

        public ActionResult SpDashboardFgMonthClass([DataSourceRequest]DataSourceRequest fgClass, string fYrMo, string classification)
        {
            DataTable dtFgClass = DashboardFgMonthClass(fYrMo, classification);

            return Json(dtFgClass.ToDataSourceResult(fgClass));
        }


        /// <Action Result for Finished Goods Groups Weeks>

        public ActionResult SpDashboardFgWeekGroups([DataSourceRequest]DataSourceRequest fgWGroups, string fYrWk, string groupings)
        {
            DataTable dtFgWGroups = DashboardFgWeekGroups(fYrWk, groupings);

            return Json(dtFgWGroups.ToDataSourceResult(fgWGroups));
        }

       
        /// <Action Result for Finished Goods Class Weeks>

        public ActionResult SpDashboardFgWeekClass([DataSourceRequest]DataSourceRequest fgWClass, string fYrWk, string classification)
        {
            DataTable dtWFgClass = DashboardFgWeekClass(fYrWk, classification);

             return Json(dtWFgClass.ToDataSourceResult(fgWClass));
        }


        /// <Action Result for Outs per Operation ProductGroup Weeks>

        public ActionResult SpDashboardOutsPerOpsWeekGroups([DataSourceRequest]DataSourceRequest outsWGroups, string fYrWk, string groupings)
        {
            DataTable dtOutsWGroups = DashboardOutsPerOpsWeekGroups(fYrWk, groupings);

            return Json(dtOutsWGroups.ToDataSourceResult(outsWGroups));
        }

        public ActionResult SpDashboardOutsPerOpsWeekGroupsChild([DataSourceRequest]DataSourceRequest outsWGroupsChild, string fYrWk, string groupings, string opClass)
        {
            DataTable dtOutsWGroupsChild = DashboardOutsPerOpsWeekGroupsChild(fYrWk, groupings, opClass);

            return Json(dtOutsWGroupsChild.ToDataSourceResult(outsWGroupsChild));
        }


        /// <Action Result for Outs per Operation PackageGroup1 Weeks>

        public ActionResult SpDashboardOutsPerOpsWeekPackage([DataSourceRequest]DataSourceRequest outsWPackage, string fYrWk, string groupings, string classification)
        {
            DataTable dtOutsWPackage = DashboardOutsPerOpsWeekPackage(fYrWk, groupings, classification);

            return Json(dtOutsWPackage.ToDataSourceResult(outsWPackage));
        }

        public ActionResult SpDashboardOutsPerOpsWeekPackageChild([DataSourceRequest]DataSourceRequest outsWPackageChild, string fYrWk, string groupings, string opClass)
        {
            DataTable dtOutsWPackageChild = DashboardOutsPerOpsWeekPackageChild(fYrWk, groupings, opClass);

            return Json(dtOutsWPackageChild.ToDataSourceResult(outsWPackageChild));
        }


        /// <Action Result for Equipment Utilization PackageGroup1 Weeks>

        public ActionResult SpDashboardEquipmentUtilizationWeekPackage([DataSourceRequest]DataSourceRequest equipUtilWPackage, string fYrWk, string groupings, string classification)
        {
            DataTable dtEquipUtilWPackage = DashboardEquipmentUtilizationWeekPackage(fYrWk, groupings, classification);

            return Json(dtEquipUtilWPackage.ToDataSourceResult(equipUtilWPackage));
        }

        public ActionResult SpDashboardEquipmentUtilizationWeekPackageChild([DataSourceRequest]DataSourceRequest equipUtilWPackageChild, string fYrWk, string groupings, string classification, string connector)
        {
            DataTable dtEquipUtilWPackageChild = DashboardEquipmentUtilizationWeekPackageChild(fYrWk, groupings, classification, connector);

            return Json(dtEquipUtilWPackageChild.ToDataSourceResult(equipUtilWPackageChild));
        }


        /// <Action Result for Equipment Utilization ProductGroup Weeks>

        public ActionResult SpDashboardEquipmentUtilizationWeekProduct([DataSourceRequest]DataSourceRequest equipUtilWProduct, string fYrWk, string groupings)
        {
            DataTable dtEquipUtilWProduct = DashboardEquipmentUtilizationWeekProduct(fYrWk, groupings);

            return Json(dtEquipUtilWProduct.ToDataSourceResult(equipUtilWProduct));
        }

        public ActionResult SpDashboardEquipmentUtilizationWeekProductChild([DataSourceRequest]DataSourceRequest equipUtilWProductChild, string fYrWk, string groupings, string connector)
        {
            DataTable dtEquipUtilWProductChild = DashboardEquipmentUtilizationWeekProductChild(fYrWk, groupings, connector);

            return Json(dtEquipUtilWProductChild.ToDataSourceResult(equipUtilWProductChild));
        }


        private DataTable DashboardTestOutput_parent()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;            

            var dt = new DataTable();           
            dt.Clear();          

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [DashboardTESTOutputParentMonthly_Final]";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }                

                return dt;
            }
        }

        private DataTable DashboardTestOutput_child(string connector)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            dt.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [DashboardTESTOutputChildMonthly_Final] WHERE CONNECTOR = '" + connector + "'";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                return dt;
            }
        }


        private DataTable DashboardTestOutput_parent_OLD()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            //fMonth = "July";

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardTESTOutput_MonthlySummary_parentNEW";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardTestOutput_child_OLD(string connector)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;            

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardTESTOutput_MonthlySummary_childNEW";

                    cmd.Parameters.Add("@Connector", SqlDbType.NVarChar);
                    cmd.Parameters["@Connector"].Value = connector;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardTestYield()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;            

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardTESTYield_MonthlySummary";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardTestYield_parent()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DashboardTESTYieldParentMonthly_Final WHERE Connector is not null and PackageGroup1 <> 'SOIC'";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                return dt;
            }
        }

        private DataTable DashboardTestYield_child(string connector)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DashboardTESTYieldChildMonthly_Final WHERE CONNECTOR = '" + connector + "'";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                return dt;
            }
        }

        private DataTable DashboardAssyOutput3001()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();          

            dt.Clear();        

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DashboardAssemblyOuts3001_Final";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }               

                return dt;
            }
        }

        private DataTable DashboardAssyOutput3995()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DashboardAssemblyOuts3995_Final";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                return dt;
            }
        }

        private DataTable DashboardAssyOutput6000()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM DashboardAssemblyOuts6000_Final";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                return dt;
            }
        }

        private DataTable DashboardAssyOutput3001_OLD()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardAssemblyOuts3001_MonthlySummary";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardAssyOutput3995_OLD()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardAssemblyOuts3995_MonthlySummary";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardAssyOutput6000_OLD()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardAssemblyOuts6000_MonthlySummary";

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

                return dt2;
            }
        }

        private DataTable DashboardAssyWIP(string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "Dashboard_ASSEMBLYWIP";


                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());                       

                        conn.Close();
                    }
                }

            }
            return dt2;
        }

        private DataTable DashboardFGInv() 
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "Dashboard_Weekly_WIPINVSTOCKS";


                   // cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                   // cmd.Parameters["@ProductGroup"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        conn.Close();
                    }
                }

            }
            return dt2;
        
        }

        private DataTable DashboardAsmbldGoodsInv()
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "Dashboard_Weekly_WIPASMBLDGOODS";
                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        conn.Close();
                    }
                }

            }
            return dt2;

        }

        /// <DataTables for Equipment Utilization Parent Group Months>

        private DataTable DashboardEUMonthPGroups(string fYrMo, string groupings)
        {      
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;                      

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Monthly_ProductGroupparentNEW";                                                          

                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());                       

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName.Contains("WW"))
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 168;
                                }
                            }
                        }
                       
                        conn.Close();
                    }
                }

            }

            return dt2;
           
        }
       

        /// <DataTables for Equipment Utilization Child Groups Months>

        private DataTable DashboardEUMonthCGroups(string fYrMo, string groupings, string connector)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;            
            
            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Monthly_ProductGroupchildNEW";


                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@Connector", SqlDbType.NVarChar);
                    cmd.Parameters["@Connector"].Value = connector;          

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName.Contains("WW"))
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 84;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;
        }


        /// <DataTables for Equipment Utilization Parent Class Months>

        private DataTable DashboardEUMonthPClass(string fYrMo, string groupings, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Monthly_PackageGroup1parentNEW";                                       

                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName.Contains("WW"))
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 168;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;
        }


        /// <DataTables for Equipment Utilization Child Class Months>

        private DataTable DashboardEUMonthCClass(string fYrMo, string groupings, string classification, string connector)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Monthly_PackageGroup1childNEW";


                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup"].Value = classification;
                    cmd.Parameters.Add("@Connector", SqlDbType.NVarChar);
                    cmd.Parameters["@Connector"].Value = connector;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName.Contains("WW"))
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 84;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;
        }


        /// <DataTables for Outs per Operation Parent Groups Months>

        private DataTable DashboardOutsMonthPGroups(string fYrMo, string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardOutsbyOp_Monthly_ProductGroupParentNEW";                           

                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }


        /// <DataTables for Outs per Operation Child Groups Months>

        private DataTable DashboardoutsMonthCGroups(string fYrMo, string groupings, string opClass)
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;


            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[DashboardOutsbyOp_Monthly_ProductGroupChildNEW]";

                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@OpClass", SqlDbType.NVarChar);
                    cmd.Parameters["@OpClass"].Value = opClass;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;

        }       


        /// <DataTables for Outs per Operation Parent Class Months>

        private DataTable DashboardOutsMonthPClass(string fYrMo, string groupings, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;            

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 5000;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[DashboardOutsbyOp_Monthly_PackageGroup1ParentNEW]";                       

                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup1", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup1"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }

                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }     


        /// <DataTables for Outs per Operation Child Class Months>

        private DataTable DashboardOutsMonthCClass(string fYrMo, string groupings, string opClass)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           
            
            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[DashboardOutsbyOp_Monthly_PackageGroup1ChildNEW]";


                    cmd.Parameters.Add("@FYrMo", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrMo"].Value = fYrMo;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@OpClass", SqlDbType.NVarChar);
                    cmd.Parameters["@OpClass"].Value = opClass;        

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }
               

        /// <DataTables for Finished Goods Group Months>

        private DataTable DashboardFgMonthGroups(string fYrMo, string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardFG_MonthlySummary_ProductGroupNEW";                              

                    cmd.Parameters.Add("@FYearMonth", SqlDbType.NVarChar);
                    cmd.Parameters["@FYearMonth"].Value = fYrMo;
                    cmd.Parameters.Add("@Groupings", SqlDbType.NVarChar);
                    cmd.Parameters["@Groupings"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }     


        /// <DataTables for Finished Goods Class Months>

        private DataTable DashboardFgMonthClass(string fYrMo, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;           

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardFG_MonthlySummary_PackageGroup1NEW";

                    cmd.Parameters.Add("@FYearMonth", SqlDbType.NVarChar);
                    cmd.Parameters["@FYearMonth"].Value = fYrMo;
                    cmd.Parameters.Add("@Classification", SqlDbType.NVarChar);
                    cmd.Parameters["@Classification"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }



        /// <DataTables for Finished Goods Group Weeks>

        private DataTable DashboardFgWeekGroups(string fYrWk, string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardFG_WWSummary_ProductGroupNEW";

                    cmd.Parameters.Add("@FYrWeek", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWeek"].Value = fYrWk;
                    cmd.Parameters.Add("@Groupings", SqlDbType.NVarChar);
                    cmd.Parameters["@Groupings"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }     


        /// <DataTables for Finished Goods Class Weeks>

        private DataTable DashboardFgWeekClass(string fYrWk, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardFG_WWSummary_PackageGroup1NEW";

                    cmd.Parameters.Add("@FYrWeek", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWeek"].Value = fYrWk;
                    cmd.Parameters.Add("@Classification", SqlDbType.NVarChar);
                    cmd.Parameters["@Classification"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }


        /// <DataTables for Outs per Operation ProductGroup Parent Weeks>
        
        private DataTable DashboardOutsPerOpsWeekGroups(string fYrWk, string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardOutsbyOp_Daily_ProductGroupParentNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }

        /// <DataTables for Outs per Operation ProductGroup Child Weeks>
        
        private DataTable DashboardOutsPerOpsWeekGroupsChild(string fYrWk, string groupings, string opClass)
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;


            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[DashboardOutsbyOp_Daily_ProductGroupChildNEW]";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@OpClass", SqlDbType.NVarChar);
                    cmd.Parameters["@OpClass"].Value = opClass;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;

        }


        /// <DataTables for Outs per Operation PackageGroup1 Parent Weeks>

        private DataTable DashboardOutsPerOpsWeekPackage(string fYrWk, string groupings, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardOutsbyOp_Daily_PackageGroup1ParentNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup1", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup1"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;
        }

        /// <DataTables for Outs per Operation PackageGroup1 Child Weeks>

        private DataTable DashboardOutsPerOpsWeekPackageChild(string fYrWk, string groupings, string opClass)
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;


            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardOutsbyOp_Daily_PackageGroup1ChildNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@OpClass", SqlDbType.NVarChar);
                    cmd.Parameters["@OpClass"].Value = opClass;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }

            }
            return dt2;

        }


        /// <DataTables for Equipment Utilization PackageGroup1 Parent Weeks>

        private DataTable DashboardEquipmentUtilizationWeekPackage(string fYrWk, string groupings, string classification)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Daily_PackageGroup1parentNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup"].Value = classification;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName == "Saturday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Sunday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Monday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Tuesday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Wednesday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Thursday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Friday" && row[column.ColumnName].ToString() != "")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }                                
                                else if (column.ColumnName == "WTD" && row[column.ColumnName].ToString()!="")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 168;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;
        }

        /// <DataTables for Equipment Utilization PackageGroup1 Child Weeks>

        private DataTable DashboardEquipmentUtilizationWeekPackageChild(string fYrWk, string groupings, string classification, string connector)
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;


            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Daily_PackageGroup1childNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;
                    cmd.Parameters.Add("@PackageGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@PackageGroup"].Value = classification;
                    cmd.Parameters.Add("@Connector", SqlDbType.NVarChar);
                    cmd.Parameters["@Connector"].Value = connector;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName == "Saturday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Sunday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Monday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Tuesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Wednesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Thursday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Friday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "WTD")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 84;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;

        }


        /// <DataTables for Equipment Utilization ProductGroup Parent Weeks>

        private DataTable DashboardEquipmentUtilizationWeekProduct(string fYrWk, string groupings)
        {
            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;

            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Daily_ProductGroupparentNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;                   

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName == "Saturday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Sunday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Monday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Tuesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Wednesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Thursday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "Friday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 24;
                                }
                                else if (column.ColumnName == "WTD")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 168;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;
        }

        /// <DataTables for Equipment Utilization ProductGroup Child Weeks>

        private DataTable DashboardEquipmentUtilizationWeekProductChild(string fYrWk, string groupings, string connector)
        {

            var connStr = ConfigurationManager.ConnectionStrings["PDDBContextPROD"].ConnectionString;


            var dt = new DataTable();
            var dt2 = new DataTable();

            dt.Clear();
            dt2.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "DashboardEquipUtilization_Daily_ProductGroupchildNEW";

                    cmd.Parameters.Add("@FYrWk", SqlDbType.NVarChar);
                    cmd.Parameters["@FYrWk"].Value = fYrWk;
                    cmd.Parameters.Add("@ProductGroup", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductGroup"].Value = groupings;                   
                    cmd.Parameters.Add("@Connector", SqlDbType.NVarChar);
                    cmd.Parameters["@Connector"].Value = connector;

                    cmd.ExecuteNonQuery();

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }

                }


                if (dt.Rows[0][0].ToString().Trim() == "")
                {

                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string strSql = dt.Rows[0][0].ToString();

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select a.* into #temp from( " + strSql + " ) as a select * from #temp drop table #temp";

                        dt2.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dt2.Rows)
                        {
                            foreach (DataColumn column in dt2.Columns)
                            {
                                if (column.ColumnName == "Saturday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Sunday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Monday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Tuesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Wednesday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Thursday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "Friday")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 12;
                                }
                                else if (column.ColumnName == "WTD")
                                {
                                    row[column.ColumnName] = Convert.ToDecimal(row[column.ColumnName]) / 84;
                                }
                            }
                        }

                        conn.Close();
                    }
                }

            }
            return dt2;

        }       


    }
}