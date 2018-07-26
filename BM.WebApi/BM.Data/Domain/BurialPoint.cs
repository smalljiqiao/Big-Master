namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BurialPoint")]
    public partial class BurialPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BpId { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(10)]
        public string FType { get; set; }

        [StringLength(10)]
        public string SType { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }
    }
}
