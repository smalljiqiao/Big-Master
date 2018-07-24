using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单查询类
    /// 记录订单所有查询的关键字信息
    /// </summary>
    public class OrderSearch : BaseEntity
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
        /// 查询姓名 对应八字详批和宝宝取名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名
        /// </summary>
        public byte Sex { get; set; }

        /// <summary>
        /// 出生日期 对应八字详批和宝宝取名
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 八字合婚男方姓名
        /// </summary>
        public string ManName { get; set; }

        /// <summary>
        /// 八字合婚男方出生日期
        /// </summary>
        public DateTime ManBirthDay { get; set; }

        /// <summary>
        /// 八字合婚女方姓名
        /// </summary>
        public string WomanName { get; set; }

        /// <summary>
        /// 八字合婚女方出生日期
        /// </summary>
        public DateTime WomanBirthDay { get; set; }

        /// <summary>
        /// 外键关联Order表
        /// </summary>
        public Order Order { get; set; }
    }
}
