using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Interface;
using System.Data.Entity.Validation;
using System.Data;

namespace MSShippingLtd.Web.Respository
{
    public class EmployeeRepository : IEmployeeRespository<Employee>
    {
        private MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();

        public void Delete(object obj)
        {
            Employee employee = db.Employees.Find(obj);
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees.ToList();
        }

        public IEnumerable<Employee> GetAllById(int Id)
        {
            return db.Employees.Where(a => a.EmployeeId == Id).ToList();
        }

        public Employee GetById(int Id)
        {
            return db.Employees.Find(Id);
        }

        public Employee GetByUserName(string username)
        {
            return db.Employees.Where(a => a.UserName.ToLower() == username.ToLower()).FirstOrDefault();
        }

        public Employee Insert(Employee obj)
        {
            var employeeObj = new Employee();

            try
            {
                employeeObj.FirstName = obj.FirstName;
                employeeObj.LastName = obj.LastName;
                employeeObj.Email = obj.Email;
                employeeObj.UserName = obj.UserName;
                employeeObj.Telephone = obj.Telephone;

                db.Employees.Add(employeeObj);
                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    var error = eve.ValidationErrors;
                }
            }

            return employeeObj;
        }

        public Employee Update(Employee obj)
        {
            Employee employee = db.Employees.Where(a=>a.EmployeeId == obj.EmployeeId).FirstOrDefault();
            employee.FirstName = obj.FirstName;
            employee.LastName = obj.LastName;
            employee.Telephone = obj.Telephone;
            employee.Email = obj.Email;

            db.Entry(employee).State = EntityState.Modified;
            return obj;
        }
    }
}