using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// 用户信息模型类
    /// </summary>
    public partial class User : BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [Phone(ErrorMessage = "不合法的手机号码")]
        public string Phone { get; set; }

        /// <summary>
        /// 安卓ID
        /// </summary>
        [StringLength(50)]
        public string Android { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(20, ErrorMessage = "昵称长度限制为20个字符")]
        public string Nickname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress(ErrorMessage = "不合法的邮箱地址")]
        public string Email { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string VCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [RegularExpression(@"[a-zA-Z0-9]{6,16}", ErrorMessage = "密码限制为英文和数字组合，且长度为6-16位字符")]
        public string Password { get; set; }
    }
}
