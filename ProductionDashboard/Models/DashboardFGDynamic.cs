using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProductionDashboard.Models
{
    public class DashboardFGDynamic
    {
        [Key]
        public string GUID
        {
            get;
            set;
        }

        public int FYear
        {
            get;
            set;
        }

        public string FMonth
        {
            get;
            set;
        }

        public int WorkWeek
        {

            get;
            set;
        }

        public string WorkDayName
        {
            get;
            set;
        }

        public DateTime WorkDay
        {

            get;
            set;
        }

        public int Day
        {

            get;
            set;
        }

        public string Night
        {

            get;
            set;
        }

        public string WorkShift
        {

            get;
            set;
        }

        public int Qty
        {

            get;
            set;
        }

        public string Grouping
        {

            get;
            set;
        }


        public string Classification
        {

            get;
            set;
        }

        public DateTime DateCreated
        {
            get;
            set;
        }
    }
}