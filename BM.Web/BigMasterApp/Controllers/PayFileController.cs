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
            ViewBag.Message = "订单支付";

            return View();
        }
        public ActionResult BabyNamePay()
        {
            ViewBag.Message = "订单支付";
            return View();
        }
    }
}