using System;

namespace BM.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public User()
        {
            this.CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        public string Phone { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string SaltPassword { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
