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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderSearch = new HashSet<OrderSearch>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Phone { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid OrderId { get; set; }

        [StringLength(20)]
        public string OrderType { get; set; }

        public decimal Price { get; set; }

        [StringLength(10)]
        public string PayState { get; set; }

        public DateTime? CreateTime { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderSearch> OrderSearch { get; set; }
    }
}
