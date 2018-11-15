using MSShippingLtd.Web.Helper;
using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;


namespace MSShippingLtd.Web.Controllers

{
    public class EmployeeController : Controller
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private AnnualLeaveRepository _leaveRepository = new AnnualLeaveRepository();
        private EmployeeLeaveRepository _empLeaveRepository = new EmployeeLeaveRepository();
        private EmployeeRoleRepository roleRepository = new EmployeeRoleRepository();
       


        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnnualLeave()
        {
            EmployeeViewModel employeeViewModel = GetEmployeeDetails();

            return View(employeeViewModel);
        }

        private EmployeeViewModel GetEmployeeDetails()
        {
            MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();
            string roleName = AnnualLeaveHelper.EmployeeRoleName.Admin.ToString();


            var managers = (from em in db.Employees
                           join emRole in db.EmployeeRoles on em.EmployeeId equals emRole.Employee_Id
                           join rol in db.Roles on emRole.EmployeeRoleID equals rol.RoleID
                           where ( rol.RoleName == roleName)
                           select new { em.EmployeeId, em .UserName}
                           ).ToList();

            

            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.EmployeeList = new SelectList(employeeRepository.GetAll().ToList(), "EmployeeId", "UserName");
            employeeViewModel.EmployeeManagerList = new SelectList(managers.ToList(), "EmployeeId", "UserName");
            return employeeViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnnualLeave(EmployeeViewModel employeeViewModel)
        {
            EmployeeViewModel _employeeViewModel = GetEmployeeDetails();
            var annualLeaveObj = new AnnualLeave();

            if (ModelState.IsValid)
            {
                var employeeUsername = employeeViewModel.EmployeeUserName;
                var managerID = employeeViewModel.ManagerUserName;
                annualLeaveObj.EmployeeStartDate = employeeViewModel.StartDate;
                annualLeaveObj.Employee_Id = Convert.ToInt32(employeeUsername);
                var isUserAdmin = roleRepository.GetAll();
                annualLeaveObj.EmployeeManager_Id = Convert.ToInt32(managerID);

                annualLeaveObj.Year = employeeViewModel.Year;
                _leaveRepository.Insert(annualLeaveObj);

            }

            return View(_employeeViewModel);
        }
        public ActionResult EmployeeLeave()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeLeave(EmployeeViewModel employeeViewModel)
        {

            DateTime StartDate = Convert.ToDateTime(employeeViewModel.StartDate);
            DateTime EndDate = Convert.ToDateTime(employeeViewModel.EndDate);

            bool isStartAndEndDateGreater = false;
            bool isEndDateGreater = false;
            //check if start date is greater than today's date and if end date is greater than today's date 
            AnnualLeaveHelper.StartAndEndDateValidation(StartDate, EndDate, ref isStartAndEndDateGreater, ref isEndDateGreater);

            double leaveTotal = AnnualLeaveHelper.CalculateLeave(StartDate, EndDate, isStartAndEndDateGreater, isEndDateGreater);

            //check if user already booked those dates 
            int? empId = GetEmployeeId();
            var dateAlreadyExits = new HashSet<DateTime>();

            ValidateLeaveDate(StartDate, EndDate, empId, dateAlreadyExits);

            if (leaveTotal > 0 && dateAlreadyExits.Count() == 0)
            {

                if (empId != null)
                {
                    var _employeeLeave = new EmployeeLeave();
                    _employeeLeave.Employee_Id = empId;
                    _employeeLeave.LeaveStartDate = StartDate;
                    _employeeLeave.LeaveEndDate = EndDate;
                    _employeeLeave.TotalLeaveAuthorized = Convert.ToDecimal(leaveTotal);
                    _empLeaveRepository.Insert(_employeeLeave);

                    //Send email to manager and employee 
                    MailMessage msg = new MailMessage();

                }

            }
            else
            {
                //this date has already been requested for and has been approved 
            }

            return View();
        }

        private int? GetEmployeeId()
        {
            var username = HttpContext.User.Identity.Name;
            int? empId = employeeRepository.GetByUserName(username).EmployeeId;
            return empId;
        }

        public void ValidateLeaveDate(DateTime StartDate, DateTime EndDate, int? empId, HashSet<DateTime> dateAlreadyExits)
        {
            //Check date that date is not rquested already 
            IEnumerable<EmployeeLeave> leave = _empLeaveRepository.GetAllById(Convert.ToInt32(empId));

            foreach (EmployeeLeave item in leave)
            {

                for (DateTime i = Convert.ToDateTime(item.LeaveStartDate); i <= item.LeaveEndDate; i = i.AddDays(1))
                {
                    for (DateTime x = StartDate; x <= EndDate; x = x.AddDays(1))
                    {
                        if (i == x)
                        {
                            dateAlreadyExits.Add(i);
                        }
                    }

                }
            }
        }

        [Authorize]
        public ActionResult GetEmployeeLeave()
        {
            IEnumerable<EmployeeLeave> empLeave = _empLeaveRepository.GetAll();

            return View(empLeave);
        }
        [Authorize]
        public ActionResult Edit(int Id)
        {
            EmployeeLeave empLeave = _empLeaveRepository.GetById(Id);


            return View(empLeave);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeLeave employeeLeave)
        {
            if (ModelState.IsValid)
            {
                UpdateLeave(employeeLeave);
            }

            return View();
        }
        public ActionResult UpdateLeave(EmployeeLeave employeeLeave)
        {
            EmployeeLeave leaveObj = new EmployeeLeave();
            leaveObj.EmployeeLeaveId = employeeLeave.EmployeeLeaveId;
            leaveObj.Authorized = employeeLeave.Authorized;
            int? empId = GetEmployeeId();
            leaveObj.AuthorizedBy = Convert.ToInt32(empId);
            _empLeaveRepository.Update(leaveObj);

            return View();
        }
    }
}
