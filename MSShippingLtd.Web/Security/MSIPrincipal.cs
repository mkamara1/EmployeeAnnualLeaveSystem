using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MSShippingLtd.Web.Security
{
    public class MSIPrincipal : IPrincipal
    {
        private readonly MSIdentity MSIdentity;

        public MSIPrincipal(MSIdentity _mSIdentity)
        {
            MSIdentity = _mSIdentity;
        }

        public IIdentity Identity
        {
            get { return MSIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}