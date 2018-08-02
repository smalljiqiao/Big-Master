using BM.Services.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BM.Services.Data.BurialPoint;
using BM.Services.Data.Logs;

namespace BM.Services.WebData.DetailedBatch
{
    /// <summary>
    /// 八字详批功能
    /// </summary>
    public static class Handler
    {
        private const string Url = "http://bz.99166.com/result.asp";

        /// <summary>
        /// 获取八字详批页面HTML
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="birthDay">出生日期</param>
        /// <param name="isMan">是否为男性</param>
        /// <returns></returns>
        private static string Html(string userName, DateTime birthDay, bool isMan = true)
        {
            //性别参数，1代表女性；2代表男性

            //获取年月日，当月和日为单数时，在前面加0
            var year = birthDay.Year;
            var month = birthDay.Month.ToString().Length == 1 ? "0" + birthDay.Month : birthDay.Month.ToString();
            var day = birthDay.Day.ToString().Length == 1 ? "0" + birthDay.Day : birthDay.Day.ToString();

            var userNameEn = System.Web.HttpUtility.UrlEncode(userName, Encoding.GetEncoding("GB2312"))?.ToUpper();
            var sex = isMan ? 2 : 1;

            var postData =
                "StrtypeYear=1&StrtypeTime=2&cY=115&cM=972&cD=29229&cH=350634&StrName=" + userNameEn + "&StrSex=" +
                sex + "&StrYear1=" + year + "&StrMonth=" + month + "&StrDay=" + day + "&StrTime=%B2%BB%C7%E5%B3%FE";

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
                    Referer = "http://bz.99166.com/",
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
        /// 解析HTML
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="birthDay">出生日期</param>
        /// <param name="isMan">是否为男性</param>
        /// <returns></returns>
        public static object Get(string userName, DateTime birthDay, bool isMan = true)
        {
            var html = Html(userName, birthDay, isMan);
            if (string.IsNullOrEmpty(html))
                return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var title = string.Empty; //标题

            var fDic = new List<KeyValuePair<string, string>>(); //八字命盘个人信息
            var fHtml = ""; //八字命盘 命柱HTML
            var fList1 = new List<string>(); //八字命盘流年
            var fList2 = new List<string>(); //八字命盘流年
            var fList3 = new List<string>(); //八字命盘流年
            var fList4 = new List<string>(); //八字命盘流年

            var sList = new List<string>(); //八字论命内容数组

            var tHtml = ""; //五行分析HTML
            var tList = new List<string>(); //五行个性分析

            var fuType = ""; //八行命卦分析类型
            var fuImgUrl = ""; //八行命卦分析图片链接
            var fuDic = new List<List<string>>(); //八行命卦分析凶吉
            var fuHtml = ""; //八行命卦分析吉位选择HTML

            var fiList = new List<string>(); //八字算命爱情内容

            var siList = new List<string>(); //八字算命财运事业

            var seList = new List<string>(); //八字算命健康

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

                    #region 八字命盘

                    if (title.IndexOf("八字命盘", StringComparison.Ordinal) != -1)
                    {
                        var tableArr = box.SelectNodes("table");
                        if (tableArr == null)
                            continue;

                        //IN THE NORMAL CASE:tableArr.Count = 4

                        if (tableArr.Count < 4)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                            continue;
                        }

                        if (tableArr.Count > 4)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                        }

                        #region 个人信息

                        var fTableArr = tableArr[0].SelectNodes("tr//td//table");
                        if (fTableArr == null)
                            continue;

                        //IN THE NORMAL CASE:fTableArr.Count = 3

                        if (fTableArr.Count < 3)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                            continue;
                        }

                        if (fTableArr.Count > 3)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                        }

                        var imgUrl = fTableArr[0].Attributes["background"].Value;

                        fDic.Add(new KeyValuePair<string, string>("imgUrl", imgUrl));

