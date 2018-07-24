using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigMasterApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 八字详批视图
        /// </summary>
        /// <returns></returns>
        public ActionResult EightGoodWord() {

            ViewBag.Title = "八字详批";
            return View();
        }

        public ActionResult BabyName()
        {
            ViewBag.Message = "宝宝取名";

            return View();
        }

        public ActionResult EightMarry()
        {
            ViewBag.Message = "八字合婚";

            return View();
        }

        public ActionResult ReadDream()
        {
            ViewBag.Message = "八字合婚";
            return View();
        }

        public ActionResult ReadDreamItem()
        {
            ViewBag.Message = "";
            return View();
        }
        public ActionResult Mine()
        {
            ViewBag.Message = "";
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        /// <summary>
        /// 注册页和修改密码页
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterUpsetPwd()
        {
            ViewBag.Message = "";
            return View();
        }
        public ActionResult EditUserMessage(){
            ViewBag.Message = "";
            return View();
        }
        public ActionResult OrderList()
        {
            ViewBag.Message = "";
            return View();
        }
    }
}