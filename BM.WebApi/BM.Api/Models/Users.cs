using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class Users
    {
        public Guid ID { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(6)]
        public string Salt { get; set; }

        [StringLength(40)]
        public string SaltPassword { get; set; }

        public DateTime? RegisterTime { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
