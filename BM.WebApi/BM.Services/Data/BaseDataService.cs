using BM.Data.Domain;
using BM.Services.Infrastructure;

namespace BM.Services.Data
{
    public class BaseDataService
    {
        protected DbEntities db;

        /// <summary>
        /// 构造函数：为数据库上下文字段db赋值
        /// </summary>
        protected BaseDataService()
        {
            if (Singleton<DbEntities>.Instance == null)
                Singleton<DbEntities>.Instance = new DbEntities();

            db = Singleton<DbEntities>.Instance;
        }
    }
}
