﻿using System;
using System.Data.Entity.Migrations;
using BM.Data.Domain;
using BM.Services.Logs;

namespace BM.Services.ShortMessages
{
    /// <summary>
    /// 短信服务类，主要用于数据库记录
    /// </summary>
    public static class SmsService
    {
        /// <summary>
        /// 更新Sms Table
        /// </summary>
        /// <param name="sms"></param>
        public static void InsertOrUpdate(Data.Domain.Sms sms)
        {
            try
            {
                var db = new DbEntities();
                db.Sms.AddOrUpdate(sms);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
            }
        }
    }
}
