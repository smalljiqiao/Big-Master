using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class User
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("用户手机号码")]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(8)]
        public string Salt { get; set; }

        public string SaltPassword { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
