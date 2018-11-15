using MSShippingLtd.Web.Helper;
using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MSShippingLtd.Web.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeLeaveRepository leaveRepository = new EmployeeLeaveRepository();
        private AnnualLeaveRepository annualLeaveRepository = new AnnualLeaveRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Myprofile()
        {

            EmployeeViewModel viewModel = new EmployeeViewModel();
            AnnualLeave getEmpAnnaulLeave;
            double approvedLeaveTotal, pendingLeaveTotal, remainingLeave;
            List<EmployeeLeave> leaveApproved, leavePending;
            GetLeaveSummary(out getEmpAnnaulLeave, out approvedLeaveTotal, out pendingLeaveTotal, out leaveApproved, out leavePending, out remainingLeave);

            // employee annual leave summary 
            viewModel.TotalLeavePerYear = getEmpAnnaulLeave.TotalLeavePerYear;
            viewModel.totalApprovedLeave = approvedLeaveTotal;
            viewModel.totalPendingLeave = pendingLeaveTotal;
            viewModel.remainingLeave = remainingLeave;
            viewModel.ApprovedLeavesList = leaveApproved;
            viewModel.PendingLeaveList = leavePending;


            return View(viewModel);
        }

        private void GetLeaveSummary(out AnnualLeave getEmpAnnaulLeave, out double approvedLeaveTotal, out double pendingLeaveTotal, out List<EmployeeLeave> leaveApproved, out List<EmployeeLeave> leavePending, out double remainingLeave)
        {
            var loginUsername = HttpContext.User.Identity.Name;
            var empId = employeeRepository.GetByUserName(loginUsername).EmployeeId;

            //total year for year 
            getEmpAnnaulLeave = annualLeaveRepository.GetById(empId);

            //approve leave total 

            approvedLeaveTotal = 0;
            pendingLeaveTotal = 0;
            leaveApproved = leaveRepository.GetEmployeeApprovedLeaves(empId);


            foreach (var item in leaveApproved)
            {
                DateTime startdate = Convert.ToDateTime(item.LeaveStartDate);
                DateTime endDate = Convert.ToDateTime(item.LeaveEndDate);
                approvedLeaveTotal += AnnualLeaveHelper.CalculateLeave(startdate, endDate, true, true);
            }


            leavePending = leaveRepository.GetEmployeePendingLeaves(empId);
            foreach (var item in leavePending)
            {
                DateTime startdate = Convert.ToDateTime(item.LeaveStartDate);
                DateTime endDate = Convert.ToDateTime(item.LeaveEndDate);
                pendingLeaveTotal += AnnualLeaveHelper.CalculateLeave(startdate, endDate, true, true);
            }

            remainingLeave = Convert.ToDouble(getEmpAnnaulLeave.TotalLeavePerYear) - (Convert.ToDouble(pendingLeaveTotal) + Convert.ToDouble(approvedLeaveTotal));
        }

        public ActionResult LeaveSummary()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}