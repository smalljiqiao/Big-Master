using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class Logs
    {
        public Guid UUID { get; set; }

        [StringLength(1000)]
        public string msg { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
