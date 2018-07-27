namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DreamDetail")]
    public partial class DreamDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DreamId { get; set; }

        [Required]
        public string Html { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }
    }
}
