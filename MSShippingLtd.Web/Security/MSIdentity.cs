using MSShippingLtd.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MSShippingLtd.Web.Security
{
    public class MSIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public Employee Employee { get; set; }

        public MSIdentity(Employee employee)
        {
            Identity = new GenericIdentity(employee.UserName);
            Employee = employee;
        }

        public string Name
        {
            get { return Identity.Name; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType;}
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }
    }
}