using BM.Core.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Core.Domain.Logs
{
    public class BurialPoint : BaseEntity
    {
        /// <summary>
        /// 埋点ID
        /// </summary>
        public Guid BpId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// 一级类型
        /// </summary>
        public string FType { get; set; }

        /// <summary>
        /// 二级类型
        /// </summary>
        public string SType { get; set; }

        /// <summary>
        /// 补充说明，当二类类型不能满足埋点操作时，将额外信息写入此列
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
