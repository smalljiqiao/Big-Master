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
        public static bool Insert(string androidId)
        {
            try
            {
                var db = new DbEntities();

                var androidInfo = new Android { AndroidId = androidId };

                db.Android.Add(androidInfo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 根据安卓ID获取安卓信息
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public static Android GetByAndroidId(string androidId, ReturnCode returnCode)
        {
            Android android;

            try
            {
                var db = new DbEntities();

                var query = from d in db.Android
                            where d.AndroidId == androidId
                            select d;

                android = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode.Code = -1;
                LogService.InsertLog(ex);
                return null;
            }

            return android;
        }
    }
}
