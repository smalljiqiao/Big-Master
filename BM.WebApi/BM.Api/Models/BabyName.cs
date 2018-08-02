using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// 宝宝取名模型类
    /// </summary>
    public class BabyName : BaseModel
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 姓名 长度限制为1-2
        /// </summary>
        [RegularExpression(@"[\u4e00-\u9fa5]{1,2}", ErrorMessage = "姓氏限制为中文，且长度为1-2位字符")]
        [DisplayName("姓氏")]
        public string Surname { get; set; }

        /// <summary>
        /// 出生日期 格式 yyyy-MM-dd HH:mm
        /// </summary>
        [Required]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 是否为男性 true:yes false:women
        /// </summary>
        [Required]
        public bool IsMan { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [Required]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// 名字形式 0:单字 1:双子 2:叠字
        /// </summary>
        [Required]
        public int NameType { get; set; }
    }
}