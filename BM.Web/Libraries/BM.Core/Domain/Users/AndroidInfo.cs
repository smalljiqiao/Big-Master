using System;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 安卓信息类
    /// </summary>
    public class AndroidInfo : BaseEntity
    {
        public string AndroidId { get; set; }

        public DateTime CreateTime { get; set; }

        //外键关联User表
        public virtual User User { get; set; }
    }
}
