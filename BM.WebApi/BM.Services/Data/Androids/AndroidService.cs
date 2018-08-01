using System;
using System.Linq;
using BM.Data.Domain;
using BM.Services.Common;
using BM.Services.Data.Logs;

namespace BM.Services.Data.Androids
{
    /// <summary>
    /// 安卓信息服务类
    /// </summary>
    public static class AndroidService
    {
        /// <summary>
        /// 初次更新Android Table
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <returns>true:success;false:failure</returns>
        public static void Insert(string androidId)
        {
            var db = new DbEntities();

            var androidInfo = new Android { AndroidId = androidId };

            db.Android.Add(androidInfo);
            db.SaveChanges();
        }


        /// <summary>
        /// 根据安卓ID获取安卓信息
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <returns></returns>
        public static Android GetByAndroidId(string androidId)
        {
            var db = new DbEntities();

            var query = from d in db.Android
                        where d.AndroidId == androidId
                        select d;

            var android = query.FirstOrDefault();

            return android;
        }
    }
}
