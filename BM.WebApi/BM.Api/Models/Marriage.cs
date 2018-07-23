using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// 八字合婚模型类
    /// </summary>
    public class Marriage
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 男方姓名
        /// </summary>
        [RegularExpression(@"[\u4e00-\u9fa5]{2,8}", ErrorMessage = "姓名限制为中文，且长度为2-8位字符")]
        public string ManName { get; set; }

        /// <summary>
        /// 男方出生日期 yyyy-MM-dd
        /// </summary>
        public DateTime ManBirthDay { get; set; }

        /// <summary>
        /// 男方时辰
        /// </summary>
        public string ManTime { get; set; }

        /// <summary>
        /// 女方姓名
        /// </summary>
        [RegularExpression(@"[\u4e00-\u9fa5]{2,8}", ErrorMessage = "姓名限制为中文，且长度为2-8位字符")]
        public string WomanName { get; set; }

        /// <summary>
        /// 女方出生日期 yyyy-MM-dd
        /// </summary>
        public DateTime WomanBirthDay { get; set; }

        /// <summary>
        /// 女方时辰
        /// </summary>
        public string WomanTime { get; set; }
    }
}