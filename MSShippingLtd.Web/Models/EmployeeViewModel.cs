using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSShippingLtd.Web.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<int> Manager_Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AdditionInfo { get; set; }
        public Nullable<decimal> TotalLeavePerYear { get; set; }
        public string Year { get; set; }
        public string EmployeeUserName { get; set; }
        public SelectList EmployeeList { get; set; }

        public string ManagerUserName { get; set; }
        public SelectList EmployeeManagerList { get; set; }
        public double totalApprovedLeave { get; set; }
        public double totalPendingLeave { get; set; }
        public double remainingLeave { get; set; }

        public IEnumerable<EmployeeLeave> ApprovedLeavesList { get; set; }
        public IEnumerable<EmployeeLeave> PendingLeaveList { get; set; }


    }
}