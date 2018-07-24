namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AndroidInfo")]
    public partial class AndroidInfo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Phone { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string AndroidId { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual User User { get; set; }
    }
}
