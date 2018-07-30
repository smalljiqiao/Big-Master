using System;
using BM.Data.Domain;
using BM.Services.Data.Logs;

namespace BM.Services.Data.Orders
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
