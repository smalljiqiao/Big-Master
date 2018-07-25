using BM.Services.Common;

namespace BM.Services.ReturnServices
{
    public sealed class Return
    {
        public ReturnCode ReturnCode { get; set; }

        public object Content { get; set; }
    }
}
