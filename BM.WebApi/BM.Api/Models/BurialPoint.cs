using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class BurialPoint
    {
        public Guid UUID { get; set; }

        [StringLength(50)]
        public string AndroidID { get; set; }

        public Guid? ID { get; set; }

        [StringLength(20)]
        public string FType { get; set; }

        [StringLength(20)]
        public string SType { get; set; }

        [StringLength(100)]
        public string Bak { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
