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
    public class EmployeeLeaveRepository : IEmployeeRespository<EmployeeLeave>
    {
        private MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();
        public void Delete(object Id)
        {
            EmployeeLeave employeeLeave = db.EmployeeLeaves.Find(Id);
            db.EmployeeLeaves.Remove(employeeLeave);
            db.SaveChanges();
        }

        public IEnumerable<EmployeeLeave> GetAll()
        {
            return db.EmployeeLeaves.ToList();
        }

        public EmployeeLeave GetById(int Id)
        {
            return db.EmployeeLeaves.Find(Id);            
        }

        public IEnumerable<EmployeeLeave> GetAllById(int Id)
        {
            return db.EmployeeLeaves.Where(a=>a.Employee_Id ==Id && a.Status == false && a.Authorized == false);
        }

        public List<EmployeeLeave> GetEmployeeApprovedLeaves(int empId)
        {
            return db.EmployeeLeaves.Where(a => a.Employee_Id == empId
                                               && a.Authorized == true && a.Status == true).ToList();
        }
        public List<EmployeeLeave> GetEmployeePendingLeaves(int empId)
        {
           return  db.EmployeeLeaves.Where(a => a.Employee_Id == empId
                               && a.Authorized == false && a.Status == false).ToList();
        }
        public EmployeeLeave Insert(EmployeeLeave obj)
        {
            var employeeLeave = new EmployeeLeave();
            try
            {
                employeeLeave.Employee_Id = obj.Employee_Id;
                employeeLeave.LeaveStartDate = obj.LeaveStartDate;
                employeeLeave.LeaveEndDate = obj.LeaveEndDate;
                employeeLeave.TotalLeaveAuthorized = obj.TotalLeaveAuthorized;
                employeeLeave.DateCreated = DateTime.Now.Date.Add(new TimeSpan(0,00,0));
                employeeLeave.AdditionInfo = obj.AdditionInfo;
                employeeLeave.Authorized = false;
                employeeLeave.Status = false;
                
                db.EmployeeLeaves.Add(employeeLeave);
                db.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                foreach(var eve in ex.EntityValidationErrors)
                {
                    var error = eve.ValidationErrors;
                }
            }

            return obj;
        }

        public EmployeeLeave Update(EmployeeLeave obj)
        {

            EmployeeLeave leave = db.EmployeeLeaves.Where(a => a.EmployeeLeaveId == obj.EmployeeLeaveId).FirstOrDefault();

            leave.Authorized = obj.Authorized;
            leave.AuthorizedBy = obj.AuthorizedBy;
            leave.Status = true;
            leave.AuthorizedDate = DateTime.Now.Date.Add(new TimeSpan(0, 00, 0));

            db.Entry(leave).State = EntityState.Modified;
            db.SaveChanges();
            return obj;
        }
    }
}