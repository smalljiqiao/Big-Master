namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public Guid LogId { get; set; }

        [StringLength(2000)]
        public string Mes { get; set; }

        [StringLength(4000)]
        public string StackTrace { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}