using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using BM.Data.Domain;
using BM.Services.Logs;

namespace BM.Services.Orders
{
    /// <summary>
    /// 订单服务类
    /// </summary>
    public static class OrderService
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public static bool Insert(Order orderModel)
        {
            try
            {
                var db = new DbEntities();
                db.Order.Add(orderModel);
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
