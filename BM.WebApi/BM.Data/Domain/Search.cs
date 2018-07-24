namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Search")]
    public partial class Search
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Phone { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid SearchId { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        public byte Sex { get; set; }

        public DateTime BirthDay { get; set; }

        [StringLength(40)]
        public string ManName { get; set; }

        public DateTime ManBirthDay { get; set; }

        [StringLength(40)]
        public string WomanName { get; set; }

        public DateTime WomanBirthDay { get; set; }

        [StringLength(100)]
        public string ZhouWord { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual User User { get; set; }
    }
}
