namespace BM.Services.Common
{
    /// <summary>
    /// 错误反馈类
    /// </summary>
    public class ErrorTip
    {
        public ErrorTip(int errorCode, string message)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        //错误代码
        private int ErrorCode;

        //错误信息
        private string Message;
    }
}
