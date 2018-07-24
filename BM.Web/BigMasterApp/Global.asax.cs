using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BigMasterApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册所有Area
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //注册路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册静态压缩文件
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
