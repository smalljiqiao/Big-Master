using System;
using System.Collections.Generic;
using System.Text;
using BM.Services.Common;
using BM.Services.Data.Logs;
using HtmlAgilityPack;

namespace BM.Services.WebData.Marriage
{
    /// <summary>
    /// 八字合婚功能
    /// </summary>
    public static class Handler
    {
        private const string Url = "http://bz.99166.com/bzhh/result.asp";

        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="userNameMan">男方姓名</param>
        /// <param name="birthDayMan">男方出生日期</param>
        /// <param name="timeMan">男方时辰</param>
        /// <param name="userNameWoman">女方姓名</param>
        /// <param name="birthDayWoman">女方出生日期</param>
        /// <param name="timeWoman">女方时辰</param>
        /// <returns></returns>
        private static string Html(string userNameMan, DateTime birthDayMan, string timeMan, string userNameWoman,
            DateTime birthDayWoman, string timeWoman)
        {
            //TODO VALID time 

            //timeMan = timeWoman = "不清楚";

            var yearMan = birthDayMan.Year;
            var monthMan = birthDayMan.Month.ToString().Length == 1
                ? "0" + birthDayMan.Month
                : birthDayMan.Month.ToString();
            var dayMan = birthDayMan.Day.ToString().Length == 1 ? "0" + birthDayMan.Day : birthDayMan.Day.ToString();

            var yearWoman = birthDayWoman.Year;
            var monthWoman = birthDayWoman.Month.ToString().Length == 1
                ? "0" + birthDayMan.Month
                : birthDayMan.Month.ToString();
            var dayWoman = birthDayWoman.Day.ToString().Length == 1
                ? "0" + birthDayMan.Day
                : birthDayMan.Day.ToString();

            var userNameManEn = System.Web.HttpUtility.UrlEncode(userNameMan, Encoding.GetEncoding("GB2312"))
                ?.ToUpper();
            var userNameWomanEn = System.Web.HttpUtility.UrlEncode(userNameWoman, Encoding.GetEncoding("GB2312"))
                ?.ToUpper();
            var timeManEn = System.Web.HttpUtility.UrlEncode(timeMan, Encoding.GetEncoding("GB2312"))?.ToUpper();
            var timeWomanEn = System.Web.HttpUtility.UrlEncode(timeWoman, Encoding.GetEncoding("GB2312"))?.ToUpper();


            var postData = "UserName=" + userNameManEn + "&Sex=M&iYear=" + yearMan + "&iMonth=" + monthMan + "&iDay=" +
                           dayMan + "&iHour=" + timeManEn + "&UserNameB=" + userNameWomanEn + "&SexB=F&iYearB=" +
                           yearWoman + "&iMonthB=" + monthWoman + "&iDayB=" + dayWoman + "&iHourB=" + timeWomanEn;

            var html = string.Empty;

            try
            {
                var http = new HttpHelper();
                var item = new HttpItem()
                {
                    URL = Url, //URL     必需项  
                    Method = "POST", //URL     可选项 默认为Get  
                    Postdata = postData,
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36", //用户的浏览器类型，版本，操作系统     可选项有默认值  
                    Accept =
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8", //    可选项有默认值  
                    Referer = "http://bz.99166.com/bzhh/",
                    ContentType = "application/x-www-form-urlencoded", //返回类型    可选项有默认值  
                    ResultType = ResultType.String, //返回数据类型，是Byte还是String  
                };
                var result = http.GetHtml(item);
                html = result.Html;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
            }

            return html;
        }

        /// <summary>
        /// 解释Html，返回html
        /// </summary>
        /// <param name="userNameMan"></param>
        /// <param name="birthDayMan"></param>
        /// <param name="timeMan"></param>
        /// <param name="userNameWoman"></param>
        /// <param name="birthDayWoman"></param>
        /// <param name="timeWoman"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetHtml(string userNameMan, DateTime birthDayMan, string timeMan,
            string userNameWoman, DateTime birthDayWoman, string timeWoman)
        {
            var html = Html(userNameMan, birthDayMan, timeMan, userNameWoman, birthDayWoman, timeWoman);
            if (string.IsNullOrEmpty(html))
                return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var title = string.Empty; //标题

            var fHtml = ""; //八字命盘分析
            var sHtml = ""; //男命八字爱情分析
            var tHtml = ""; //女命八字爱情分析
            var fuHtml = ""; //八字婚配提点

            var boxConArr = doc.DocumentNode.SelectNodes("//div[@class='boxcon']");
            if (boxConArr != null)
            {
                foreach (var box in boxConArr)
                {
                    var pre = box.PreviousSibling;

                    while (true)
                    {
                        if (pre.Name != "div")
                            pre = pre.PreviousSibling;
                        else
                            break;
                    }

                    title = pre.InnerText.Trim();

                    if (title.IndexOf("强烈推荐", StringComparison.Ordinal) != -1)
                        break;

                    #region 八字命盘分析

                    if (title.IndexOf("八字命盘分析", StringComparison.Ordinal) != -1)
                    {
                        fHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 男命八字爱情分析

                    if (title.IndexOf("男命八字爱情分析", StringComparison.Ordinal) != -1)
                    {
                        sHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 女命八字爱情分析

                    if (title.IndexOf("女命八字爱情分析", StringComparison.Ordinal) != -1)
                    {
                        tHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 八字婚配提点

                    if (title.IndexOf("八字婚配提点", StringComparison.Ordinal) != -1)
                    {
                        fuHtml = box.OuterHtml.Trim();
                    }

                    #endregion
                }
            }

            var resultDic =
                new Dictionary<string, object>
                {
                    {"Analysis", fHtml},  //八字命盘分析
                    {"ManAnalysis", sHtml}, //男命八字爱情分析
                    {"WomanAnalysis", tHtml}, //女命八字爱情分析
                    {"suggestion", fuHtml}, //八字婚配提点
                };

            return resultDic;
        }
    }
}
