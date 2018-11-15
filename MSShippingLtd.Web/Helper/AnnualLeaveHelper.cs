using MSShippingLtd.Web.Models;
using MSShippingLtd.Web.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSShippingLtd.Web.Helper
{
    public class AnnualLeaveHelper
    {
        public enum EmployeeRoleName
        {
            Admin,
            Employee
        }
        private EmployeeLeaveRepository _empLeaveRepository = new EmployeeLeaveRepository();

        public static void StartAndEndDateValidation(DateTime StartDate, DateTime EndDate, ref bool isStartAndEndDateGreater, ref bool isEndDateGreater)
        {
            DateTime currentDateTIme = DateTime.Now.Date.Add(new TimeSpan(0, 00, 0));

            if (StartDate > currentDateTIme && EndDate > currentDateTIme)
            {
                isStartAndEndDateGreater = true;

                //check if end date is greater than start date 
                if (EndDate >= StartDate)
                {
                    isEndDateGreater = true;
                }
            }

        }
        public static double CalculateLeave(DateTime StartDate, DateTime EndDate, bool isStartAndEndDateGreater, bool isEndDateGreater)
        {
            //loop the two date to get the number of days requested for
            double leaveTotal = 0;

            if (isStartAndEndDateGreater && isEndDateGreater)
            {
                for (DateTime i = StartDate; i <= EndDate; i = i.AddDays(1))
                {
                    if (i.DayOfWeek != DayOfWeek.Saturday || i.DayOfWeek != DayOfWeek.Sunday)
                    {
                        leaveTotal++;
                    }

                }
            }
            else if (isStartAndEndDateGreater && !isEndDateGreater)
            {
                //please check your end date 
            }
            else if (!isStartAndEndDateGreater && isEndDateGreater)
            {
                //please check you start date 
            }
            else if (!isStartAndEndDateGreater && !isEndDateGreater)
            {
                //please check both date to ensure they are greater than today's date 
            }

            return leaveTotal;
        }


    }
}