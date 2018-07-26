using System;
using BM.Data.Domain;
using BM.Services.Logs;

namespace BM.Services.BurialPoint
{
    /// <summary>
    /// 埋点服务类
    /// </summary>
    public static class BpService
    {
        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="remark"></param>
        public static void Use(string remark)
        {
            try
            {
                var bPModel = new BM.Data.Domain.BurialPoint { Remark = remark };

                var db = new DbEntities();
                db.BurialPoint.Add(bPModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
            }
        }
    }
}
