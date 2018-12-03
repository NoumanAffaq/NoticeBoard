using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoticeBoard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User == null)
            {
                ViewBag.Admin = 0;
            }
            else
            {
                ViewBag.Admin = 1;
            }
            return View();
        }

        public ActionResult About()
        {
            if (User == null)
            {
                ViewBag.Admin = 0;
            }
            else
            {
                ViewBag.Admin = 1;
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult PostNotice()
        {
            if (User == null)
            {
                ViewBag.Admin = 0;
            }
            else
            {
                ViewBag.Admin = 1;
            }
            return View();
        }

        public ActionResult Contact()
        {
            if (User == null)
            {
                ViewBag.Admin = 0;
            }
            else
            {
                ViewBag.Admin = 1;
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}