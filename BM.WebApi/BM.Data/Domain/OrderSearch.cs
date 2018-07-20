namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderSearch")]
    public partial class OrderSearch
    {
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Key]
        public Guid OrderId { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        public byte Sex { get; set; }

        public DateTime BirthDay { get; set; }

        [StringLength(40)]
        public string ManName { get; set; }

        public DateTime ManBirthDay { get; set; }

        [StringLength(40)]
        public string WomanName { get; set; }

        public DateTime WomanBirthDay { get; set; }

        [Required]
        [StringLength(20)]
        public string Order_Phone { get; set; }

        public Guid Order_OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
