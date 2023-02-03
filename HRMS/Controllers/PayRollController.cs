﻿using HRMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HRMS.Controllers
{
    public class PayRollController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        // GET: PayRoll
        public ActionResult Index()
        {
            return View();
        }

        #region Allowance Type

        public ActionResult AllowanceType()
        {
            return View();
        }

        public ActionResult AllowanceTypeList()
        {
            try
            {
                var Result = _hrms.AllowanceTypes.Select(s => s).ToList();

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult EditAllowanceType(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.AllowanceTypes.Where(x => x.Id == id).FirstOrDefault<AllowanceType>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddAllowanceType(AllowanceType obj)
        {
            _hrms.AllowanceTypes.Add(obj);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult UpdateAllowanceType(AllowanceType obj)
        {
            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteAllowanceType(int id)
        {

            AllowanceType allowanceType = _hrms.AllowanceTypes.Where(x => x.Id == id).FirstOrDefault<AllowanceType>();
            _hrms.AllowanceTypes.Remove(allowanceType);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

        }

        #endregion Allowance Type End

        #region Allowance

        public ActionResult Allowance()
        {
            return View();
        }

        public ActionResult AllowanceList()
        {
            try
            {
                var allowance = _hrms.Allowances.Select(s => s).ToList();
                var allowanceType = _hrms.AllowanceTypes.Select(s => s).ToList();
                var Result = from st in allowance
                             join d in allowanceType on st.FK_AllowanceTypeId equals d.Id into table1
                             from d in table1.ToList()
                             select new
                             {
                                 Id = st.Id,
                                 Name = st.Name,
                                 AllowanceTypeName =d.Name, 
                                 AllowanceTypeId =d.Id, 
                                 Active = st.IsActive,
 
                             };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult EditAllowance(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.Allowances.Where(x => x.Id == id).FirstOrDefault<Allowance>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddAllowance(Allowance obj)
        {
            _hrms.Allowances.Add(obj);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult UpdateAllowance(Allowance obj)
        {
            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteAllowance(int id)
        {

            Allowance allowance = _hrms.Allowances.Where(x => x.Id == id).FirstOrDefault<Allowance>();
            _hrms.Allowances.Remove(allowance);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

        }

        #endregion Allowance


        public ActionResult AllowancesDeduction()
        {
            return View();
        }


        public ActionResult AllowancesDeductionList()
        {
            try
            {
                var allowance = _hrms.AllowancesDeductions.Select(s => s).ToList();
                var allowanceType = _hrms.Allowances.Select(s => s).ToList();
                var Result = from st in allowance
                             join d in allowanceType on st.FK_AllowanceId equals d.Id into table1
                             from d in table1.ToList()
                             select new
                             {
                                 Id = st.Id,
                                 Code = st.Code,
                                 AllowanceName = d.Name,
                                 AllowanceId = d.Id,
                                 Active = st.IsActive,
                                 Effect=st.Effect,
                                 Percentage = st.Percentage,
                                 Amount = st.Amount,
                                 GLCode = st.GLCode

                             };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult EditAllowancesDeduction(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.AllowancesDeductions.Where(x => x.Id == id).FirstOrDefault<AllowancesDeduction>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult AddAllowancesDeduction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAllowancesDeduction(AllowancesDeduction obj)
        {
            _hrms.AllowancesDeductions.Add(obj);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult UpdateAllowancesDeduction(AllowancesDeduction obj)
        {
            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteAllowancesDeduction(int id)
        {

            AllowancesDeduction allowance = _hrms.AllowancesDeductions.Where(x => x.Id == id).FirstOrDefault<AllowancesDeduction>();
            _hrms.AllowancesDeductions.Remove(allowance);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

        }



        public ActionResult Allowanceddl()
        {
            // var data = _hrms.HrmEmployees.Where(x => x.Active == true).ToList();

            var dd = _hrms.Allowances.Where(x => x.IsActive == true).FirstOrDefault();
            List<AllowancesDeduction> empList = _hrms.AllowancesDeductions.ToList<AllowancesDeduction>();


            var data = (from cs in _hrms.AllowancesDeductions
                        join emp in _hrms.Allowances on cs.FK_AllowanceId equals emp.Id
                       

                        select new
                        {
                            Id = cs.Id,
                            Name = emp.Name,
                            Amount =cs.Amount
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        #region OverTimeSetups Start

        public ActionResult OverTimeSetup()
        {
            return View();
        }
        //public ActionResult AutoCodeGenrate()
        //{
        //    var stringCode = string.Empty;
        //    _hrms.Configuration.ProxyCreationEnabled = false;
        //    //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
        //    var LastCode = _hrms.Regions.Where(x => x.Active == true).ToList().LastOrDefault();
        //    if (LastCode != null && !string.IsNullOrEmpty(LastCode.Code))

        //    {
        //        stringCode = LastCode.Code.Substring(0, 3);
        //        int intCode = Convert.ToInt16(LastCode.Code.Substring(3));
        //        intCode++;
        //        var threeDidgit = intCode.ToString("D2"); // = "001"
        //        stringCode += threeDidgit;
        //    }
        //    else
        //    {
        //        stringCode = "Rg-" + 1.ToString("D2");
        //    }
        //    var Code = stringCode;
        //    return Json(Code, JsonRequestBehavior.AllowGet);
        // return View(Code);
        //Common employeeDropDowns = new Common
        //{

        //    Code = stringCode
        //};


        //return View(employeeDropDowns);
        // }

        public ActionResult GetOverTimeSetupsList()
        {
            try
            {


                var dd = _hrms.OverTimeSetups.Where(x => x.Active == true).FirstOrDefault();
                List<OverTimeSetup> OverTimeSetupList = _hrms.OverTimeSetups.ToList<OverTimeSetup>();
                //var result = OverTimeSetupList.Select(S => new
                //{
                //    Id = S.Id,
                //    Name = S.Name,
                //    Code = S.Code,
                //    Active = S.Active
                //});
                var result = _hrms.OverTimeSetups.Where(x => x.Active == true).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditOverTimeSetup(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.OverTimeSetups.Where(x => x.Id == id).FirstOrDefault<OverTimeSetup>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddOverTimeSetup(OverTimeSetup obj)
        {
            try
            {


                bool IsrecExisit = _hrms.OverTimeSetups.Any(x => x.Id == obj.Id);
                if (IsrecExisit != true)
                {

                    _hrms.OverTimeSetups.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateOverTimeSetup(OverTimeSetup obj)
        {

            try
            {

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteOverTimeSetup(int id)
        {
            try
            {
                OverTimeSetup rg = _hrms.OverTimeSetups.Where(x => x.Id == id).FirstOrDefault<OverTimeSetup>();
                _hrms.OverTimeSetups.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion OverTimeSetups


        #region EmployeeCost Start


         
        public ActionResult EmployeeCost()
        {
            return View();
        }

        //public ActionResult AutoCodeDeduction()
        //{
        //    var stringCode = string.Empty;
        //    _hrms.Configuration.ProxyCreationEnabled = false;
        //    //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
        //    var LastCode = _hrms.Deductions.Where(x => x.Active == true).ToList().LastOrDefault();
        //    if (LastCode != null && !string.IsNullOrEmpty(LastCode.Code))

        //    {
        //        stringCode = LastCode.Code.Substring(0, 3);
        //        int intCode = Convert.ToInt16(LastCode.Code.Substring(3));
        //        intCode++;
        //        var threeDidgit = intCode.ToString("D2"); // = "001"
        //        stringCode += threeDidgit;
        //    }
        //    else
        //    {
        //        stringCode = "Rg-" + 1.ToString("D2");
        //    }
        //    var Code = stringCode;
        //    return Json(Code, JsonRequestBehavior.AllowGet);
        //    // return View(Code);
        //    //Common employeeDropDowns = new Common
        //    //{

        //    //    Code = stringCode
        //    //};


        //    //return View(employeeDropDowns);
        //}

        public ActionResult GetEmployeeCostList()
        {
            try
            {
                var data = (from cs in _hrms.EmployeeCosts
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id into empGroup
                            from empg in empGroup.DefaultIfEmpty()
                            join csTb in _hrms.CostingTabs on cs.CostingTabsId equals csTb.Id into csTbGroup
                            from csTbg in csTbGroup.DefaultIfEmpty()
                            where empg.Active == true && csTbg.Active == true
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeName = empg.FirstName + " " + empg.LastName,
                                CostingTabsId = cs.CostingTabsId,
                                CostingTabsName = csTbg.Name,
                                TotalCost = cs.TotalCost,
                                Active = cs.Active
                            }).Where(x => x.Active == true).ToList();

                //var dd = _hrms.CostingTabs.Where(x => x.Active == true).FirstOrDefault();
                //List<CostingTab> CostList = _hrms.CostingTabs.ToList<CostingTab>();
                //var result = CostList.Select(S => new
                //{
                //    Id = S.Id,
                //    Name = S.Name,
                //    Code = S.Code,
                //    Active = S.Active
                //});
              
                //var result = _hrms.EmployeeCosts.Where(x => x.Active == true).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult CostingTabsDdl()
        {

            List<CostingTab> costingTabList = _hrms.CostingTabs.Where(x => x.Active == true).ToList();

            var result = costingTabList.Select(S => new
            {
                Id = S.Id,
                Name = S.Name

            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult EditEmployeeCost(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.EmployeeCosts.Where(x => x.Id == id).FirstOrDefault<EmployeeCost>();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEmployeeCost(EmployeeCost obj)
        {
            try
            {
                _hrms.EmployeeCosts.Add(obj);

                _hrms.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateEmployeeCost(EmployeeCost obj)
        {

            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
                //recrod.EmployeeId = obj.EmployeeId;

                 _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteEmployeeCost(int id)
        {
            try
            {
                EmployeeCost rg = _hrms.EmployeeCosts.Where(x => x.Id == id).FirstOrDefault<EmployeeCost>();
                _hrms.EmployeeCosts.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion EmployeeCost


        #region BonusSetup
       



        public ActionResult BonusSetup()
        {
            return View();
        }

        
        public ActionResult GetBonusSetupList()
        {
            try
            {

                var data = (from cs in _hrms.BonusSetups
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                            join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeNumber = emp.EmployeeCode, 
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                Designation = dsg.Name,
                                Department = dpt.Name,
                                Amount = cs.Amount,
                                PayoutMonth = cs.PayoutMonth
                            }).ToList();
                
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditBonusSetup(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.BonusSetups
                        join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        select new
                        {
                            Id = cs.Id,
                            EmployeeId = cs.EmployeeId,
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                            Amount = cs.Amount,
                            PayoutMonth = cs.PayoutMonth
                        }).ToList();

            var result = _hrms.BonusSetups.Where(x => x.Id == id).FirstOrDefault<BonusSetup>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddBonusSetup(BonusSetup obj)
        {
            try
            {
                _hrms.BonusSetups.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateBonusSetup(BonusSetup obj)
        {

            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
                //recrod.EmployeeId = obj.EmployeeId;

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteBonusSetup(int id)
        {
            try
            {
                BonusSetup rg = _hrms.BonusSetups.Where(x => x.Id == id).FirstOrDefault<BonusSetup>();
                _hrms.BonusSetups.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
        public ActionResult EmployeeChange (long JobId)
        {
            var data = (from emp in _hrms.HrmEmployees 
                       
                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where emp.Id == JobId
                        select new
                        {
                          
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                         
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion BonusSetup


        #region Arrears




        public ActionResult Arrear()
        {
            return View();
        }

        

        public ActionResult GetArrearList()
        {
            try
            {

                var data = (from cs in _hrms.Arrears
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                            join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeNumber = emp.EmployeeCode,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                Designation = dsg.Name,
                                Department = dpt.Name,
                                Amount = cs.Amount,
                                PayoutMonth = cs.PayoutMonth
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditArrear(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.Arrears
                        join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        select new
                        {
                            Id = cs.Id,
                            EmployeeId = cs.EmployeeId,
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                            Amount = cs.Amount,
                            PayoutMonth = cs.PayoutMonth
                        }).ToList();

            var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddArrear(Arrear obj)
        {
            try
            {
                _hrms.Arrears.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateArrear(Arrear obj)
        {
            obj.Id = 1;

            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
                //recrod.EmployeeId = obj.EmployeeId;

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteArrear(int id)
        {
            try
            {
                Arrear rg = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
                _hrms.Arrears.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult EmployeeChangeArrear(long JobId)
        {
            var data = (from emp in _hrms.HrmEmployees

                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where emp.Id == JobId
                        select new
                        {

                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion Arrears

        #region AdvanceSalary




        public ActionResult AdvanceSalary()
        {
            return View();
        }



        public ActionResult GetAAdvanceSalaryList()
        {
            try
            {

                var data = (from cs in _hrms.AdvaceSalarys
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                            join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeNumber = emp.EmployeeCode,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                Designation = dsg.Name,
                                Department = dpt.Name,
                                Amount = cs.Amount,
                                PayoutMonth = cs.PayoutMonth
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditAdvanceSalary(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.AdvaceSalarys
                        join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        select new
                        {
                            Id = cs.Id,
                            EmployeeId = cs.EmployeeId,
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                            Amount = cs.Amount,
                            PayoutMonth = cs.PayoutMonth
                        }).ToList();

            var result = _hrms.AdvaceSalarys.Where(x => x.Id == id).FirstOrDefault<AdvanceSalary>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddAdvanceSalary(AdvanceSalary obj)
        {
            try
            {
                _hrms.AdvaceSalarys.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateAdvanceSalary(AdvanceSalary obj)
        {
            obj.Id = 1;

            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
                //recrod.EmployeeId = obj.EmployeeId;

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteAdvanceSalary(int id)
        {
            try
            {
                AdvanceSalary rg = _hrms.AdvaceSalarys.Where(x => x.Id == id).FirstOrDefault<AdvanceSalary>();
                _hrms.AdvaceSalarys.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult EmployeeChangeAdvanceSalary(long JobId)
        {
            var data = (from emp in _hrms.HrmEmployees

                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where emp.Id == JobId
                        select new
                        {

                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion AdvanceSalary

        #region SalarySetup




        public ActionResult SalarySetup()
        {
            return View();
        }



        public ActionResult GetSalarySetupList()
        {
            try
            {

                var data = (from cs in _hrms.SalarySetups
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                            join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                            //join alw in _hrms.Allowances on cs.Id equals alw.Id
                            //join alwde in _hrms.AllowancesDeductions on cs.Id equals alwde.FK_AllowanceId


                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeNumber = emp.EmployeeCode,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                Designation = dsg.Name,
                                Department = dpt.Name,
                                BasicSalary = emp.BasicSalary,
                                //Allowances = cs.Allowances,
                                TotalAmount = cs.TotalAmount,
                                OverTime = cs.OverTime,
                                //Name = alw.Name,
                                //Amount = alwde.Amount


                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditSalarySetup(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;
            string Allowanced = "";
            string AllowanceName = "";
            var SalarySetups = _hrms.SalarySetups.Where(x => x.Id == id).FirstOrDefault();
            int allowanceId = 0;
            string[] values = SalarySetups.AllowanceId.Split(',');
            foreach (var item in values)
            {
                allowanceId = Convert.ToInt32(item);
                Allowanced += item + ",";
                var Allowance = _hrms.AllowancesDeductions.Where(x => x.Id == allowanceId).FirstOrDefault();
                var AName = _hrms.AllowanceTypes.Where(x => x.Id == Allowance.FK_AllowanceId).FirstOrDefault();
                AllowanceName += AName.Name + ",";
            }
            AllowanceName = AllowanceName.Remove(AllowanceName.Length - 1);


            //var asd = Allowanced.Take(Allowanced.Length - 1).Reverse(); 
            var data = (from cs in _hrms.SalarySetups
                        join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where cs.Id == id
                        select new
                        {
                            Id = cs.Id,
                            EmployeeId = cs.EmployeeId,
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                            BasicSalary = emp.BasicSalary,
                            AllowanceId = allowanceId,
                            OverTime = cs.OverTime,
                            TotalAmount = cs.TotalAmount
                        }).ToList();

            //var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddSalarySetup(SalarySetup obj, string[] types)
        {
            try
            {
                _hrms.SalarySetups.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateSalarySetup(SalarySetup obj)
        {


            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
                //recrod.EmployeeId = obj.EmployeeId;

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteSalarySetup(int id)
        {
            try
            {
                SalarySetup rg = _hrms.SalarySetups.Where(x => x.Id == id).FirstOrDefault<SalarySetup>();
                _hrms.SalarySetups.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult EmployeeChangeSalarySetup(long JobId)
        {
            var data = (from emp in _hrms.HrmEmployees

                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where emp.Id == JobId
                        select new
                        {
                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,
                            BasicSalary = emp.BasicSalary

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion SalarySetups+


        #region PayRollCutOff Start

        public ActionResult PayRollCutOff()
        {
            return View();
        }


        
        //public ActionResult AutoCodePayRollCutOff()
        //{
        //    var stringCode = string.Empty;
        //    _hrms.Configuration.ProxyCreationEnabled = false;
        //    //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
        //    var LastCode = _hrms.PayRollCutOffs.Where(x => x.Active == true).ToList().LastOrDefault();
        //    if (LastCode.CutOffDate == null)

        //    {
        //        stringCode = LastCode.CutOffDate.Substring(0, 3);
        //        int intCode = Convert.ToInt16(LastCode.MOLCode.Substring(3));
        //        intCode++;
        //        var threeDidgit = intCode.ToString("D2"); // = "001"
        //        stringCode += threeDidgit;
        //    }
        //    else
        //    {
        //        stringCode = "T-" + 1.ToString("D2");
        //    }
        //    var Code = stringCode;
        //    return Json(Code, JsonRequestBehavior.AllowGet);

        //}
       
        public ActionResult GetPayRollCutOffList()
        {
            try
            {
                //JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //var dd = _hrms.PayRollCutOffs.Where(x => x.Active == true).FirstOrDefault();

                var TradeLicenseList = _hrms.PayRollCutOffs.Where(x => x.Active == true).ToList();

                var result = TradeLicenseList.Select(S => new
                {
                    Id = S.Id,
                    Active = S.Active,
                    CutOffDate = S.CutOffDate?.ToLongDateString(),
                });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult EditPayRollCutOff(int id)
        {
            var result = _hrms.PayRollCutOffs.Where(x => x.Id == id).ToList();
  
            var data = result.Select(S => new
            {
                Id = S.Id,
                Active = S.Active,
                CutOffDate = S.CutOffDate?.ToString("yyyy-MM-dd"),
            }).FirstOrDefault();


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddPayRollCutOff(PayRollCutOff obj)
        {
            try
            {


                bool IsrecExisit = _hrms.PayRollCutOffs.Any(x => x.Id == obj.Id);
                if (IsrecExisit != true)
                {
                    if(DateTime.Today > obj.CutOffDate)
                    {
                        obj.CutOffDate = obj.CutOffDate?.AddMonths(1);
                    }

                    _hrms.PayRollCutOffs.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "TradeLicenses is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdatePayRollCutOff(PayRollCutOff obj)
        {

            try
            {

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public ActionResult DeletePayRollCutOff(int id)
        {
            try
            {
                PayRollCutOff rg = _hrms.PayRollCutOffs.Where(x => x.Id == id).FirstOrDefault<PayRollCutOff>();
                _hrms.PayRollCutOffs.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #endregion PayRollCutOff

         
        #region SpecialAllowance




        public ActionResult SpecialAllowance()
        {
            return View();
        }



        public ActionResult GetSpecialAllowanceList()
        { 
            try
            {

                var data = (from cs in _hrms.SpecialAllowances
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            join alw in _hrms.Allowances on cs.AllowanceType equals alw.Id
                             
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                Amount = cs.Amount,
                                PayRollMonth = cs.PayRollMonth,
                                Effect = cs.Effect,
                                
                                Name = alw.Name,
                                 


                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditSpecialAllowance(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.SpecialAllowances
                        join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                        join alw in _hrms.Allowances on cs.AllowanceType equals alw.Id
                         
                        select new
                        {
                            Id = cs.Id,
                            EmployeeId = cs.EmployeeId,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Amount = cs.Amount,
                            PayRollMonth = cs.PayRollMonth,
                            Effect = cs.Effect,
                           
                            Name = alw.Name,
                             

                        }).ToList();

            //var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddSpecialAllowance(SpecialAllowance obj)
        {
            try
            {
                _hrms.SpecialAllowances.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateSpecialAllowance(SpecialAllowance obj)
        {
             
            try
            {
                //obj.Id = 2;
                //var recrod = _hrms.SpecialAllowances.Where(x => x.Id == obj.Id).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.PayRollMonth = obj.PayRollMonth;
                //recrod.Effect = obj.Effect; 
                //recrod.AllowanceType = obj.AllowanceType;
                //recrod.EmployeeId = obj.EmployeeId;

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteSpecialAllowance(int id)
        {
            try
            {
                SpecialAllowance rg = _hrms.SpecialAllowances.Where(x => x.Id == id).FirstOrDefault<SpecialAllowance>();
                _hrms.SpecialAllowances.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion SpecialAllowance

        #region Payroll Calculation

        public ActionResult PayrollCalculation()
        {   
            return View();
        }


        public ActionResult GetPayrollCalculationDeductionData()
        {
            var sll = _hrms.AdvaceSalarys.ToList();

            var data = (from setup in _hrms.SalarySetups.AsEnumerable()
                        join emp in _hrms.HrmEmployees
                        on setup.EmployeeId equals emp.Id
                        join dedct in _hrms.Deductions
                        on emp.Id equals dedct.EmployeeId into DeductionsGroup
                        from dedctg in DeductionsGroup.DefaultIfEmpty()
                        join loan in _hrms.LoanSanctions
                        on emp.Id equals loan.EmployeeId into LoanSanctionsGroup
                        from loang in LoanSanctionsGroup.DefaultIfEmpty()
                        join advc in _hrms.AdvaceSalarys
                        on emp.Id equals advc.EmployeeId into AdvaceSalarysGroup
                        from advcg in AdvaceSalarysGroup.DefaultIfEmpty()
                        select new
                        {
                            EmployeeId = emp.Id,
                            BasicSalary = emp.BasicSalary,
                            AnnualSalary = emp.BasicSalary * 12,
                            IncomeTax = IncomeTaxCalculation(emp?.BasicSalary * 12),
                            ProvidentFund = ProvidentFundCalculation(),
                            EOBI = ((emp.BasicSalary * 1) / 100),
                            Graduity = GraduityCalculation(emp.JoiningDate, emp.BasicSalary),
                            Deductions = dedctg?.Amount,
                            Advance = advcg?.Amount,
                            Loan = loang?.LoanAmount

                        }).ToList();

            var groupData = data.GroupBy(x => x.EmployeeId).Select(y => new
            {
                EmployeeId = y.ToList().Select(z => z.EmployeeId).FirstOrDefault(),
                BasicSalary = y.ToList().Select(z => z.BasicSalary).FirstOrDefault(),
                AnnualSalary = y.ToList().Select(z => z.AnnualSalary).FirstOrDefault(),
                IncomeTax = y.ToList().Select(z => z.IncomeTax).FirstOrDefault(),
                ProvidentFund = y.ToList().Select(z => z.ProvidentFund).FirstOrDefault(),
                EOBI = y.ToList().Select(z => z.EOBI).FirstOrDefault(),
                Graduity = y.ToList().Select(z => z.Graduity).FirstOrDefault(),
                Deductions = y.ToList().Select(z => z.Deductions).FirstOrDefault(),
                Advance = y.ToList().Select(z => z.Advance).FirstOrDefault(),
                Loan = y.ToList().Select(z => z.Loan).FirstOrDefault(),

            }).ToList();

            return Json(groupData, JsonRequestBehavior.AllowGet);
        }

        public double GraduityCalculation(DateTime? joiningDate, double? BasicSalary)
        {
            var Year = DateTime.Now.Year - joiningDate.Value.Year;

            if(DateTime.Now.Month < joiningDate.Value.Month)
            {
                Year = Year - 1; 
            }

            return (BasicSalary.Value)/26*15*Year;
        }


        public double ProvidentFundCalculation()
        {
            double providentFund = 0.0;

            // providentFund = ((15000 * 12) / 100) * ((3.67 * 15000) / 100);

            providentFund = 15 * 15000 / 100;

            return providentFund;
        }

        public double IncomeTaxCalculation(double? salary)
        {
            double totalTax = 0.0;

            if(salary <= 600000)      
            {
                totalTax = 0.0;
            }
            else if(salary >= 600000 && salary <= 1200000){   

                totalTax = (((salary.Value - 600000) * 2.5) / 100);
            }
            else if (salary >= 1200000 && salary <= 2400000) 
            {
                totalTax = 15000 + (((salary.Value - 1200000) * 12.5) / 100);
            }
            else if (salary >= 2400000 && salary <= 3600000)   
            {
                totalTax = 165000 + (((salary.Value - 2400000) * 20) / 100);
            }
            else if (salary >= 3600000 && salary <= 6000000)   
            {
                totalTax = 405000 + (((salary.Value - 3600000) * 25) / 100);
            }
            else if (salary >= 6000000 && salary <= 12000000)   
            {
                totalTax = 1005000 + (((salary.Value - 6000000) * 32.5) / 100);
            }
            else if (salary > 12000000)   
            {
                totalTax = 2955000 + (((salary.Value - 12000000) * 35) / 100);
            }

            return totalTax;
        }

        public ActionResult GetPayrollCalculationTableData()
        {
            try
            {

                var data1 = (from cs in _hrms.SalarySetups.AsEnumerable()

                             join alw in _hrms.Allowances on cs.AllowanceId equals alw.Id.ToString() into AllowancesGroup
                             from alwg in AllowancesGroup.DefaultIfEmpty()

                             join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id into HrmEmployeesGroup
                             from empg in HrmEmployeesGroup.DefaultIfEmpty()

                             join ds in _hrms.Designations on empg?.DesignationId equals ds.Id into DesignationGroup
                             from dsg in DesignationGroup.DefaultIfEmpty()

                             join dpt in _hrms.Departments on empg?.DepartmentId equals dpt.Id into DepartmentGroup
                             from dptg in DepartmentGroup.DefaultIfEmpty()

                             join atdcal in _hrms.tbl_EmployeeAttendanceCalculations on cs.EmployeeId equals atdcal.EmployeeId into EmployeeAttendanceCalculationsGroup
                             from atdcalg in EmployeeAttendanceCalculationsGroup.DefaultIfEmpty()

                             join arrs in _hrms.Arrears on cs.EmployeeId equals arrs.EmployeeId into ArrearsGroup
                             from arrsg in ArrearsGroup.DefaultIfEmpty()

                             join bonus in _hrms.BonusSetups on cs.EmployeeId equals bonus.EmployeeId into BonusSetupsGroup
                             from bonusg in BonusSetupsGroup.DefaultIfEmpty()

                             join dedct in _hrms.Deductions on empg?.Id equals dedct.EmployeeId into DeductionsGroup
                             from dedctg in DeductionsGroup.DefaultIfEmpty()

                             //join loan in _hrms.LoanSanctions on emp.Id equals loan.EmployeeId into LoanSanctionsGroup
                             //from loang in LoanSanctionsGroup.DefaultIfEmpty()

                             join advc in _hrms.AdvaceSalarys on empg?.Id equals advc.EmployeeId into AdvaceSalarysGroup
                             from advcg in AdvaceSalarysGroup.DefaultIfEmpty()

                             where cs.Active == true

                             select new
                             {
                                 Id = cs.Id,
                                 EmployeeId = cs.EmployeeId,
                                 EmployeeNumber = empg?.EmployeeCode,
                                 EmployeeName = empg?.FirstName + " " + empg?.LastName,
                                 DateofJoining = empg?.JoiningDate?.ToString("dd-MMM-yyyy"),
                                 Designation = dsg?.Name,
                                 Department = dptg?.Name,
                                 BasicSalary = empg?.BasicSalary ?? 0,
                                 AnnualSalary = empg?.BasicSalary * 12 ?? 0,
                                 //Allowances = cs.Allowances,
                                 TotalAmount = cs.TotalAmount,
                                 OverTime = cs.OverTime,
                                 AllowanceName = alwg?.Name,
                                 //Amount = alwde.Amount,
                                 SalaryMonth = DateTime.Now.ToString("MMMM"),
                                 TotalPayableDays = (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - (atdcalg?.AbsentDays ?? 0),
                                 NormalOTHours = atdcalg?.NormalOTHours ?? 0,
                                 WeekendOTHours = atdcalg?.WeekendOTHours ?? 0,
                                 PublicHolidaysOTHours = atdcalg?.PublicHolidaysOTHours ?? 0,
                                 ArrearsAmount = arrsg?.Amount ?? 0,
                                 BonusAmount = bonusg?.Amount ?? 0,
                                 Deductions = dedctg?.Amount ?? 0,
                                 Advance = advcg?.Amount ?? 0,
                                 // Loan = loang?.LoanAmount,
                                 Loan = 0,
                                 IncomeTax = IncomeTaxCalculation((empg?.BasicSalary * 12) ?? 0),
                                 ProvidentFund = ProvidentFundCalculation(),
                                 EOBI = ((empg?.BasicSalary * 1) / 100) ?? 0,
                                 Graduity = GraduityCalculation(empg?.JoiningDate, empg?.BasicSalary),

                             }).ToList();

                var data18 = (from cs in _hrms.SalarySetups.AsEnumerable()

                              join alw in _hrms.Allowances on cs.AllowanceId equals alw.Id.ToString()

                              join atdcal in _hrms.tbl_EmployeeAttendanceCalculations on cs.EmployeeId equals atdcal.EmployeeId into tbl_EmployeeAttendanceCalculationsGroup
                              from atdcalg in tbl_EmployeeAttendanceCalculationsGroup.DefaultIfEmpty()

                              join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                              join ds in _hrms.Designations on emp.DesignationId equals ds.Id
                              join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id

                              join arrs in _hrms.Arrears on cs.EmployeeId equals arrs.EmployeeId into ArrearsGroup
                              from arrsg in ArrearsGroup.DefaultIfEmpty()

                              join bonus in _hrms.BonusSetups on cs.EmployeeId equals bonus.EmployeeId
                              join dedct in _hrms.Deductions on emp.Id equals dedct.EmployeeId
                              join advc in _hrms.AdvaceSalarys on emp.Id equals advc.EmployeeId

                              //join loan in _hrms.LoanSanctions on emp.Id equals loan.EmployeeId

                              where cs.Active == true

                              select new
                              {
                                  AmountArrear = arrsg?.Amount,
                                  AbsentDays = atdcalg?.AbsentDays,
                                  Id = cs.Id,
                                  EmployeeId = cs.EmployeeId,
                                  EmployeeNumber = emp.EmployeeCode,
                                  EmployeeName = emp.FirstName + " " + emp.LastName,
                                  DateofJoining = emp.JoiningDate?.ToString("dd-MMM-yyyy"),
                                  //Designation = ds.Name,
                                  //Department = dpt.Name,
                                  BasicSalary = emp.BasicSalary,
                                  AnnualSalary = emp.BasicSalary * 12,
                                  //Allowances = cs.Allowances,
                                  TotalAmount = cs.TotalAmount,
                                  OverTime = cs.OverTime,
                                  AllowanceName = alw.Name,
                                  //Amount = alwde.Amount,
                                  SalaryMonth = DateTime.Now.ToString("MMMM"),
                                  TotalPayableDays = (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - atdcal?.AbsentDays,
                                  NormalOTHours = atdcalg?.NormalOTHours,
                                  WeekendOTHours = atdcalg?.WeekendOTHours,
                                  PublicHolidaysOTHours = atdcalg?.PublicHolidaysOTHours,
                                  ArrearsAmount = arrsg?.Amount,
                                  BonusAmount = bonus?.Amount,
                                  Deductions = dedct?.Amount,
                                  Advance = advc?.Amount,
                                  //Loan = loan?.LoanAmount,
                                  IncomeTax = IncomeTaxCalculation(emp.BasicSalary * 12),
                                  ProvidentFund = ProvidentFundCalculation(),
                                  EOBI = ((emp.BasicSalary * 1) / 100),
                                  Graduity = GraduityCalculation(emp.JoiningDate, emp.BasicSalary),

                              }).ToList();


                //var data1 = (from cs in _hrms.SalarySetups.AsEnumerable()
                //             join alw in _hrms.Allowances on cs.AllowanceId equals alw.Id.ToString()
                //             join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                //             join ds in _hrms.Designations on emp.DesignationId equals ds.Id
                //             join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id 
                //             join atdcal in _hrms.tbl_EmployeeAttendanceCalculations on cs.EmployeeId equals atdcal.EmployeeId
                //             join arrs in _hrms.Arrears on cs.EmployeeId equals arrs.EmployeeId
                //             join bonus in _hrms.BonusSetups on cs.EmployeeId equals bonus.EmployeeId
                //             join dedct in _hrms.Deductions on emp.Id equals dedct.EmployeeId 
                //             join loan in _hrms.LoanSanctions on emp.Id equals loan.EmployeeId 
                //             join advc in _hrms.AdvaceSalarys on emp.Id equals advc.EmployeeId

                //             where cs.Active == true && alw.IsActive == true && emp.Active == true && ds?.Active == true && dpt?.Active == true && arrs?.Active == true && bonus?.Active == true
                //             select new
                //             {
                //                 Id = cs.Id,
                //                 EmployeeId = cs.EmployeeId,
                //                 EmployeeNumber = emp.EmployeeCode,
                //                 EmployeeName = emp.FirstName + " " + emp.LastName,
                //                 DateofJoining = emp.JoiningDate?.ToString("dd-MMM-yyyy"),
                //                 Designation = ds.Name,
                //                 Department = dpt.Name,
                //                 BasicSalary = emp.BasicSalary,
                //                 AnnualSalary = emp.BasicSalary * 12,
                //                 //Allowances = cs.Allowances,
                //                 TotalAmount = cs.TotalAmount,
                //                 OverTime = cs.OverTime,
                //                 AllowanceName = alw.Name,
                //                 //Amount = alwde.Amount,
                //                 SalaryMonth = DateTime.Now.ToString("MMMM"),
                //                 TotalPayableDays = (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - atdcal?.AbsentDays,
                //                 NormalOTHours = atdcal?.NormalOTHours,
                //                 WeekendOTHours = atdcal?.WeekendOTHours,
                //                 PublicHolidaysOTHours = atdcal?.PublicHolidaysOTHours,
                //                 ArrearsAmount = arrs?.Amount,
                //                 BonusAmount = bonus?.Amount,
                //                 Deductions = dedct?.Amount,
                //                 Advance = advc?.Amount,
                //                 Loan = loan?.LoanAmount,
                //                 IncomeTax = IncomeTaxCalculation(emp.BasicSalary * 12),
                //                 ProvidentFund = ProvidentFundCalculation(),
                //                 EOBI = ((emp.BasicSalary * 1) / 100),
                //                 Graduity = GraduityCalculation(emp.JoiningDate, emp.BasicSalary),

                //             }).ToList();

                var data = data1.GroupBy(x => x.EmployeeId).Select(y => new
                {
                    Id = y.ToList().Select(z => z.Id).FirstOrDefault(),
                    EmployeeId = y.ToList().Select(z => z.EmployeeId).FirstOrDefault(),
                    EmployeeNumber = y.ToList().Select(z => z.EmployeeNumber).FirstOrDefault(),
                    EmployeeName = y.ToList().Select(z => z.EmployeeName).FirstOrDefault(),
                    DateofJoining = y.ToList().Select(z => z.DateofJoining).FirstOrDefault(),
                    Designation = y.ToList().Select(z => z.Designation).FirstOrDefault(),
                    Department = y.ToList().Select(z => z.Department).FirstOrDefault(),
                    BasicSalary = y.ToList().Select(z => z.BasicSalary).FirstOrDefault(),
                    Allowances1 = y.ToList().Select(z => z.AllowanceName).FirstOrDefault(),    //Skip(), Take()
                    Allowances2 = y.ToList().Select(z => z.AllowanceName).Skip(1).FirstOrDefault(),
                    Allowances3 = y.ToList().Select(z => z.AllowanceName).Skip(2).FirstOrDefault(),
                    TotalAmount1 = y.ToList().Select(z => z.TotalAmount).FirstOrDefault(),   
                    TotalAmount2 = y.ToList().Select(z => z.TotalAmount).Skip(1).FirstOrDefault(),
                    TotalAmount3 = y.ToList().Select(z => z.TotalAmount).Skip(2).FirstOrDefault(),
                    SalaryMonth = y.ToList().Select(z => z.SalaryMonth).FirstOrDefault(),
                    TotalPayableDays = y.ToList().Select(z => z.TotalPayableDays).FirstOrDefault(),
                    NormalOTHours = y.ToList().Select(z => z.NormalOTHours).FirstOrDefault(),
                    WeekendOTHours = y.ToList().Select(z => z.WeekendOTHours).FirstOrDefault(),
                    PublicHolidaysOTHours = y.ToList().Select(z => z.PublicHolidaysOTHours).FirstOrDefault(),
                    ArrearsAmount = y.ToList().Select(z => z.ArrearsAmount).FirstOrDefault(),
                    BonusAmount = y.ToList().Select(z => z.BonusAmount).FirstOrDefault(),
                    AnnualSalary = y.ToList().Select(z => z.AnnualSalary).FirstOrDefault(),
                    IncomeTax = y.ToList().Select(z => z.IncomeTax).FirstOrDefault(),
                    ProvidentFund = y.ToList().Select(z => z.ProvidentFund).FirstOrDefault(),
                    EOBI = y.ToList().Select(z => z.EOBI).FirstOrDefault(),
                    Graduity = y.ToList().Select(z => z.Graduity).FirstOrDefault(),
                    Deductions = y.ToList().Select(z => z.Deductions).FirstOrDefault(),
                    Advance = y.ToList().Select(z => z.Advance).FirstOrDefault(),
                    Loan = y.ToList().Select(z => z.Loan).FirstOrDefault(),
                }).ToList();

                var overTimeSetupsList = _hrms.OverTimeSetups.Where(x => x.Active == true).ToList();

                return Json(new { Data = data, OverTimeSetupsList = overTimeSetupsList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Payroll Calculation
    }
}