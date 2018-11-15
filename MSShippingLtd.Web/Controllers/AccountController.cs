using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Respository;
using MSShippingLtd.Web.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MSShippingLtd.Web.Controllers
{
    public class AccountController : Controller
    {

        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private EmployeeAddressRepository _employeeAddressRepository = new EmployeeAddressRepository();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {
            MSMembershipProvider mem = new MSMembershipProvider();

            if (ModelState.IsValid)
            {
                bool isValidUser = mem.ValidateUser(l.Username, l.Password);
                if (isValidUser)
                {
                    Employee employee = null;
                    using (MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities())
                    {
                        employee = db.Employees.Where(e => e.UserName.Equals(l.Username)).FirstOrDefault();
                    }

                    if (employee != null)
                    {
                        var js = new JavaScriptSerializer();
                        string data = js.Serialize(employee);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, employee.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), l.RememberMe, data);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authoCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authoCookie);
                        return Redirect(ReturnUrl);

                    }
                }
            }

            ModelState.Remove("Password");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Registration(Registration registration)
        {

            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.FirstName = registration.FirstName;
                employee.LastName = registration.LastName;
                employee.Email = registration.LastName;
                employee.UserName = registration.UserName;
                employee.Telephone = registration.Telephone;

                _employeeRepository.Insert(employee);

                EmployeeAddress employeeAddress = new EmployeeAddress();
                employeeAddress.Address1 = registration.Address1;
                employeeAddress.Address2 = registration.Address2;
                employeeAddress.City = registration.City;
                employeeAddress.Country = registration.Country;
                employeeAddress.PostCode = registration.PostCode;
                _employeeAddressRepository.Insert(employeeAddress);
            }

            return View();
        }
        [Authorize(Roles = "Employee")]
        public ActionResult UserIndex()
        {

            return View();
        }
    }
}