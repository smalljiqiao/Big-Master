namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderSearch")]
    public partial class OrderSearch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderSearch()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [StringLength(16)]
        public string OrderID { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        public byte? Sex { get; set; }

        public DateTime? BirthDay { get; set; }

        [StringLength(20)]
        public string ManName { get; set; }

        public DateTime? ManBirthDay { get; set; }

        [StringLength(20)]
        public string WomanName { get; set; }

        public DateTime? WomanBirthDay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
