using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 安卓信息类
    /// </summary>
    public class AndroidInfo : BaseEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        [Column("Phone", Order = 1)]
        public string Phone { get; set; }

        /// <summary>
        /// 安卓ID
        /// </summary>
        public string AndroidId { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        //外键关联User表
        public virtual User User { get; set; }
    }
}
