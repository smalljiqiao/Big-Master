using System;

namespace BM.Services.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 随机创建六位随机数
        /// </summary>
        /// <returns></returns>
        public static string CreateCode()
        {
            var rd = new Random();
            var num = rd.Next(99999, 1000000);
            return num.ToString();
        }
    }
}
