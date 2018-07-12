namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
