using BM.Services.Common;
using System;
using System.Text;

namespace BM.Services.WebData.DetailedBatch
{
    /// <summary>
    /// 八字详批功能
    /// </summary>
    public static class Handler
    {
        private const string Url = "http://bz.99166.com/result.asp";

        /// <summary>
        /// 获取八字详批页面内容
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="birthDay">出生日期</param>
        /// <param name="isMan">是否为男性</param>
        /// <returns></returns>
        public static string Get(string userName, DateTime birthDay, bool isMan = true)
        {
            //性别参数，1代表女性；2代表男性

            //获取年月日，当月和日为单数时，在前面加0
            var year = birthDay.Year;
            var month = birthDay.Month.ToString().Length == 1 ? "0" + birthDay.Month : birthDay.Month.ToString();
            var day = birthDay.Day.ToString().Length == 1 ? "0" + birthDay.Day : birthDay.Day.ToString();

            var userNameEn = System.Web.HttpUtility.UrlEncode(userName, Encoding.UTF8);
            var sex = isMan ? 2 : 1;

            var postData =
                "StrtypeYear=1&StrtypeTime=2&cY=115&cM=972&cD=29229&cH=350634&StrName=" + userNameEn + "&StrSex=" +
                sex + "&StrYear1=" + year + "&StrMonth=" + month + "&StrDay=" + day + "&StrTime=%B2%BB%C7%E5%B3%FE";

            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = Url,//URL     必需项  
                Method = "POST",//URL     可选项 默认为Get  
                Postdata = postData,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",//    可选项有默认值  
                Referer = "http://bz.99166.com/",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            var result = http.GetHtml(item);

            var html = result.Html;

            return html;
        }
    }
}
