using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Api.Models
{
    public partial class Orders
    {
        public Guid ID { get; set; }

        [StringLength(16)]
        public string OrderID { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderType { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(10)]
        public string PayState { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
