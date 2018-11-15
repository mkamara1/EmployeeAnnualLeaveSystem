using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSShippingLtd.Web.Interface;
using MSShippingLtd.Web.Respository;
using MSShippingLtd.Web.Models;
using System.Data.Entity.Validation;
using System.Data;

namespace MSShippingLtd.Web.Respository
{
    public class EmployeeAddressRepository : IEmployeeRespository<EmployeeAddress>
    {
        private MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();
        public void Delete(object obj)
        {
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Find(obj);
            db.EmployeeAddresses.Remove(employeeAddress);
        }

        public IEnumerable<EmployeeAddress> GetAll()
        {
            return db.EmployeeAddresses.ToList();
        }

        public EmployeeAddress GetById(int Id)
        {
            return db.EmployeeAddresses.Find(Id);
        }

        public IEnumerable<EmployeeAddress> GetAllById(int Id)
        {
            return db.EmployeeAddresses.Where(a=>a.ID == Id).ToList();
        }

        public EmployeeAddress Insert(EmployeeAddress obj)
        {
            try
            {
                db.EmployeeAddresses.Add(obj);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    var error = eve.ValidationErrors;
                }
            }


            return obj;
        }

        public EmployeeAddress Update(EmployeeAddress obj)
        {
            EmployeeAddress employeeAddress = db.EmployeeAddresses.Where(a=>a.ID == obj.ID).FirstOrDefault();
            employeeAddress.Address1 = obj.Address1;
            employeeAddress.Address2 = obj.Address2;
            employeeAddress.City = obj.City;
            employeeAddress.Country = obj.Country;
            employeeAddress.PostCode = obj.PostCode;

            db.Entry(employeeAddress).State = EntityState.Modified;
            db.SaveChanges();
            return obj;
        }
    }
}