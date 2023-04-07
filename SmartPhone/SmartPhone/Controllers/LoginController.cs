using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPhone.Models;

namespace SmartPhone.Controllers
{
    public class LoginController : Controller
    {
        SmartPhoneDBEntities Db = new SmartPhoneDBEntities();
        // GET: Login
        public ActionResult Index()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Authen(Account account)
        {
            var check = Db.Accounts.Where(s => s.Email.Equals(account.Email) && s.Password.Equals(account.Password)).FirstOrDefault();
            if(check == null)
            {
                account.LoginErrorMessage = "Email hoặc mật khẩu không đúng! Vui lòng nhập lại!";
                return View("Index",account);
            }
            else
            {
                Session["AccountID"] = account.AccountID;
                Session["Email"] = account.Email;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                var check = Db.Accounts.FirstOrDefault(s => s.Email == account.Email);
                if(check == null)
                {
                    Db.Configuration.ValidateOnSaveEnabled = false;
                    Db.Accounts.Add(account);
                    Db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại! Vui lòng sử dụng email khác!";
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