namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            //ע�⽫�ַ����ĺ���ת��Ϊʱ���ʽ��.f,:f�ǻᱨ��ġ�
            //ע��������ʽ������yyyy/MM/dd ת��Ϊʱ���ʽ֮�󻹻���yyyy-MM-dd��
            this.CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(8)]
        public string Salt { get; set; }

        [Required]
        public string SaltPassword { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
