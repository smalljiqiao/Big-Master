using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace BigMasterApp.App_Start
{
    public class AppHelper
    {
        /// <summary>
        /// 获取Url地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAbsoluteUriSite(HttpRequestBase request)
        {
            return new StringBuilder()
              .Append(request.Url.Scheme)
              .Append("://")
              .Append(request.Url.Host)
              .ToString();
        }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        /// <returns></returns>
        public static string GetImgServiceRoot() {
          return  ConfigurationManager.AppSettings["imgServiceRoot"];
        }
        /// <summary>
        /// 接口地址
        /// </summary>
        /// <returns></returns>
        public static string GetApiServiceRoot()
        {
            return ConfigurationManager.AppSettings["apiServiceRoot"];
        }
    }


}