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
    
    public partial class HrmEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmEmployee()
        {
            this.Departments = new HashSet<Department>();
            this.HrmEmployees1 = new HashSet<HrmEmployee>();
            this.HrmInternalRequisitions = new HashSet<HrmInternalRequisition>();
            this.HrmInternalRequisitions1 = new HashSet<HrmInternalRequisition>();
            this.HrmInterviewDetails = new HashSet<HrmInterviewDetail>();
            this.HrmInterviews = new HashSet<HrmInterview>();
            this.HrmJobPosts = new HashSet<HrmJobPost>();
            this.KeyResults = new HashSet<KeyResult>();
        }
    
        public int Id { get; set; }
        public Nullable<int> TitleId { get; set; }
        public string FirstName { get; set; }
        public string Middlename { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public string Gender { get; set; }
        public string FatherHusbandName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string IdentityCardNo { get; set; }
        public Nullable<System.DateTime> IdentityExpiryDate { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<int> Dependants { get; set; }
        public Nullable<int> NationalCountryId { get; set; }
        public Nullable<int> EthnicityId { get; set; }
        public string BloodGroup { get; set; }
        public Nullable<int> HrmLanguageId { get; set; }
        public Nullable<int> DisabilitiesId { get; set; }
        public string EmployeeType { get; set; }
        public Nullable<int> EmployeeGroupId { get; set; }
        public Nullable<int> HrmLocationOficeId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> ReportToId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> SubDepartmentId { get; set; }
        public Nullable<int> HrmGradeId { get; set; }
        public string MachineId { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<int> ProbationPeriod { get; set; }
        public Nullable<System.DateTime> ConfirmationDate { get; set; }
        public Nullable<System.DateTime> ContractExpiryDate { get; set; }
        public Nullable<double> BasicSalary { get; set; }
        public string ContacNo { get; set; }
        public string Email { get; set; }
        public Nullable<int> LivingCountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string ZipCode { get; set; }
        public string CurrentAddress { get; set; }
        public Nullable<int> CurrentCountryId { get; set; }
        public Nullable<int> CurrentStateId { get; set; }
        public Nullable<int> CurrentCityId { get; set; }
        public string CurrentZipCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IswebloginAllowed { get; set; }
        public string SoftRoleinformation { get; set; }
        public string ApplicationUserId { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> CostCenterId { get; set; }
        public Nullable<bool> Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public string BiometricCode { get; set; }
        public string Title { get; set; }
        public string Passport { get; set; }
        public string DrivingLicence { get; set; }
        public Nullable<int> BirthCountryId { get; set; }
        public string HrmLanguage { get; set; }
        public string ProbationType { get; set; }
        public string OfficialNo { get; set; }
        public string OfficialEmail { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string PermnantAddress { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public Nullable<System.DateTime> PassportExpiryDate { get; set; }
        public Nullable<long> ShiftId { get; set; }
    
        public virtual AspNetRole AspNetRole { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
        public virtual Country Country { get; set; }
        public virtual Country Country1 { get; set; }
        public virtual Country Country2 { get; set; }
        public virtual Country Country3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments { get; set; }
        public virtual Department Department { get; set; }
        public virtual Department Department1 { get; set; }
        public virtual Designation Designation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }
        public virtual HrmEmployee HrmEmployee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterviewDetail> HrmInterviewDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterview> HrmInterviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KeyResult> KeyResults { get; set; }
    }
}
