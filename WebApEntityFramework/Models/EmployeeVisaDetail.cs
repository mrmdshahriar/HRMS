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
    
    public partial class EmployeeVisaDetail
    {
        public long Id { get; set; }
        public string VisaTitle { get; set; }
        public Nullable<long> VisaNumber { get; set; }
        public Nullable<System.DateTime> VisaIssuanceDate { get; set; }
        public Nullable<System.DateTime> VisaExpiryDate { get; set; }
        public Nullable<long> LabourCardNumber { get; set; }
        public string LabourCardCode { get; set; }
        public Nullable<System.DateTime> LabourCardExpiry { get; set; }
        public Nullable<long> InsuranceCardNumber { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
