using BM.Services.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //TODO Log Exception
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

            var FDic = new List<KeyValuePair<string, string>>(); //八字命盘个人信息
            var FHtml = ""; //八字命盘 命柱HTML
            var FList1 = new List<string>(); //八字命盘流年
            var FList2 = new List<string>(); //八字命盘流年
            var FList3 = new List<string>(); //八字命盘流年
            var FList4 = new List<string>(); //八字命盘流年

            var SList = new List<string>(); //八字论命内容数组

            var THtml = ""; //五行分析HTML

            var TList = new List<string>(); //五行个性分析

            var FuType = ""; //八行命卦分析类型
            var FuImgUrl = ""; //八行命卦分析图片链接
            var FuDic = new List<List<string>>(); //八行命卦分析凶吉
            var FuDic2 = new List<KeyValuePair<string, string>>(); //八行命卦分析吉位选择

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
                            continue;
                        }

                        if (tableArr.Count > 4)
                        {
                            //TODO LOG INNORMAL
                        }

                        #region 个人信息

                        var fTableArr = tableArr[0].SelectNodes("tr//td//table");
                        if (fTableArr == null)
                            continue;

                        //IN THE NORMAL CASE:fTableArr.Count = 3

                        if (fTableArr.Count < 3)
                        {
                            //TODO LOG INNORMAL
                            continue;
                        }

                        if (fTableArr.Count > 3)
                        {
                            //TODO LOG INNORMAL
                        }

                        var imgUrl = fTableArr[0].Attributes["background"].Value;

                        FDic.Add(new KeyValuePair<string, string>("imgUrl", imgUrl));

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
                                        var value = contentTdArr[i + 1].InnerText.Replace("&nbsp;", "").Trim();

                                        FDic.Add(new KeyValuePair<string, string>(key, value));

                                        i += 1;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 命柱

                        FHtml = tableArr[1].OuterHtml;

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
                            FList1.Add(tdArr1.ElementAt(i).InnerText.Trim());
                            FList2.Add(tdArr2.ElementAt(i).InnerText.Trim());

                            var img1 = tdArr3.ElementAt(i).SelectSingleNode("img");
                            var imgUrl1 = "";
                            if (img1 == null)
                            {
                                if (i == 0)
                                    FList3.Add(tdArr3.ElementAt(i).InnerText.Trim());

                                //TODO SET DEFAULT IMG URL
                            }
                            else
                            {
                                imgUrl1 = "" + img1.Attributes["src"].Value?.Split('/').LastOrDefault();
                                FList3.Add(imgUrl1);
                            }



                            var img2 = tdArr4.ElementAt(i).SelectSingleNode("img");
                            var imgUrl2 = "";
                            if (img2 == null)
                            {
                                if (i == 0)
                                    FList4.Add(tdArr4.ElementAt(i).InnerText.Trim());

                                //TODO SET DEFAULT IMG URL
                            }
                            else
                            {
                                imgUrl2 = "http://47.106.183.2:8887/Images/" + img1.Attributes["src"].Value?.Split('/').LastOrDefault();
                                FList4.Add(imgUrl2);
                            }
                        }

                        #endregion
                    }


                    #endregion

                    #region 八字论命

                    if (title.IndexOf("八字论命", StringComparison.Ordinal) != -1)
                    {
                        SList.AddRange(box.ChildNodes.Select(p => p.InnerText.Trim()).Where(text => text.Replace("\r", "").Replace("\n", "").Trim() != ""));
                    }

                    #endregion

                    #region 五行分析

                    if (title.IndexOf("八字论命", StringComparison.Ordinal) != -1)
                    {
                        var tableArr = box.ChildNodes.Where(node => node.Name == "table");
                        if (!tableArr.Any())
                            continue; //TODO LOG

                        //IN NORMAL CASE:tableArr.Count = 2

                        if (tableArr.Count() < 2)
                        {
                            //TODO LOG INNORMAL 
                            continue;
                        }

                        if (tableArr.Count() > 2)
                        {
                            //TODO LOG INNORMAL
                        }

                        THtml = tableArr.Aggregate(THtml, (current, table) => current + table.OuterHtml.Trim());
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
                                TList.Add(childText);
                                childText = "";
                            }
                            else
                                childText += node.InnerText.Replace("&nbsp;", "").Trim();
                        }

                        if (childText != "")
                            TList.Add(childText);
                    }

                    #endregion

                    #region 八字命卦分析

                    if (title.IndexOf("八字命卦分析", StringComparison.Ordinal) != -1)
                    {
                        var type = box.SelectSingleNode("b//font")?.InnerText.Trim();
                        var imgUrl = box.SelectSingleNode("img[@class='imgs']")?.Attributes["src"].Value.Trim();

                        FuType = type;
                        FuImgUrl = imgUrl;

                        var trArr = box.SelectNodes("table//tr");

                        if (trArr == null)
                            continue;
                        //&darr;
                        FuDic.AddRange(trArr.Select(tr => new List<string>
                        {
                            tr.SelectNodes("td").ElementAt(0)?.InnerText.Replace("&darr;","").Trim(),
                            tr.SelectNodes("td").ElementAt(1)?.InnerText.Replace("&darr;","").Trim()
                        }));

                        var flag = true;
                        var childNodes = box.ChildNodes;

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
                                    FuDic2.Add(new KeyValuePair<string, string>("img", img));
                            }
                            else
                            {
                                if (child.InnerText.Trim() != "")
                                    FuDic2.Add(new KeyValuePair<string, string>("text", child.InnerText.Trim()));
                            }

                        }
                    }

                    #endregion
                }
            }

            var dic1 = new Dictionary<string, object>
            {
                {"PersonalInfo", FDic},
                {"Life", FHtml},
                {"ByGone", new List<object> {FList1, FList2, FList3, FList4}}
            };

            var resultDic = new Dictionary<string, object> { { "Natal", dic1 }, { "Fate", SList } };



            return resultDic;
        }
    }
}
