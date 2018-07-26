using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// 八字详批模型类
    /// </summary>
    public class DetailedBatch
    {
        /// <summary>
        /// 手机号码
        /// </summary>
       
        public string Phone { get; set; }

        /// <summary>
        /// 姓名 长度限制为2-8
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 出生日期 格式 yyyy-MM-dd
        /// </summary>
        [Required]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 是否为男性 true:yes false:women
        /// </summary>
        [Required]
        public bool IsMan { get; set; }
    }
}