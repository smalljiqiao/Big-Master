using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单查询类
    /// 记录订单所有查询的关键字信息
    /// </summary>
    public class OrderSearch : BaseEntity
    {
        public Guid OrderId { get; set; }

        public string UserName { get; set; }

        [Range(0, 2)]
        public byte Sex { get; set; }

        public DateTime BirthDay { get; set; }

        public string ManName { get; set; }

        public DateTime ManBirthDay { get; set; }

        public string WoManName { get; set; }

        public DateTime WoManBirthDay { get; set; }

        //外键关联Order表
        public Order Order { get; set; }
    }
}
