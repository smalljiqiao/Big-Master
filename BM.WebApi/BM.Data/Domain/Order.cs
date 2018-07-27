namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string AndroidId { get; set; }

        [StringLength(20)]
        public string OrderType { get; set; }

        public decimal Price { get; set; }

        [StringLength(10)]
        public string PayState { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }
    }
}
