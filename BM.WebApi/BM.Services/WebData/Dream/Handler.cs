using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BM.Data.Domain;
using BM.Services.Common;
using HtmlAgilityPack;

namespace BM.Services.WebData.Dream
{
    /// <summary>
    /// 周公解梦功能
    /// </summary>
    public static class Handler
    {
        #region 爬取数据
        /// <summary>
        /// 获取标题和子标题
        /// </summary>
        private static string GetTitleHtml()
        {
            var url = "http://www.99166.com/dream/";

            var html = string.Empty;

            try
            {
                var http = new HttpHelper();
                var item = new HttpItem()
                {
                    URL = url, //URL     必需项  
                    Method = "Get", //URL     可选项 默认为Get  
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
                //TODO Log
            }

            return html;
        }

        /// <summary>
        /// 将标题信息插入数据库
        /// </summary>
        public static void InsertTitleToDb()
        {
            var html = GetTitleHtml();

            if (html == "")
                return;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            try
            {
                var mjDiv = doc.DocumentNode.SelectSingleNode("//div[@class='mj']");

                var dlList = mjDiv.SelectNodes("dl");

                foreach (var dl in dlList)
                {
                    var title = dl.SelectSingleNode("dt/a").InnerText.Trim();
                    var subTitle = string.Empty;

                    var liList = dl.SelectNodes("dd/ul/li");

                    foreach (var li in liList)
                    {
                        var url = li.SelectSingleNode("a").Attributes["href"].Value;
                        subTitle = li.InnerText.Trim();

                        if (url != "")
                        {
                            url = "http://www.99166.com/dream/" + url;

                            var info = new DreamTitle
                            {
                                Title = title,
                                SubTitle = subTitle,
                                Url = url
                            };

                            var db = new DbEntities();
                            db.DreamTitle.Add(info);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO LOG
            }
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        public static string GetDetailHtml(string url)
        {
            var html = string.Empty;

            try
            {
                var http = new HttpHelper();
                var item = new HttpItem()
                {
                    URL = url, //URL     必需项  
                    Method = "Get", //URL     可选项 默认为Get  
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
                //TODO Log
            }

            return html;
        }

        /// <summary>
        /// 将详细信息插入数据库
        /// </summary>
        public static void InsertDetailToDb()
        {
            var db = new DbEntities();
            var info = from d in db.DreamTitle
                       select d;

            foreach (var dream in info)
            {
                var html = GetDetailHtml(dream.Url);

                if (html == "")
                    return;

                var resultHmle = string.Empty;

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var table = doc.DocumentNode.SelectSingleNode("//div[@class='listb']//div[@class='ltbox']//table");

                var trArr = table.SelectNodes("tr");

                foreach (var tr in trArr)
                {
                    var tdArr = tr.SelectNodes("td");

                    //标题
                    if (tdArr.Count == 1)
                    {
                        if (tdArr[0].InnerText.Trim() != "")
                        {
                            resultHmle += "<h3>" + tdArr[0].SelectSingleNode("span").InnerHtml.Trim() + "</h3>";
                        }
                    }
                    //内容
                    else
                    {
                        var pArr = tdArr[1].SelectNodes("p");

                        if (pArr == null)
                        {
                            resultHmle += "<p>" + tdArr[1].InnerText.Trim() + "</p>";
                        }
                        else
                        {
                            foreach (var p in pArr)
                            {
                                var img = p.SelectSingleNode("img");
                                var strong = p.SelectSingleNode("strong");


                                if (img != null)
                                {
                                    var src = img.Attributes["src"].Value.Trim();
                                    resultHmle += "<img src='" + src + "'/>";

                                    continue;
                                }
                                else if (strong != null)
                                {
                                    var regex = new Regex(@"<strong>.*?</strong>", RegexOptions.IgnorePatternWhitespace);
                                    var strongText = strong.InnerText.Trim();

                                    var text = regex.Replace(p.InnerHtml, "<storng>" + strongText + "</strong>");

                                    resultHmle += "<p>" + text + "</p>";
                                }
                                else
                                {
                                    var text = p.InnerText.Trim();
                                    resultHmle += "<p>" + text + "</p>";
                                }
                            }
                        }
                    }
                }

                var detailInfo = new DreamDetail
                {
                    DreamId = dream.DreamId,
                    Html = resultHmle
                };

                var db2 = new DbEntities();

                db2.DreamDetail.Add(detailInfo);
                db2.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// 获取联动标题
        /// </summary>
        /// <returns>JSON格式</returns>
        public static Dictionary<string, Dictionary<int, string>> Title()
        {
            var db = new DbEntities();

            var titleEn = from d in db.DreamTitle
                          orderby d.Title
                          select d;

            var dic = new Dictionary<string, Dictionary<int, string>>();
            var dicList = new Dictionary<int, string>();
            var title = string.Empty;

            foreach (var t in titleEn)
            {
                if (t.Title != title && title != "")
                {
                    dic.Add(title, dicList);
                    dicList.Clear();
                }

                title = t.Title;

                dicList.Add(t.DreamId, t.SubTitle);
            }

            return dic;
        }

        /// <summary>
        /// 获取详细内容
        /// </summary>
        /// <param name="dreamId">ID</param>
        /// <returns>HTML</returns>
        public static string Detail(int dreamId)
        {
            var db = new DbEntities();

            var detail = from d in db.DreamDetail
                         where d.DreamId == dreamId
                         select d;

            if (detail.Any())
            {
                return detail.First().Html;
            }

            return "";
        }
    }
}
