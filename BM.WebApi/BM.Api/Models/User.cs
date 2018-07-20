using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class User
    {
        [Required]
        [StringLength(20)]
        [DisplayName("�û��ֻ�����")]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }
    }
}
