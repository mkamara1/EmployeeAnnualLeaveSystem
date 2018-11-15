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
    public class AnnualLeaveRepository : IEmployeeRespository<AnnualLeave>
    {
        private MS_ShippingLimited_DevEntities db = new MS_ShippingLimited_DevEntities();
        public void Delete(object Id)
        {
            AnnualLeave annualLeave = db.AnnualLeaves.Find(Id);
            db.AnnualLeaves.Remove(annualLeave);
            db.SaveChanges();
        }

        public IEnumerable<AnnualLeave> GetAll()
        {
            return db.AnnualLeaves.ToList();
        }

        public AnnualLeave GetById(int Id)
        {
            return db.AnnualLeaves.Find(Id);
        }

        public IEnumerable<AnnualLeave> GetAllById(int Id)
        {
            return db.AnnualLeaves.Where(a=>a.LeaveID ==Id).ToList();

        }

        public AnnualLeave Insert(AnnualLeave obj)
        {
            var annualLeave = new  AnnualLeave();
            
            try
            {
                annualLeave.Employee_Id = obj.Employee_Id;
                annualLeave.EmployeeStartDate = obj.EmployeeStartDate;
                annualLeave.EmployeeManager_Id = obj.EmployeeManager_Id;
                annualLeave.TotalLeavePerYear = obj.TotalLeavePerYear;
                annualLeave.Year = obj.Year;
                annualLeave.DateCreated = DateTime.Now.Date.Add(new TimeSpan(0,00,0));


                db.AnnualLeaves.Add(annualLeave);
                db.SaveChanges();

            }
            catch(DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    var error = eve.ValidationErrors;
                }
            }
            return obj;
        }

        public AnnualLeave Update(AnnualLeave obj)
        {
            AnnualLeave annualLeave = db.AnnualLeaves.Where(a=>a.LeaveID == obj.LeaveID).FirstOrDefault();
            annualLeave.TotalLeavePerYear = obj.TotalLeavePerYear;
            annualLeave.Year = obj.Year;
            annualLeave.EmployeeStartDate = obj.EmployeeStartDate;
            annualLeave.EmployeeManager_Id = obj.EmployeeManager_Id;

            db.Entry(annualLeave).State = EntityState.Modified;
            return obj;
        }
    }
}