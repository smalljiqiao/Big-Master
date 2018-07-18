using System;

namespace BM.Core.Domain.Users
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class User : BaseEntity
    {
        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string SaltPassword { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
