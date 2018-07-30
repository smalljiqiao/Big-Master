using System;

namespace BM.Core.Domain.Data.Dream
{
    /// <summary>
    /// 周公解梦详情类
    /// </summary>
    public class DreamDetail
    {
        /// <summary>
        /// 周公解梦详情类ID
        /// </summary>
        public int DreamId { get; set; }

        /// <summary>
        /// Html
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 外键关联DreamTitle表
        /// </summary>
        public DreamTitle DreamTitle { get; set; }
    }
}
