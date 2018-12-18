using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravaliaMvc.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            ViewBag.msg = "This Msg From Viewbag";
            ViewData["a"]= "This Msg From ViewData";
            TempData["b"]= "This Msg From TempData";
            return View();
        }
        public ActionResult SecoundPage()
        {
            return View();
        }
    }
}