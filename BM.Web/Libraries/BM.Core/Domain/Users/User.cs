using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BM.Core.Domain.ShortMessage;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        [Column("Phone", Order = 1)]
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码明文
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码盐值
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 密码密文
        /// </summary>
        public string SaltPassword { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 外键关联Sms表，一对一关系
        /// </summary>
        public Sms Sms { get; set; }
    }
}
