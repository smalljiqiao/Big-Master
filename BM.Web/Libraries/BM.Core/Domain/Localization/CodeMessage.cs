namespace BM.Core.Domain.Localization
{
    /// <summary>
    /// 返回码类
    /// </summary>
    public class CodeMessage : BaseEntity
    {
        /// <summary>
        /// 返回码类ID
        /// </summary>
        public int CmId { get; set; }

        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回码说明
        /// </summary>
        public string Message { get; set; }
    }
}
