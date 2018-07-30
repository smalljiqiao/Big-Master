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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [StringLength(40)]
        public string DName { get; set; }

        public byte? DSex { get; set; }

        public DateTime DBirthDay { get; set; }

        [StringLength(10)]
        public string BSurname { get; set; }

        public byte? BSex { get; set; }

        public DateTime? BBirthDay { get; set; }

        [StringLength(20)]
        public string BProvince { get; set; }

        [StringLength(20)]
        public string BCity { get; set; }

        [StringLength(40)]
        public string MManName { get; set; }

        public DateTime? MManBirthDay { get; set; }

        [StringLength(20)]
        public string MManTime { get; set; }

        [StringLength(40)]
        public string MWomanName { get; set; }

        public DateTime? MWomanBirthDay { get; set; }

        [StringLength(20)]
        public string MWomanTime { get; set; }

        public virtual Order Order { get; set; }
    }
}
