using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigMasterApp.Controllers
{
    public class PayResultController : Controller
    {
        // GET: PayResult
        public ActionResult BabyNameForecastResult()
        {
            ViewBag.Title = "测算结果";
            return View();
        }
        public ActionResult ReadDreamResult()
        {
            ViewBag.Title = "";
            return View();
        }
        public ActionResult EightGoodWordResult() {
            ViewBag.Title = "";
            return View();
        }
    }
}