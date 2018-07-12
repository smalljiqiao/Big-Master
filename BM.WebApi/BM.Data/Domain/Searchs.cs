namespace BM.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Searchs
    {
        public Guid ID { get; set; }

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

        [StringLength(50)]
        public string ZhouWord { get; set; }

        public virtual Users Users { get; set; }
    }
}
