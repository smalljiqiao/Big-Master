using BM.Data.Domain;
using System.Linq;

namespace BM.Services.Data.Androids
{
    /// <summary>
    /// 安卓信息服务类
    /// </summary>
    public class AndroidService : BaseDataService
    {
        /// <summary>
        /// 初次更新Android Table
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <returns>true:success;false:failure</returns>
        public void Insert(string androidId)
        {
            var androidInfo = new Android { AndroidId = androidId };

            db.Android.Add(androidInfo);
            db.SaveChanges();
        }


        /// <summary>
        /// 根据安卓ID获取安卓信息
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <returns></returns>
        public Android GetByAndroidId(string androidId)
        {
            var query = from d in db.Android
                        where d.AndroidId == androidId
                        select d;

            var android = query.FirstOrDefault();

            return android;
        }
    }
}
