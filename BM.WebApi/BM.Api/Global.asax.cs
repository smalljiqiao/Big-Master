using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BM.Api.BMModelBinders;

namespace BM.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var v = Regex.Match("2018-07-20", @"[12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])").Success;

            


            //为string类型指定stringTrimBinder
            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
