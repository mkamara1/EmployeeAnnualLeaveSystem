using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MSShippingLtd.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authoCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if(authoCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                JavaScriptSerializer js = new JavaScriptSerializer();
                Employee employee = js.Deserialize<Employee>(ticket.UserData);
               // MSIdentity mSIIdentity = new MSIdentity(employee);
                //MSIPrincipal mSIPrincipal = new MSIPrincipal(mSIIdentity);
                //HttpContext.Current.User = mSIPrincipal;
            }
        }
    }
}
