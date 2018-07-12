namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Orders
    {
        [Key]
        [Column(Order = 0)]
        public Guid ID { get; set; }

        [Key]
        [Column(Order = 1)]
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

        public virtual OrderSearch OrderSearch { get; set; }

        public virtual Users Users { get; set; }
    }
}
