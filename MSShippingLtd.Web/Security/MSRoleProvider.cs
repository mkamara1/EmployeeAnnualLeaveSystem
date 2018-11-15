using MSShippingLtd.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace MSShippingLtd.Web.Security
{
    public class MSRoleProvider : RoleProvider
    {
        private int _cacheTimeOutInMinutes = 20;
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            //check cache 

            var cacheKey = string.Format("{0}", username);
            if(HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[])HttpRuntime.Cache[cacheKey];
            }

            string[] roles = new string[] { };
            using (MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities())
            {
                 roles = (from a in db.Roles
                             join b in db.EmployeeRoles on a.RoleID equals b.Role_Id
                             join c in db.Employees on b.Employee_Id equals c.EmployeeId
                             where c.UserName.Equals(username)
                             select a.RoleName).ToArray<string>();

                if(roles.Count() > 0)
                {
                    HttpRuntime.Cache.Insert(cacheKey,roles, null,DateTime.Now.AddMinutes(_cacheTimeOutInMinutes),Cache.NoSlidingExpiration);

                }
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}