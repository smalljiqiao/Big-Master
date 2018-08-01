using BM.Services.Common;

namespace BM.Services.Data.Users
{
    /// <summary>
    /// 修改密码结果类
    /// </summary>
    public sealed class ChangePasswordResult : BaseResult
    {
        /// <summary>
        /// Errors
        /// </summary>
        public ReturnCode ReturnCode { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ChangePasswordResult()
        {
            ReturnCode = new ReturnCode();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success
        {
            get => ReturnCode.Code == default(int);
        }

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="code">Return code</param>
        public void SetCode(int code)
        {
            ReturnCode.Code = code;
        }
    }
}
