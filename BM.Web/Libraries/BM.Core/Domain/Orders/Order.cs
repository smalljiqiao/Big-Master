using System;
using System.ComponentModel.DataAnnotations;
using BM.Core.Domain.Users;

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
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

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

        /// <summary>
        /// 主键关联OrderSearch表 一对一关系
        /// </summary>
        public OrderSearch OrderSearch { get; set; }

        /// <summary>
        /// 外键关联User表
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 外键关联Android表
        /// </summary>
        public Android Android { get; set; }
    }
}
