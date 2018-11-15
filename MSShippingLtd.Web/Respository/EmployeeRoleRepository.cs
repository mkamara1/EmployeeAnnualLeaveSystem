using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Interface;

namespace MSShippingLtd.Web.Respository
{
    public class EmployeeRoleRepository : IEmployeeRespository<EmployeeRole>
    {
        private MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();

        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeRole> GetAll()
        {
            return db.EmployeeRoles.ToList();
        }

        public IEnumerable<EmployeeRole> GetAllById(int Id)
        {
            return db.EmployeeRoles.Where(a=>a.EmployeeRoleID == Id).ToList();
        }

        public EmployeeRole GetById(int Id)
        {
            return db.EmployeeRoles.Find(Id);
        }

        public EmployeeRole Insert(EmployeeRole obj)
        {
            throw new NotImplementedException();
        }

        public EmployeeRole Update(EmployeeRole obj)
        {
            throw new NotImplementedException();
        }
    }
}