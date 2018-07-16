using BM.Services.Localization;

namespace BM.Services.Common
{
    /// <summary>
    /// 反馈类
    /// </summary>
    public class ReturnCode
    {
        private static readonly LocalizationService Local = new LocalizationService();

        private int _code;
        private string _message;

        //返回码
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                Message = Local.GetResource(Code.ToString());
            }
        }

        //返回码说明
        public string Message
        {
            get => _message;
            private set => _message = value;
        }

        //补充说明
        public string Remark { get; set; }
    }
}
