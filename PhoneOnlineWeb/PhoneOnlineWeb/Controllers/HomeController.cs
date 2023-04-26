using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneOnlineWeb.Models;

namespace PhoneOnlineWeb.Controllers
{
    public class HomeController : Controller
    {
        SHOPMobileDatabaseEntities _db = new SHOPMobileDatabaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Products.ToList());
        }
        public ActionResult DetailPro(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}