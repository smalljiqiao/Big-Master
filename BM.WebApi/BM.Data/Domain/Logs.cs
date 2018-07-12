namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logs
    {
        [Key]
        public Guid UUID { get; set; }

        [StringLength(1000)]
        public string msg { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
