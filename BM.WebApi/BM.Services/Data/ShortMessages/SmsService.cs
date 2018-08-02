using BM.Services.Data.Logs;
using System;
using System.Data.Entity.Migrations;
using System.Linq;


namespace BM.Services.Data.ShortMessages
{
    /// <summary>
    /// 短信服务类，主要用于数据库记录
    /// </summary>
    public class SmsService : BaseDataService
    {
        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public BM.Data.Domain.Sms GetSmsByPhone(string phone)
        {
            var query = from d in db.Sms
                        where d.Phone == phone
                        select d;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// 更新Sms Table
        /// </summary>
        /// <param name="sms">Sms模型</param>
        /// <returns></returns>
        public bool InsertOrUpdate(BM.Data.Domain.Sms sms)
        {
            try
            {
                db.Sms.AddOrUpdate(sms);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                return false;
            }

            return true;
        }
    }
}
