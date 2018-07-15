using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigMasterApp.Controllers
{
    public class PayFileController : Controller
    {
        // GET: PayFile
        public ActionResult EightGoodWordPay()
        {
            ViewBag.Message = "八字详批支付";

            return View();
        }
        public ActionResult BabyNamePay()
        {
            ViewBag.Message = "宝宝取名支付";
            return View();
        }
        public ActionResult EightMarryPay()
        {
            ViewBag.Message = "八字合婚支付";
            return View();
        }
    }
}