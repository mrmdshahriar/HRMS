//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApEntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HrmEmployeeRefrence
    {
        public long ReferenceId { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<int> ReferenceType { get; set; }
        public string FullName { get; set; }
        public Nullable<long> Designation { get; set; }
        public string Organization { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PeriodOfKowing { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
