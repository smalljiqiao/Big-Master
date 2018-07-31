using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    /// <summary>
    /// �û���Ϣģ����
    /// </summary>
    public partial class User : BaseModel
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// �ֻ�����
        /// </summary>
        [Required]
        [Phone(ErrorMessage = "���Ϸ����ֻ�����")]
        public string Phone { get; set; }

        /// <summary>
        /// ��׿ID
        /// </summary>
        [StringLength(50)]
        public string Android { get; set; }

        /// <summary>
        /// �ǳ�
        /// </summary>
        [StringLength(20, ErrorMessage = "�ǳƳ�������Ϊ20���ַ�")]
        public string Nickname { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [EmailAddress(ErrorMessage = "���Ϸ��������ַ")]
        public string Email { get; set; }

        /// <summary>
        /// ������֤��
        /// </summary>
        public string VCode { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [RegularExpression(@"[a-zA-Z0-9]{6,16}", ErrorMessage = "��������ΪӢ�ĺ�������ϣ��ҳ���Ϊ6-16λ�ַ�")]
        public string Password { get; set; }
    }
}
