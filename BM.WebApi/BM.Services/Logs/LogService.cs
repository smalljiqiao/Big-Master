using System;
using BM.Data.Domain;

namespace BM.Services.Logs
{
    /// <summary>
    /// 日记记录类
    /// </summary>
    public static class LogService
    {
        /// <summary>
        /// 记录错误信息 存储在Log Table
        /// </summary>
        public static void InsertLog(Exception ex)
        {
            var logInfo = new Log { Msg = ex.Message, StackTrace = ex.StackTrace };

            var db = new DbEntities();
            db.Log.Add(logInfo);
            db.SaveChanges();
        }
    }
}
