using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单类
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// 安卓ID
        /// </summary>
        public string AndroidId { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 订单价格,精确两位小数
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 订单支付状态
        /// </summary>
        public string PayState { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
