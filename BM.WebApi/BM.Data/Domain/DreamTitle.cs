namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DreamTitle")]
    public partial class DreamTitle
    {
        [Key]
        public int DreamId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string SubTitle { get; set; }

        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual DreamDetail DreamDetail { get; set; }
    }
}
