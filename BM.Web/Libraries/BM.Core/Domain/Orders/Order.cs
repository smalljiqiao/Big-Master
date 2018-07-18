using BM.Core.Description;
using BM.Core.Domain.Users;
using System;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单类
    /// </summary>
    [Description("订单类")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Description("订单ID")]
        public Guid OrderId { get; set; }

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
        public DateTime CreateTime { get; set; }

        //外键关联User表
        public virtual User User { get; set; }

        //一对一关系外键关联OrderSearch表
        public virtual OrderSearch OrderSearch { get; set; }
    }
}
