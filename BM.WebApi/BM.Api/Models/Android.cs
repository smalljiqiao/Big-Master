using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// 安卓信息类
    /// </summary>
    public class Android : BaseModel
    {
        /// <summary>
        /// 安卓ID
        /// </summary>
        [Required]
        public string AndroidId { get; set; }

        /// <summary>
        ///用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}