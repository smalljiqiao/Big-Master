using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BM.Data.Domain;
using BM.Services.Data;
using BM.Services.Data.Androids;
using BM.Services.Data.ShortMessages;
using BM.Services.Data.Users;
using BM.Services.Infrastructure;

namespace BM.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //BM.Services.WebData.DetailedBatch.Handler.Get("测试", DateTime.Now.AddYears(-1), true);

            //BM.Services.WebData.Dream.Handler.InsertTitleToDb();
            //BM.Services.WebData.Dream.Handler.InsertDetailToDb();

            //注册数据库上下文类为单例模式
            Singleton<DbEntities>.Instance = new DbEntities();

            Singleton<UserService>.Instance = new UserService();
            Singleton<AndroidService>.Instance = new AndroidService();
            Singleton<SmsService>.Instance = new SmsService();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
