using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class UserController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        #region start User 
        public ActionResult Users()
        {
            return View();
        }


        public ActionResult GetUserList()
        {
            List<User> UserList = _hrms.Users.ToList<User>();
            List<UserType> UserTypeList = _hrms.UserTypes.ToList<UserType>();
            var innerjoin = from s in UserTypeList // outer sequence
                            join st in UserList //inner sequence 
                            on s.Id equals st.UserTypeId // key selector 
                            select new
                            { // result selector 
                                Id = st.Id,
                                Name = st.Name,
                                UserTypeId = st.UserTypeId,
                                Active = st.Active,
                                UserTypeName = s.Name
                            };
            return Json(innerjoin, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.Users.Where(x => x.Id == id).FirstOrDefault<User>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddUser(User obj)
        {
            _hrms.Users.Add(obj);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult UpdateUser(User obj)
        {
            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {

            User u = _hrms.Users.Where(x => x.Id == id).FirstOrDefault<User>();
            _hrms.Users.Remove(u);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

        }

        #endregion


        #region start User Type

        public ActionResult UserType()
        {
            return View();
        }

        public ActionResult GetUserTypeList()
        {
            List<UserType> UserTypeList = _hrms.UserTypes.ToList<UserType>();
            var result = UserTypeList.Select(S => new {
                Id = S.Id,
                Name = S.Name

            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditUserType(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.UserTypes.Where(x => x.Id == id).FirstOrDefault<UserType>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddUserType(UserType obj)
        {
            _hrms.UserTypes.Add(obj);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult UpdateUserType(UserType obj)
        {
            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteUserType(int id)
        {

            UserType UType = _hrms.UserTypes.Where(x => x.Id == id).FirstOrDefault<UserType>();
            _hrms.UserTypes.Remove(UType);
            _hrms.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

        }

        #endregion end userType
    }
}