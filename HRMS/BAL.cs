using HRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS
{
    public class BAL
    {
        Models.HRMS db = new Models.HRMS();
        IList<EmployeeView> IListEmpView = new List<EmployeeView>();
        public IList<HrmEmployee> GetAllEmployee()
        {
           
                try
                {
                    IList<HrmEmployee> IList = new List<HrmEmployee>();
                    var emp = db.HrmEmployees.Where(x => x.FirstName != "").ToList();
                    foreach (var item in emp)
                    {
                        HrmEmployee ObjModel = new HrmEmployee();
                        ObjModel.Id = item.Id;
                        ObjModel.TitleId = item.TitleId;
                        ObjModel.FirstName = item.FirstName +" "+ item.LastName;
                        ObjModel.Middlename = item.Middlename;
                        ObjModel.LastName = item.LastName;
                        ObjModel.EmployeeCode = item.EmployeeCode;
                        ObjModel.Gender = item.Gender;
                        ObjModel.FatherHusbandName = item.FatherHusbandName;
                        ObjModel.DateOfBirth = item.DateOfBirth;
                        ObjModel.IdentityCardNo = item.IdentityCardNo;
                        ObjModel.IdentityExpiryDate = item.IdentityExpiryDate;
                        ObjModel.ReligionId = item.ReligionId;
                        ObjModel.MaritalStatus = item.MaritalStatus;
                        ObjModel.Dependants = item.Dependants;
                        ObjModel.NationalCountryId = item.NationalCountryId;
                        ObjModel.EthnicityId = item.EthnicityId;
                        ObjModel.BloodGroup = item.BloodGroup;
                        ObjModel.HrmLanguageId = item.HrmLanguageId;
                        ObjModel.DisabilitiesId = item.DisabilitiesId;
                        ObjModel.EmployeeType = item.EmployeeType;
                        ObjModel.EmployeeGroupId = item.EmployeeGroupId;
                        ObjModel.HrmLocationOficeId = item.HrmLocationOficeId;
                        ObjModel.DesignationId = item.DesignationId;
                        ObjModel.ReportToId = item.ReportToId;
                        ObjModel.DepartmentId = item.DepartmentId;
                        ObjModel.SubDepartmentId = item.SubDepartmentId;
                        ObjModel.HrmGradeId = item.HrmGradeId;
                        ObjModel.MachineId = item.MachineId;
                        ObjModel.JoiningDate = item.JoiningDate;
                        ObjModel.ProbationPeriod = item.ProbationPeriod;
                        ObjModel.ConfirmationDate = item.ConfirmationDate;
                        ObjModel.ContractExpiryDate = item.ContractExpiryDate;
                        ObjModel.BasicSalary = item.BasicSalary;
                        ObjModel.ContacNo = item.ContacNo;
                        ObjModel.Email = item.Email;
                        ObjModel.LivingCountryId = item.LivingCountryId;
                        ObjModel.StateId = item.StateId;
                        ObjModel.CityId = item.CityId;
                        ObjModel.ZipCode = item.ZipCode;
                        ObjModel.CurrentAddress = item.CurrentAddress;
                        ObjModel.CurrentCountryId = item.CurrentCountryId;
                        ObjModel.CurrentStateId = item.CurrentStateId;
                        ObjModel.CurrentCityId = item.CurrentCityId;
                        ObjModel.CurrentZipCode = item.CurrentZipCode;
                        ObjModel.UserName = item.UserName;
                        ObjModel.Password = item.Password;
                        ObjModel.IswebloginAllowed = item.IswebloginAllowed;
                        ObjModel.SoftRoleinformation = item.SoftRoleinformation;
                        ObjModel.ApplicationUserId = item.ApplicationUserId;
                        ObjModel.ProfilePicture = item.ProfilePicture;
                        ObjModel.CostCenterId = item.CostCenterId;
                        ObjModel.Active = item.Active;
                        ObjModel.CreatedBy = item.CreatedBy;
                        ObjModel.CreatedOn = item.CreatedOn;
                        ObjModel.LastModifiedBy = item.LastModifiedBy;
                        ObjModel.LastModifiedOn = item.LastModifiedOn;
                        ObjModel.IsDeleted = item.IsDeleted;
                        ObjModel.Country_Id = item.Country_Id;
                        ObjModel.BiometricCode = item.BiometricCode;
                        ObjModel.Title = item.Title;
                        ObjModel.Passport = item.Passport;
                        ObjModel.DrivingLicence = item.DrivingLicence;
                        ObjModel.BirthCountryId = item.BirthCountryId;
                        ObjModel.HrmLanguage = item.HrmLanguage;
                        ObjModel.ProbationType = item.ProbationType;
                        ObjModel.OfficialNo = item.OfficialNo;
                        ObjModel.OfficialEmail = item.OfficialEmail;
                        ObjModel.RegionId = item.RegionId;
                        ObjModel.PermnantAddress = item.PermnantAddress;
                        ObjModel.BankName = item.BankName;
                        ObjModel.BranchCode = item.BranchCode;
                        ObjModel.BranchName = item.BranchName;
                        ObjModel.AccountTitle = item.AccountTitle;
                        ObjModel.AccountNumber = item.AccountNumber;
                        ObjModel.AccountType = item.AccountType;
                        ObjModel.PassportExpiryDate = item.PassportExpiryDate;
                        IList.Add(ObjModel);
                    }
                    // var result = _hrms.HrmEmployees.Select(x => x).ToList();
                    var result = IList;
                    //var result = JsonConvert.SerializeObject(data);
                    return IList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
         
        }
        public IList<LeaveType> GetLeaveType()
        {
            List<LeaveType> ObjList = new List<LeaveType>();
            IList<LeaveType> ObjIList = new List<LeaveType>();
            ObjList = db.LeaveTypes.ToList();
            try
            {
                foreach (var item in ObjList)
                {
              
                    ObjIList.Add(new LeaveType
                    {
                        Id = Convert.ToInt32(item.Id),
                        Name = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjIList;
        }

        public IList<PublicHoliday> GetHoliDay()
        {
            List<PublicHoliday> ObjList = new List<PublicHoliday>();
            IList<PublicHoliday> ObjIList = new List<PublicHoliday>();
            ObjList = db.PublicHolidays.ToList();
            try
            {
                foreach (var item in ObjList)
                {
                    
                    ObjIList.Add(new PublicHoliday
                    {
                        Pk_PublicId = Convert.ToInt32(item.Pk_PublicId),
                        Name = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjIList;
        }

        public dynamic GetAttendanceBetwenDate(long EmpId, DateTime Date)
        {
        
            var GetDate = db.LeaveRequests.Where(x => x.Employee == EmpId && x.DateFrom >= Date && x.DateTo <= Date && x.Status == "Approved").ToList();

            return GetDate;
        }

        public dynamic GetAllEmployeeGroup()
        {
            var record = db.EmployeeGroups.Where(x => x.IsActive == true).ToList();
            return record;
        }

        public dynamic GetAllDepartment()
        {
            var record = db.Departments.Where(x => x.Active == true).ToList();
            return record;
        }

        public dynamic GetEmployeebyDepartment(long DepartmentId)
        {
            var record = db.HrmEmployees.Where(x => x.DepartmentId == DepartmentId && x.Active == true).ToList();
            return record;
        }

        public dynamic GetEmployeebyDepartmentName()
        {
            var record = (from s in db.HrmEmployees
                          join c in db.Departments on s.DepartmentId equals c.Id
                          where s.Active == true
                          select new
                          {
                              Id = s.Id,
                              EmployeeName = s.FirstName + " " + s.LastName,
                              DepartmentId = s.DepartmentId,
                              DepartmentName = c.Name,                             
                          }).ToList();

            foreach (var item in record)
            {
                EmployeeView Obj = new EmployeeView();
                Obj.Id = item.Id;
                Obj.FullName = item.EmployeeName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;

                IListEmpView.Add(Obj);
            }
            return IListEmpView;
        }
    }
}