                        var contentTrArr = fTableArr[2].SelectNodes("tr");
                        if (contentTrArr != null)
                        {
                            foreach (var tr in contentTrArr)
                            {
                                var contentTdArr = tr.SelectNodes("td");
                                if (contentTdArr != null)
                                {
                                    for (var i = 0; i < contentTdArr.Count; i++)
                                    {
                                        var key = contentTdArr[i].InnerText.Trim();
                                        var value = contentTdArr[i + 1].InnerText.Replace("&nbsp;", "")
                                            .Replace("\r", "").Replace("\n", "").Trim();

                                        fDic.Add(new KeyValuePair<string, string>(key, value));

                                        i += 1;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 命柱

                        fHtml = tableArr[1].OuterHtml;

                        #endregion

                        #region 流年

                        var trArr = tableArr[3].ChildNodes.Where(node => node.Name == "tr");

                        //IN THE NORMAL CASE:trArr.Count = 4

                        if (trArr.Count() < 4)
                        {
                            //TODO LOG INNORMAL
                            continue;
                        }

                        if (trArr.Count() > 4)
                        {
                            //TODO LOG INNORMAL
                        }

                        var tdCount = trArr.ElementAt(0).ChildNodes.Count(node => node.Name == "td");
                        var tdArr1 = trArr.ElementAt(0).ChildNodes.Where(node => node.Name == "td");
                        var tdArr2 = trArr.ElementAt(1).ChildNodes.Where(node => node.Name == "td");
                        var tdArr3 = trArr.ElementAt(2).ChildNodes.Where(node => node.Name == "td");
                        var tdArr4 = trArr.ElementAt(3).ChildNodes.Where(node => node.Name == "td");

                        for (var i = 0; i < tdCount; i++)
                        {
                            fList1.Add(tdArr1.ElementAt(i).InnerText.Trim());
                            fList2.Add(tdArr2.ElementAt(i).InnerText.Trim());

                            var img1 = tdArr3.ElementAt(i).SelectSingleNode("img");
                            var imgUrl1 = "";
                            if (img1 == null)
                            {
                                if (i == 0)
                                    fList3.Add(tdArr3.ElementAt(i).InnerText.Trim());

                                //TODO SET DEFAULT IMG URL
                            }
                            else
                            {
                                imgUrl1 = "" + img1.Attributes["src"].Value?.Split('/').LastOrDefault();
                                fList3.Add(imgUrl1);
                            }



                            var img2 = tdArr4.ElementAt(i).SelectSingleNode("img");
                            var imgUrl2 = "";
                            if (img2 == null)
                            {
                                if (i == 0)
                                    fList4.Add(tdArr4.ElementAt(i).InnerText.Trim());

                                //TODO SET DEFAULT IMG URL
                            }
                            else
                            {
                                imgUrl2 = "http://47.106.183.2:8887/Images/" +
                                          img1.Attributes["src"].Value?.Split('/').LastOrDefault();
                                fList4.Add(imgUrl2);
                            }
                        }

                        #endregion
                    }


                    #endregion

                    #region 八字论命

                    if (title.IndexOf("八字论命", StringComparison.Ordinal) != -1)
                    {
                        sList.AddRange(box.ChildNodes.Select(p => p.InnerText.Trim())
                            .Where(text => text.Replace("\r", "").Replace("\n", "").Trim() != ""));
                    }

                    #endregion

                    #region 五行分析

                    if (title.IndexOf("八字论命", StringComparison.Ordinal) != -1)
                    {
                        var tableArr = box.ChildNodes.Where(node => node.Name == "table");
                        if (!tableArr.Any())
                        {
                            BpService.Use(html);
                            continue; //TODO LOG
                        }

                        //IN NORMAL CASE:tableArr.Count = 2

                        if (tableArr.Count() < 2)
                        {
                            //TODO LOG INNORMAL 
                            BpService.Use(html);
                            continue;
                        }

                        if (tableArr.Count() > 2)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                        }

                        tHtml = tableArr.Aggregate(tHtml, (current, table) => current + table.OuterHtml.Trim());
                    }

                    #endregion

                    #region 五行个性分析

                    if (title.IndexOf("五行个性分析", StringComparison.Ordinal) != -1)
                    {
                        var childText = string.Empty;
                        var childNodes = box.ChildNodes;

                        foreach (var node in childNodes)
                        {
                            if (node.Name == "br")
                            {
                                tList.Add(childText);
                                childText = "";
                            }
                            else
                                childText += node.InnerText.Replace("&nbsp;", "").Trim();
                        }

                        if (childText != "")
                            tList.Add(childText);
                    }

                    #endregion

                    #region 八字命卦分析

                    if (title.IndexOf("八字命卦分析", StringComparison.Ordinal) != -1)
                    {
                        var type = box.SelectSingleNode("b//font")?.InnerText.Trim();
                        var imgUrl = box.SelectSingleNode("img[@class='imgs']")?.Attributes["src"].Value.Trim();

                        fuType = type;
                        fuImgUrl = imgUrl;

                        var trArr = box.SelectNodes("table//tr");

                        if (trArr == null)
                            continue;

                        fuDic.AddRange(trArr.Select(tr => new List<string>
                        {
                            tr.SelectNodes("td").ElementAt(0)?.InnerText.Replace("&darr;", "").Trim(),
                            tr.SelectNodes("td").ElementAt(1)?.InnerText.Replace("&darr;", "").Trim()
                        }));

                        var flag = true;
                        var childNodes = box.ChildNodes;

                        var childHtml = "";

                        foreach (var child in childNodes)
                        {
                            if (child.Name == "hr")
                                flag = false;

                            if (flag)
                                continue;

                            if (child.Name == "img")
                            {
                                var img = child.Attributes["src"].Value.Trim();
                                if (img != "")
                                    childHtml += "<img src='" + img + "'/>";
                            }
                            else
                            {
                                if (child.InnerText.Trim() != "")
                                    childHtml += "<span>" + child.InnerText.Trim() + "</span>";
                            }

                            if (child.Name == "br")
                            {
                                fuHtml += "<p>" + childHtml + "</p>";
                                childHtml = "";
                            }
                        }
                    }

                    #endregion

                    #region 八字算命爱情

                    if (title.IndexOf("八字算命爱情", StringComparison.Ordinal) != -1)
                    {
                        var childNode = box.ChildNodes;

                        foreach (var child in childNode)
                        {
                            if (child.Name == "p")
                            {
                                if (child.InnerText.IndexOf("强烈推荐", StringComparison.Ordinal) != -1)
                                    break;

                                if (child.InnerText.Trim() != "")
                                {
                                    if (child.InnerHtml.IndexOf("<br>", StringComparison.Ordinal) != -1)
                                    {
                                        var text = "";
                                        foreach (var item in child.ChildNodes)
                                        {
                                            switch (item.Name.ToLower())
                                            {
                                                case "#text":
                                                    text += item.InnerText.Replace("&nbsp;", "").Trim();
                                                    break;
                                                case "br":
                                                    if (text != "")
                                                        fiList.Add(text);
                                                    text = "";
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var rText = child.InnerText.Replace("&nbsp;", "").Trim();

                                        if (rText != "")
                                            fiList.Add(rText);
                                    }
                                }
                            }

                        }
                    }

                    #endregion

                    #region 八字算命财运事业

                    if (title.IndexOf("八字算命财运事业", StringComparison.Ordinal) != -1)
                    {
                        var childNode = box.ChildNodes;

                        foreach (var child in childNode)
                        {
                            if (child.Name == "p")
                            {
                                if (child.InnerText.IndexOf("强烈推荐", StringComparison.Ordinal) != -1)
                                    break;

                                if (child.InnerText.Trim() != "")
                                {
                                    if (child.InnerHtml.IndexOf("<br>", StringComparison.Ordinal) != -1)
                                    {
                                        var text = "";
                                        foreach (var item in child.ChildNodes)
                                        {
                                            switch (item.Name.ToLower())
                                            {
                                                case "#text":
                                                    text += item.InnerText.Replace("&nbsp;", "").Trim();
                                                    break;
                                                case "br":
                                                    if (text != "")
                                                        siList.Add(text);
                                                    text = "";
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var rText = child.InnerText.Replace("&nbsp;", "").Trim();

                                        if (rText != "")
                                            siList.Add(rText);
                                    }
                                }
                            }

                        }
                    }

                    #endregion

                    #region 八字算命健康

                    if (title.IndexOf("八字算命健康", StringComparison.Ordinal) != -1)
                    {
                        var childNode = box.ChildNodes;

                        foreach (var child in childNode)
                        {
                            if (child.Name == "p")
                            {
                                if (child.InnerText.IndexOf("强烈推荐", StringComparison.Ordinal) != -1)
                                    break;

                                if (child.InnerText.Trim() != "")
                                {
                                    if (child.InnerHtml.IndexOf("<br>", StringComparison.Ordinal) != -1)
                                    {
                                        var text = "";
                                        foreach (var item in child.ChildNodes)
                                        {
                                            switch (item.Name.ToLower())
                                            {
                                                case "#text":
                                                    text += item.InnerText.Replace("&nbsp;", "").Trim();
                                                    break;
                                                case "br":
                                                    if (text != "")
                                                        seList.Add(text);
                                                    text = "";
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var rText = child.InnerText.Replace("&nbsp;", "").Trim();

                                        if (rText != "")
                                            seList.Add(rText);
                                    }
                                }
                            }

                        }
                    }

                    #endregion
                }
            }

            var dic1 = new Dictionary<string, object>
            {
                {"PersonalInfo", fDic},
                {"Life", fHtml},
                {"ByGone", new List<object> {fList1, fList2, fList3, fList4}},
            };

            var dic2 = new Dictionary<string, object>
            {
                {"Html", tHtml},
                {"Personality", tList}
            };

            var dic3 = new Dictionary<string, object>
            {
                { "Type",fuType},
                { "ImgUrl",fuImgUrl},
                { "Komo",fuDic},
                { "LuckySite",fuHtml}
            };

            var resultDic =
                new Dictionary<string, object>
                {
                    {"Natal", dic1},
                    {"Fate", sList},
                    {"Five", dic2},
                    {"Destiny", dic3},
                    {"Love", fiList},
                    {"Money", siList},
                    {"Healthy", seList}
                };

            return resultDic;
        }

        /// <summary>
        /// 解析HTML,返回Html
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="birthDay">出生日期</param>
        /// <param name="isMan">是否为男性</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetHtml(string userName, DateTime birthDay, bool isMan = true)
        {
            var html = Html(userName, birthDay, isMan);
            if (string.IsNullOrEmpty(html))
                return null;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var title = string.Empty; //标题

            var fHtml = ""; //八字命盘HTML

            var sHtml = ""; //八字论命HTML

            var tHtml = ""; //五行分析
            var tScript = ""; //五行分析Script

            var fuHtml = ""; //五行个性分析

            var fiHtml = ""; //八字命卦分析

            var siHtml = ""; //八字算命爱情

            var seHtml = ""; //八字算命财运

            var eiHtml = ""; //八字算命健康

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

                    #region 八字命盘

                    if (title.IndexOf("八字命盘", StringComparison.Ordinal) != -1)
                    {
                        var tableArr = box.SelectNodes("table");
                        if (tableArr == null)
                            continue;

                        //IN THE NORMAL CASE:tableArr.Count = 4

                        if (tableArr.Count < 4)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                            continue;
                        }

                        if (tableArr.Count > 4)
                        {
                            //TODO LOG INNORMAL
                            BpService.Use(html);
                        }

                        fHtml += tableArr[0].OuterHtml.Trim() + tableArr[1].OuterHtml.Trim() +
                                 tableArr[2].OuterHtml.Trim() +
                                 tableArr[3].OuterHtml.Trim();
                    }


                    #endregion

                    #region 八字论命

                    if (title.IndexOf("八字论命", StringComparison.Ordinal) != -1)
                    {
                        sHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 五行分析

                    if (title.IndexOf("五行分析", StringComparison.Ordinal) != -1)
                    {
                        tHtml = box.ChildNodes.Where(child => child.Name.ToLower() == "table").Aggregate(tHtml, (current, child) => current + child.OuterHtml);

                        var scriptNode = box.NextSibling;

                        while (true)
                        {
                            if (scriptNode.Name != "script" && scriptNode.Name != "div")
                                scriptNode = scriptNode.NextSibling;
                            else
                                break;
                        }

                        if (scriptNode.Name == "script")
                        {
                            tScript = scriptNode.InnerHtml;
                        }
                    }

                    #endregion

                    #region 五行个性分析

                    if (title.IndexOf("五行个性分析", StringComparison.Ordinal) != -1)
                    {
                        fuHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 八字命卦分析

                    if (title.IndexOf("八字命卦分析", StringComparison.Ordinal) != -1)
                    {
                        fiHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 八字算命爱情

                    if (title.IndexOf("八字算命爱情", StringComparison.Ordinal) != -1)
                    {
                        siHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 八字算命财运事业

                    if (title.IndexOf("八字算命财运事业", StringComparison.Ordinal) != -1)
                    {
                        seHtml = box.OuterHtml.Trim();
                    }

                    #endregion

                    #region 八字算命健康

                    if (title.IndexOf("八字算命健康", StringComparison.Ordinal) != -1)
                    {
                        eiHtml = box.OuterHtml.Trim();
                    }

                    #endregion
                }
            }

            var resultDic =
                new Dictionary<string, object>
                {
                    {"Natal", fHtml},  //八字命盘
                    {"Fate", sHtml}, //八字论命
                    {"Five", tHtml}, //五行分析
                    {"FivecScript", tScript}, //五行分析Script
                    {"FivePersonality", fuHtml}, //五行个性分析
                    {"Destiny", fiHtml}, //八字命卦
                    {"Love", siHtml}, //爱情
                    {"Money", seHtml}, //财运
                    {"Healthy", eiHtml} //八字算命健康
                };

            return resultDic;
        }
    }
}
