using BM.Services.Common;

namespace BM.Services.ReturnServices
{
    /// <summary>
    /// 数据返回类
    /// </summary>
    public sealed class Return
    {
        /// <summary>
        /// 返回码对象
        /// </summary>
        public ReturnCode ReturnCode { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public object Content { get; set; }
    }
}
