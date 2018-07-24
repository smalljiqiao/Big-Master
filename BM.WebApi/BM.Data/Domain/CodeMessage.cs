namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CodeMessage")]
    public partial class CodeMessage
    {
        [Key]
        public int CmId { get; set; }

        [Required]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; }
    }
}
