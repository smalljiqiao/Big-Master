using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BM.Services.Common;
using BM.Services.Data.Logs;
using HtmlAgilityPack;

namespace BM.Services.WebData.BabyName
{
    /// <summary>
    /// 宝宝取名功能
    /// </summary>
    public static class Handler
    {
        private const string Url = "http://xm.99166.com/bbqmdq/result.asp";

        /// <summary>
        /// 获取宝宝取名HTML
        /// </summary>
        /// <param name="surname">姓氏</param>
        /// <param name="birthDay">出生日期 yyyy-MM-dd HH:mm</param>
        /// <param name="isMan">是否为男性</param>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <param name="type">名字类型 reportDan(单字) reportDie(叠字) report(双字)</param>
        /// <returns></returns>
        private static string Html(string surname, DateTime birthDay, bool isMan, string province, string city, int type)
        {
            //性别参数，2代表女性；1代表男性

            //名字形式 0:单字 1:双子 2:叠字

            var nameType = "";

            switch (type)
            {
                case 0:
                    nameType = "reportDan";
                    break;
                case 2:
                    nameType = "reportDie";
                    break;
                case 1:
                default:
                    nameType = "report";
                    break;
            }

            //获取年月日，当月和日为单数时，在前面加0
            var year = birthDay.Year;
            var month = birthDay.Month.ToString().Length == 1 ? "0" + birthDay.Month : birthDay.Month.ToString();
            var day = birthDay.Day.ToString().Length == 1 ? "0" + birthDay.Day : birthDay.Day.ToString();
            var hour = birthDay.Hour.ToString().Length == 1 ? "0" + birthDay.Hour : birthDay.Hour.ToString();
            var minute = birthDay.Minute.ToString().Length == 1 ? "0" + birthDay.Minute : birthDay.Minute.ToString();

            var surnameEn = System.Web.HttpUtility.UrlEncode(surname, Encoding.GetEncoding("GB2312"))?.ToUpper();
            var provinceEn = System.Web.HttpUtility.UrlEncode(province, Encoding.GetEncoding("GB2312"))?.ToUpper();
            var cityEn = System.Web.HttpUtility.UrlEncode(city, Encoding.GetEncoding("GB2312"))?.ToUpper();

            var sex = isMan ? 1 : 2;

            var postData = "cY=115&cM=972&cD=29229&cH=350628&userSurname=" + surnameEn + "&userSex=" + sex + "&year=" +
                           year + "&month=" + month + "&day=" + day + "&hour=" + hour + "&minute=" + minute +
                           "&ddlProvince=" + provinceEn + "&ddlCity=" + cityEn + "&searchType=" + nameType;

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
                    Referer = "http://xm.99166.com/bbqmdq/",
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
        /// 解析HTML,返回Html
        /// </summary>
        /// <param name="surname">姓氏</param>
        /// <param name="birthDay">出生日期 yyyy-MM-dd HH:mm</param>
        /// <param name="isMan">是否为男性</param>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <param name="type">名字类型 reportDan(单字) reportDie(叠字) report(双字)</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetHtml(string surname, DateTime birthDay, bool isMan, string province, string city, int type)
        {
            var html = Html(surname, birthDay, isMan, province, city, type);
            if (string.IsNullOrEmpty(html))
                return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var fHtml = ""; // 个人资料
            var sList = new List<string>(); // 推荐姓名数组


            var pleftmainDiv = doc.DocumentNode.SelectSingleNode("//div[@class='pleftmain']");

            var divArr = pleftmainDiv.SelectNodes("div");

            //In normal case divArr.Count == 3
            if (divArr.Count >= 3)
            {
                fHtml = divArr[1].InnerHtml.Trim();

                var aArr = divArr[2].SelectNodes("div//ul//li//a");

                foreach (var a in aArr)
                {
                    if (a != null && !string.IsNullOrEmpty(a.InnerText))
                    {
                        sList.Add(a.InnerText.Replace("·", "").Trim());
                    }
                }
            }

            var dic = new Dictionary<string, object>
            {
                { "personalInfo",fHtml},
                { "suggestName",sList}
            };

            return dic;
        }
    }
}
