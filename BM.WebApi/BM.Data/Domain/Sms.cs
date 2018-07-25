namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sms
    {
        [Key]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual User User { get; set; }
    }
}
