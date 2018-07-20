using System;

namespace BM.Core.Domain.Logs
{
    /// <summary>
    /// 错误日志类
    /// </summary>
    public class Log : BaseEntity
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public Guid LogId { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Mes { get; set; }

        /// <summary>
        /// 堆栈轨迹
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
