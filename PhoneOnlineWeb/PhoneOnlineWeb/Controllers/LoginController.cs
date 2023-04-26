using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneOnlineWeb.Models;

namespace PhoneOnlineWeb.Controllers
{
    public class LoginController : Controller
    {
        SHOPMobileDatabaseEntities _db = new SHOPMobileDatabaseEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(User _user)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email)
            && s.Password.Equals(_user.Password)).FirstOrDefault();
            if (check == null)
            {
                _user.LoginErrorMessage = "Email hoặc mật khẩu không đúng! Vui lòng nhập lại!";
                return View("Index", _user);
            }
            else
            {
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (test.Email != "admin@gmail.com")
                {
                    Session["IDUser"] = _user.IDUser;
                    Session["Email"] = _user.Email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã được sử dụng! Vui lòng dùng email khác!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}