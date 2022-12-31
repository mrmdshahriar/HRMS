using HRMS.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class RecruitmentController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        string DocPhysicalPath = WebConfigurationManager.AppSettings["DocPhysicalPath"];
        string DocPhysicalPathImage = WebConfigurationManager.AppSettings["DocPhysicalPathImage"];
        string DocLivePath = WebConfigurationManager.AppSettings["DocLivePath"];
        // GET: Recruitment

        // GET: Recruitment
        public ActionResult Index()
        {
            return RedirectToAction(nameof(JobsRequestion));
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            file = Request.Files["file"];
            string FileWithPath = string.Empty;
            string fileName = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var files = fileName.Split('.');
                var FileName = files[0];
                var FileExtenstion = files[1];
                var guid = string.Join("", Guid.NewGuid().ToString().Split('-'));
                fileName = guid + "." + FileExtenstion;
                if (!Directory.Exists(Server.MapPath("~/Files/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Files/"));
                }
                FileWithPath = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(FileWithPath);
                return Json(new { success = true, savedFilePath = fileName, JsonRequestBehavior.AllowGet });

            }
            return Json(new { success = false, savedFilePath = fileName, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ContentResult DownloadFile(string fileName)
        {

            //Build the File Path.
            string path = Server.MapPath("~/Files/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            // return File(bytes, "application/octet-stream", fileName);
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }

        #region Employee_JobRequestion
        public ActionResult JobsRequestion()
        {
            return View();
        }
        public ActionResult GetAllJobsRequestion()
        {
            var data = _hrms.JobRequisitions.ToList();
            List<object> listOfObject = new List<object>();
            foreach (var item in data)
            {
                var newObject = new
                {
                    JobRequestion = item,
                    LastDate = item.LastDate.ToString("dd/MM/yyyy"),
                    JobType = _hrms.HrmJobTypes.FirstOrDefault(x => x.Id == item.JobType)?.Name,
                    Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
                    Department = _hrms.Departments.FirstOrDefault(x => x.Id == item.DepartmentId)?.Name,
                };
                listOfObject.Add(newObject);
            }
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(listOfObject, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult JobRequestion(int? Id)
        {
            var jobRequisitions = _hrms.JobRequisitions.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var JobTypes = _hrms.HrmJobTypes.ToList();
            var model = new Common
            {
                JobRequisitions = jobRequisitions,
                DesignationList = Designations,
                DepartmentList = Departments,
                JobTypeList = JobTypes
            };
            return View(model);
        }
        public ActionResult LoadJobsRequestion(int id)
        {
            var jobRequstion = _hrms.JobRequisitions.FirstOrDefault(x => x.ReqId == id);
            return Json(new
            {
                jobRequstion = jobRequstion,
                LastDate = jobRequstion?.LastDate.ToString("yyyy-MM-dd")
            }
            , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertUpdateJobsRequestion(JobRequisitions jobRequisition)
        {
            var obj = _hrms.JobRequisitions.Where(x => x.ReqId == jobRequisition.ReqId).FirstOrDefault();
            if (obj != null && !string.IsNullOrEmpty(obj.Attachment) && !obj.Attachment.Equals(jobRequisition.Attachment))
            {
                string path = Path.Combine(Server.MapPath("~/Files/"), obj.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            if (obj != null)
                _hrms.Entry(obj).State = EntityState.Detached;
            if (jobRequisition.ReqId > 0)
            {
                _hrms.Entry(jobRequisition).State = EntityState.Modified;
            }
            else
            {
                _hrms.JobRequisitions.Add(jobRequisition);
            }

            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        public ActionResult DeleteJobsRequestion(int Id)
        {
            JobRequisitions job = _hrms.JobRequisitions.Where(x => x.ReqId == Id).FirstOrDefault<JobRequisitions>();
            if (job != null && !string.IsNullOrEmpty(job.Attachment))
            {
                string path = Path.Combine(Server.MapPath("~/Files/"), job.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            _hrms.JobRequisitions.Remove(job);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion

        #region Job Master Start
        public ActionResult Job()
        {
            return View();
        }


        public ActionResult GetJobList()
        {
            try
            {

                var JobList = _hrms.Jobs.Select(s => s).ToList();
                var DepartmentList = _hrms.Departments.Select(s => s).ToList();
                var DesignationList = _hrms.Designations.Select(s => s).ToList();
                var Result = from st in JobList
                                     join d in DepartmentList on st.DepartmentId equals d.Id into table1
                                     from d in table1.ToList()
                                     join i in DesignationList on st.DesignationId equals i.Id into table2
                                     from i in table2.ToList()

                                select new
                                {
                                    Id = st.JobId,
                                    Name = st.JobTitle,
                                    ClosingDate =st.ClosingDate.ToString(),
                                    Active = st.Active,
                                    DepertmentName = d.Name,
                                    DesignationName = i.Name,
                                    Shift = st.ShiftId
                                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public ActionResult EditJobList(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Jobs.Where(x => x.JobId == id).FirstOrDefault<Job>();
                //DateTime dt=DateTime.Parse(s);
                var abc=result.ClosingDate.ToString();
                result.CreatedBy = abc;
                return Json(result, JsonRequestBehavior.AllowGet);

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddJobMaster(Job obj)
        {
            try
            {
                _hrms.Jobs.Add(obj);

                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateJobMaster(Job obj)
        {
            try
            {
                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteJobMaster(int id)
        {
            try
            {
                Job jb = _hrms.Jobs.Where(x => x.JobId == id).FirstOrDefault<Job>();
                _hrms.Jobs.Remove(jb);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion Job Master End


        #region Applied Candidates Start


        public ActionResult HrmAppliedCandidate()
        {

            return View();
        }


        public ActionResult AppliedCandidateList()
        {
            try
            {

                //List<AppliedCandidat> AppliedCandidatList = _hrms.Applieds.ToList<AppliedCandidat>();
                //List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                var DepartmentList = _hrms.Designations.Select(s => s).ToList();
                var DesignationList = _hrms.Designations.Select(s => s).ToList();
                var Result = from st in AppliedCandidatList
                             join d in DepartmentList on st.DepartmentId equals d.Id into table1
                             from d in table1.ToList()
                             join i in DesignationList on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                             from j in table3.ToList()
                             where st.Status == "applied"
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = j.JobTitle,
                                 ApplyDate = st.ApplyDate.ToString(),
                                 AvailableDate = st.AvailableDate.ToString(),
                                 Active = st.Active,
                                 DepertmentName = d.Name,
                                 DesignationName = i.Name,
                                 Status = st.Status,
                                 Attachment = DocPhysicalPath + st.Attachment

                             };


                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AppliedCandidate(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                var applyDate = result.ApplyDate.ToString();
                result.CreatedBy = applyDate;
                var availableDate = result.AvailableDate.ToString();
                result.LastModifiedBy = availableDate;
                var fileView = DocPhysicalPath + result.Attachment;
                result.Attachment = fileView;
                var imageView = DocPhysicalPathImage + result.Photo;
                result.Photo = imageView;
                Session["shortList"] = "shortList";
                //ViewData["shortList"] = "shortList";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AddAppliedCandidate()
        {
            //AppliedCandidat model = new AppliedCandidat();
            //if (id!=null&&id>0)
            //{
            //    _hrms.Configuration.ProxyCreationEnabled = false;

            //    model = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
            //    if (model == null)
            //    {
            //        model = new AppliedCandidat();
            //    }
            //}

            return View();
        }

        [HttpPost]
        public ActionResult AddAppliedCandidate(AppliedCandidat obj)
        {
            try
            {

                    string _imgname = string.Empty;
                    string fileGuid = string.Empty;

                    if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                    {
                        var pic = System.Web.HttpContext.Current.Request.Files["Photo"];

                        var Attachment = System.Web.HttpContext.Current.Request.Files["Attachment"];
                        if (pic.ContentLength > 0 || Attachment.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(pic.FileName);
                            string _filename;
                            var _ext = Path.GetExtension(pic.FileName);
                            int fileExtPos = fileName.LastIndexOf(".");

                            _filename = fileName.Substring(0, fileExtPos);
                        if (_filename.Contains(" "))
                        {
                            _filename = _filename.Replace(" ", "_");

                        }
                        _imgname = Guid.NewGuid().ToString();
                            string _comPath = Path.Combine(Server.MapPath(DocPhysicalPathImage) + _filename + _imgname + _ext);
                            obj.Photo = _filename + _imgname + _ext;

                            string fname = Attachment.FileName;

                           if (fname.Contains(" "))
                             {
                                fname = fname.Replace(" ", "_");

                            }

                            fileGuid = Guid.NewGuid().ToString();
                            var extension = Path.GetExtension(Attachment.FileName);
                            obj.Attachment = fileGuid + fname;
                            fname = Path.Combine(Server.MapPath(DocPhysicalPath), fileGuid + fname);
                            // Get the complete folder path and store the file inside it.

                            //obj.Attachment = "~/Content/Assets/UploadImage/fileUpload/" + fname;
                            //ViewBag.Msg = _comPath;
                            var path = _comPath;
                            obj.Status = "applied";

                            _hrms.Applieds.Add(obj);
                            if (_hrms.SaveChanges() > 0)
                            {
                                // Saving Image in Original Mode
                                pic.SaveAs(path);
                                Attachment.SaveAs(fname);

                                // resizing image
                                MemoryStream ms = new MemoryStream();
                                WebImage img = new WebImage(_comPath);

                                if (img.Width > 200)
                                    img.Resize(200, 200);
                                img.Save(_comPath);
                                // end resize
                            }
                        }
                    }

                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult UpdateAppliedCandidate(AppliedInterviewViewModel model)
        {
            try
            {

                if(Session["shortList"] == "shortList") {

                    var objApplied = new AppliedCandidat
                    {
                        AppliedId = model.AppliedId,
                        CandidateName = model.CandidateName,
                        FatherName = model.FatherName,
                        CNIC = model.CNIC,
                        ContactNumber = model.ContactNumber,
                        Email = model.Email,
                        JobTitle = model.JobTitle,
                        JobType = model.JobType,
                        ShiftId = model.ShiftId,
                        DepartmentId = model.DepartmentId,
                        DesignationId = model.DesignationId,
                        MinExpereince = model.MinExpereince,
                        MaxExpereince = model.MaxExpereince,
                        MInQualification = model.MInQualification,
                        AddvertiseNo = model.AddvertiseNo,
                        Location = model.Location,
                        Gender = model.Gender,
                        Skills = model.Skills,
                        ExpectedSalary = model.ExpectedSalary,
                        AppliedFrom = model.AppliedFrom,
                        Age = model.Age,
                        Currency = model.Currency,
                        Status = model.Status,
                        Photo = model.Photo,
                        Attachment = model.Attachment,
                        ApplyDate = model.ApplyDate,
                        AvailableDate = model.AvailableDate,
                        Active = model.Active
                    };

                    var status = "shortList";
                    objApplied.Status = status;
                    _hrms.Entry(objApplied).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    Session.Abandon();
                }
                if(Session["interview"] == "interview")
                {
                    var objApplied = new AppliedCandidat
                    {
                        AppliedId = model.AppliedId,
                        CandidateName = model.CandidateName,
                        FatherName = model.FatherName,
                        CNIC = model.CNIC,
                        ContactNumber = model.ContactNumber,
                        Email = model.Email,
                        JobTitle = model.JobTitle,
                        JobType = model.JobType,
                        ShiftId = model.ShiftId,
                        DepartmentId = model.DepartmentId,
                        DesignationId = model.DesignationId,
                        MinExpereince = model.MinExpereince,
                        MaxExpereince = model.MaxExpereince,
                        MInQualification = model.MInQualification,
                        AddvertiseNo = model.AddvertiseNo,
                        Location = model.Location,
                        Gender = model.Gender,
                        Skills = model.Skills,
                        ExpectedSalary = model.ExpectedSalary,
                        AppliedFrom = model.AppliedFrom,
                        Age = model.Age,
                        Currency = model.Currency,
                        Status = model.Status,
                        Photo = model.Photo,
                        Attachment = model.Attachment,
                        ApplyDate = model.ApplyDate,
                        AvailableDate = model.AvailableDate,
                        Active = model.Active
                    };

                    var status = "interview";
                    objApplied.Status = status;

                    _hrms.Entry(objApplied).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    Session.Abandon();
                }
                if(Session["selected"] == "selected")
                {
                    var objInterview = new InterviewAssessment
                    {
                        AppliedId = model.AppliedId,
                        Education=model.Education,
                        ComputerLiteracy=model.ComputerLiteracy,
                        Intelligence=model.Intelligence,
                        ExperienceInterviewed=model.ExperienceInterviewed,
                        ExperienceCompanyBusiness=model.ExperienceCompanyBusiness,
                        JobKnowledgeSkill=model.JobKnowledgeSkill,
                        Personality=model.Personality,
                        CommunicationSkills=model.CommunicationSkills,
                        DevelopmentMotivation=model.DevelopmentMotivation,
                        PersonalAptitude=model.PersonalAptitude,
                        Comments=model.Comments
                    };

                    var objApplied = new AppliedCandidat
                    {
                        AppliedId=model.AppliedId,
                        CandidateName=model.CandidateName,
                        FatherName=model.FatherName,
                        CNIC=model.CNIC,
                        ContactNumber=model.ContactNumber,
                        Email=model.Email,
                       JobTitle=model.JobTitle,
                       JobType=model.JobType,
                        ShiftId=model.ShiftId,
                        DepartmentId=model.DepartmentId,
                        DesignationId=model.DesignationId,
                        MinExpereince=model.MinExpereince,
                        MaxExpereince=model.MaxExpereince,
                        MInQualification=model.MInQualification,
                        AddvertiseNo=model.AddvertiseNo,
                        Location=model.Location,
                        Gender=model.Gender,
                        Skills=model.Skills,
                        ExpectedSalary=model.ExpectedSalary,
                        AppliedFrom=model.AppliedFrom,
                        Age=model.Age,
                        Currency=model.Currency,
                        Status=model.Status,
                        Photo=model.Photo,
                        Attachment=model.Attachment,
                        ApplyDate=model.ApplyDate,
                        AvailableDate=model.AvailableDate,
                        Active=model.Active
                    };

                    _hrms.InterviewAssessments.Add(objInterview);
                    var status = "Selected";
                    objApplied.Status = status;
                    _hrms.Entry(objApplied).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    Session.Abandon();

                }
                string _imgname = string.Empty;
                string fileGuid = string.Empty;

                //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                //{
                    //var pic = System.Web.HttpContext.Current.Request.Files["Photo"];

                    //var Attachment = System.Web.HttpContext.Current.Request.Files["Attachment"];
                    //if (pic.ContentLength > 0 || Attachment.ContentLength > 0)
                    //{
                        //var fileName = Path.GetFileName(pic.FileName);
                        //string _filename;
                        //var _ext = Path.GetExtension(pic.FileName);
                        //int fileExtPos = fileName.LastIndexOf(".");

                        //_filename = fileName.Substring(0, fileExtPos);

                        //_imgname = Guid.NewGuid().ToString();
                        //string _comPath = Path.Combine(Server.MapPath("~/Content/Assets/UploadImage/images/") + _filename + _imgname + _ext);
                        //obj.Photo = _filename + _imgname + _ext;

                        //string fname = Attachment.FileName;
                        //fileGuid = Guid.NewGuid().ToString();
                        //var extension = Path.GetExtension(Attachment.FileName);
                        //obj.Attachment = fileGuid + fname;
                        //fname = Path.Combine(Server.MapPath("~/Content/Assets/UploadImage/fileUpload/"), fileGuid + fname);
                        // Get the complete folder path and store the file inside it.

                        //obj.Attachment = "~/Content/Assets/UploadImage/fileUpload/" + fname;
                        //ViewBag.Msg = _comPath;
                        //var path = _comPath;


                        //if (_hrms.SaveChanges() > 0)
                        //{
                        //    // Saving Image in Original Mode
                        //    pic.SaveAs(path);
                        //    Attachment.SaveAs(fname);

                //    // resizing image
                //    MemoryStream ms = new MemoryStream();
                //    WebImage img = new WebImage(_comPath);

                //    if (img.Width > 200)
                //        img.Resize(200, 200);
                //    img.Save(_comPath);
                // end resize
                //    }
                //}
                //}

                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });


                //_hrms.Entry(obj).State = EntityState.Modified;
                //_hrms.SaveChanges();
                //return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteAppliedCandidate(int id)
        {
            try
            {
                AppliedCandidat jb = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                _hrms.Applieds.Remove(jb);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult ShortList()
        {
            return View();
        }

        public ActionResult getShortList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                var Result = from st in AppliedCandidatList
                             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                             from d in table1.ToList()
                             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                             from j in table3.ToList()
                             where st.Status == "shortList"
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = j.JobTitle,
                                 ApplyDate = st.ApplyDate.ToString(),
                                 AvailableDate = st.AvailableDate.ToString(),
                                 Active = st.Active,
                                 DepertmentName = d.Name,
                                 DesignationName = i.Name,
                                 Status = st.Status,
                                 Attachment = DocPhysicalPath + st.Attachment

                             };


                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ViewShortList(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                var applyDate = result.ApplyDate.ToString();
                result.CreatedBy = applyDate;
                var availableDate = result.AvailableDate.ToString();
                result.LastModifiedBy = availableDate;
                Session["interview"] = "interview";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Interview()
        {
            return View();
        }

        public ActionResult InterviewList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                var Result = from st in AppliedCandidatList
                             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                             from d in table1.ToList()
                             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                             from j in table3.ToList()
                             where st.Status == "interview"
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = j.JobTitle,
                                 ApplyDate = st.ApplyDate.ToString(),
                                 AvailableDate = st.AvailableDate.ToString(),
                                 Active = st.Active,
                                 DepertmentName = d.Name,
                                 DesignationName = i.Name,
                                 Status = st.Status,
                                 Attachment = DocPhysicalPath + st.Attachment

                             };


                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult ViewInterView(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                var applyDate = result.ApplyDate.ToString();
                result.CreatedBy = applyDate;
                var availableDate = result.AvailableDate.ToString();
                result.LastModifiedBy = availableDate;
                Session["selected"] = "selected";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Selected()
        {
            return View();
        }

        public ActionResult SelectedList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                var Result = from st in AppliedCandidatList
                             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                             from d in table1.ToList()
                             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                             from j in table3.ToList()
                             where st.Status == "Selected"
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = j.JobTitle,
                                 ApplyDate = st.ApplyDate.ToString(),
                                 AvailableDate = st.AvailableDate.ToString(),
                                 Active = st.Active,
                                 DepertmentName = d.Name,
                                 DesignationName = i.Name,
                                 Status = st.Status,
                                 Attachment = DocPhysicalPath + st.Attachment

                             };


                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult OfferLetter(FormCollection Form)
        {

            try
            {
                OfferLetter obj = new OfferLetter();
                long id = Convert.ToInt64(Form["AppliedId"]);
                obj.EmployeeId = Form["AppliedId"];
                obj.DesignationId =Form["designation"];
                obj.DepartmentId =Form["department"];
                obj.JoiningDate =Convert.ToDateTime(Form["date"]);
                obj.Salary =Form["Salary"];
                var data = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                bool IsrecExisit = _hrms.OfferLetters.Any(x => x.EmployeeId == obj.EmployeeId);
                if (IsrecExisit != true)
                {

                    data.Status = "OfferLetter";
                    _hrms.Entry(data).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    _hrms.OfferLetters.Add(obj);
                    _hrms.SaveChanges();

                    var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                    //List<OfferLetter> offerlist = _hrms.OfferLetters.ToList<OfferLetter>();
                    //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                    var Result = from st in AppliedCandidatList
                             join d in _hrms.OfferLetters on st.AppliedId equals Convert.ToInt32(d.EmployeeId) into table3
                             from d in table3.ToList()
                             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             where st.AppliedId == Convert.ToInt32(obj.EmployeeId)
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = st.JobTitle,
                                 ApplyDate = d.JoiningDate.ToString(),
                                 Salary=d.Salary,
                                 designation=i.Name
                             };

                var abc = Result.ToList();

                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                PdfDocument document = new PdfDocument();
                pdfDoc.Open();

                //Top Heading
                Chunk chunk = new Chunk("Offer Letter", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC, BaseColor.MAGENTA));

                pdfDoc.Add(chunk);

                //Horizontal Line
                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                pdfDoc.Add(line);

                //Table
                PdfPTable table1 = new PdfPTable(2);
                table1.WidthPercentage = 100;
                //0=Left, 1=Centre, 2=Right
                table1.HorizontalAlignment = 0;
                table1.SpacingBefore = 20f;
                table1.SpacingAfter = 30f;




                //Add table to document
                pdfDoc.Add(table1);

                //Horizontal Line
                line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                pdfDoc.Add(line);

                Paragraph para1 = new Paragraph();
                para1.Add("To,");
                pdfDoc.Add(para1);
                Paragraph para2 = new Paragraph();
                para2.Add("Name \t\t:    "+abc[0].Name);
                para2.SpacingBefore = 10f;
                para2.SpacingAfter = 10f;
                pdfDoc.Add(para2);

                Paragraph para3 = new Paragraph();
                para3.Add("CONGRATULATIONS!");
                para3.SpacingBefore = 10f;
                para3.SpacingAfter = 10f;
                pdfDoc.Add(para3);


                Paragraph para4 = new Paragraph();
                para4.Add("Subject: Offer for Appointment for\t\t   " + abc[0].designation);
                para4.SpacingBefore = 10f;
                para4.SpacingAfter = 10f;
                pdfDoc.Add(para4);


                Paragraph para5 = new Paragraph();
                para5.Add("Dear \t\t       " + abc[0].Name);
                para5.SpacingBefore = 10f;
                para5.SpacingAfter = 10f;
                pdfDoc.Add(para5);

                Paragraph para6 = new Paragraph();
                para6.Add("With reference to your application and subsequent interview with us.\nwe are pleased to offer you the following position:");
                pdfDoc.Add(para6);

                ////Cell
                //cell = new PdfPCell();
                //chunk = new Chunk("This Month's Transactions of your Credit Card");
                //cell.AddElement(chunk);
                //cell.Colspan = 5;
                //cell.BackgroundColor = BaseColor.PINK;
                //table1.AddCell(cell);


                PdfPCell cell1 = new PdfPCell(new Phrase("Position"));

                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase(abc[0].designation));
                table1.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Company Location"));
                table1.AddCell(cell2);
                cell2 = new PdfPCell(new Phrase("ABC"));
                table1.AddCell(cell2);


                PdfPCell cell3 = new PdfPCell(new Phrase("Probation"));
                table1.AddCell(cell3);
                cell3 = new PdfPCell(new Phrase("3 Month"));
                table1.AddCell(cell3);



                PdfPCell cell4 = new PdfPCell(new Phrase("Salary"));
                table1.AddCell(cell4);
                cell4 = new PdfPCell(new Phrase(obj.Salary));
                table1.AddCell(cell4);


                PdfPCell cell5 = new PdfPCell(new Phrase("Joining Date"));
                table1.AddCell(cell5);
                cell5 = new PdfPCell(new Phrase(obj.JoiningDate.ToShortDateString()));
                table1.AddCell(cell5);
                pdfDoc.Add(table1);


                Paragraph para = new Paragraph();
                para.Add("You are requested to return the duplicate copy of the offer of appointment signed by you in token of your acceptance or Email back to us using your personal email address to our official id tendering your consent.");
                para.SpacingBefore = 5f;
                para.SpacingAfter = 5f;
                pdfDoc.Add(para);


                Paragraph para8 = new Paragraph();
                para8.Add("We welcome you and look forward to a long and successful association");
                para8.SpacingBefore = 5f;
                para8.SpacingAfter = 5f;
                pdfDoc.Add(para8);

                Paragraph para9 = new Paragraph();
                para9.Add("Yours sincerely,");
                para9.SpacingBefore = 5f;
                para9.SpacingAfter = 5f;
                pdfDoc.Add(para9);


                Paragraph para11 = new Paragraph();
                para11.Add("Signature");
                para11.SpacingBefore = 5f;
                para11.SpacingAfter = 5f;
                pdfDoc.Add(para11);

                //Horizontal Line
                line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                pdfDoc.Add(line);

                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", );

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
                    return RedirectToActionPermanent("Offerletters", "Recruitment");
                }
                else
                {
                    ViewBag.SuccessCreate = "Saved successfully";
                }
                return RedirectToActionPermanent("Offerletters", "Recruitment");
                 //return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult Offerletters()
        {
            return View();
        }


        public ActionResult OfferlettersList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                var Result = from st in AppliedCandidatList
                             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                             from d in table1.ToList()
                             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                             from i in table2.ToList()
                             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                             from j in table3.ToList()
                             where st.Status == "OfferLetter"
                             select new
                             {
                                 Id = st.AppliedId,
                                 Name = st.CandidateName,
                                 JobTitle = j.JobTitle,
                                 ApplyDate = st.ApplyDate.ToString(),
                                 AvailableDate = st.AvailableDate.ToString(),
                                 Active = st.Active,
                                 DepertmentName = d.Name,
                                 DesignationName = i.Name,
                                 Status = st.Status,
                                 Attachment = DocPhysicalPath + st.Attachment

                             };


                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Applied Candidates End


        [HttpPost]
        public ActionResult AddInterView(InterviewAssessment obj)
        {
            try
            {
                _hrms.InterviewAssessments.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}