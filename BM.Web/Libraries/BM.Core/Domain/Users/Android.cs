using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 安卓信息类
    /// </summary>
    public class Android : BaseEntity
    {
        /// <summary>
        /// 安卓ID
        /// </summary>
        public string AndroidId { get; set; }

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
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
