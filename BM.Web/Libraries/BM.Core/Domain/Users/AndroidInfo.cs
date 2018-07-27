using System;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 安卓信息类
    /// </summary>
    public class AndroidInfo : BaseEntity
    {
        /// <summary>
        /// 安卓ID
        /// </summary>
        public string AndroidId { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
