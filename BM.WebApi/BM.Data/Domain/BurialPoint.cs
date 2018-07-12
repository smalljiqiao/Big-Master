namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BurialPoint")]
    public partial class BurialPoint
    {
        [Key]
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
