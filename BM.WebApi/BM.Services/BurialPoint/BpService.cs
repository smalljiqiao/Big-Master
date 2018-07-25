using BM.Data.Domain;

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
            var bPModel = new BM.Data.Domain.BurialPoint {Remark = remark};

            var db = new DbEntities();
            db.BurialPoint.Add(bPModel);
            db.SaveChanges();
        }
    }
}
