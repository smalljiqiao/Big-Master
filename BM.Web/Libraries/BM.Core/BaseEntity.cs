using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.Core
{
    /// <summary>
    /// 表基类
    /// </summary>
    public abstract partial class BaseEntity
    {
        //UUID 主键
        //定义此列顺序，因为默认会将主键拜访在第一位，而目前此列并不是主键
        [Key]
        [Phone]
        [Column("Phone", Order = 1)]
        public string Phone { get; set; }
    }
}
