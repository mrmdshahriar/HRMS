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
    
    public partial class HrmInterview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmInterview()
        {
            this.HrmInterviewDetails = new HashSet<HrmInterviewDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> HrmApplyFormId { get; set; }
        public Nullable<int> HrmJobPostId { get; set; }
        public string Result { get; set; }
        public Nullable<int> TotalScore { get; set; }
        public Nullable<int> TotalEarned { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> HrmEmployee_Id { get; set; }
    
        public virtual HrmEmployee HrmEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterviewDetail> HrmInterviewDetails { get; set; }
        public virtual HrmJobPost HrmJobPost { get; set; }
    }
}
