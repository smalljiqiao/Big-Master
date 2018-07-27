using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BM.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //BM.Services.WebData.DetailedBatch.Handler.Get("测试", DateTime.Now.AddYears(-1), true);

            //BM.Services.WebData.Dream.Handler.InsertTitleToDb();
            //BM.Services.WebData.Dream.Handler.InsertDetailToDb();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
