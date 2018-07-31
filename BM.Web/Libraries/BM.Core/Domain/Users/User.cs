using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// 用户默认名称，六位纯数字
        /// </summary>
        public string DefaultName { get; set; }

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
        /// 注册时间 代码生成
        /// </summary>
        public DateTime? RegisterTime { get; set; }
    }
}
