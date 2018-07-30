using System;

namespace BM.Core.Domain.Data.Dream
{
    /// <summary>
    /// 周公解梦标题类
    /// </summary>
    public class DreamTitle : BaseEntity
    {
        /// <summary>
        /// 周公解梦类ID
        /// </summary>
        public int DreamId { get; set; }

        /// <summary>
        /// 一级标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 主键关联DreamDetail表
        /// </summary>
        public DreamDetail DreamDetail { get; set; }
    }
}
