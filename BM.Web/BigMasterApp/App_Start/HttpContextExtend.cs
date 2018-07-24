using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BigMasterApp.App_Start
{
    public static class HttpContextExtend
    {
        public const string WeixinFlag = "micromessenger";
        /// <summary>
        /// 判断是否是微信端
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsWeixin(this HttpContext context)
        {
            if (context != null)
            {
                string userAgent = context.Request.Headers["User-Agent"];
                userAgent = userAgent ?? "";
                if (userAgent.ToLower().Contains(WeixinFlag))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetAllUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Url.Scheme)
                .Append("://")
                .Append(request.Url.Host)
                .Append(request.Url.Port)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        public static string GetSiteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Url.Scheme)
                .Append("://")
                .Append(request.Url.Host)
                .Append(request.Url.Port)
                .ToString();
        }
        public static string GetUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault().ToString();
            if (string.IsNullOrEmpty(ip))
            {
               // ip =context.Trace.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}