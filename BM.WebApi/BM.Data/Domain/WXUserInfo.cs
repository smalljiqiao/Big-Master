namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WXUserInfo")]
    public partial class WXUserInfo
    {
        public Guid ID { get; set; }

        [StringLength(50)]
        public string Openid { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        public byte? Sex { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        [StringLength(500)]
        public string HeadImgUrl { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
