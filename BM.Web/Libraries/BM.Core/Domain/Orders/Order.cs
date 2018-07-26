using BM.Core.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单类
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        [Column("Phone", Order = 1)]
        public string Phone { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
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
        public DateTime? CreateTime { get; set; }

        //外键关联User表
        public virtual User User { get; set; }

        /// <summary>
        /// 一对一关系外键关联OrderSearch表
        /// </summary>
        public virtual OrderSearch OrderSearch { get; set; }
    }
}
