//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSShippingLtd.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeRole
    {
        public int EmployeeRoleID { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<int> Role_Id { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}