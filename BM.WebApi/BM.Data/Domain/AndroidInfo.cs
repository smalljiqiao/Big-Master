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
        [StringLength(50)]
        public string AndroidId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }
    }
}
