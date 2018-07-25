using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BM.Core.Domain.Users;

namespace BM.Core.Domain.ShortMessage
{
    /// <summary>
    /// 短信类
    /// </summary>
    public class Sms : BaseEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        [Column("Phone", Order = 1)]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 更新时间 由代码生成
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 外键关联User表
        /// </summary>
        public User User { get; set; }
    }
}
