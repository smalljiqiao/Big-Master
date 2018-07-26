namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            AndroidInfo = new HashSet<AndroidInfo>();
            Order = new HashSet<Order>();
            Search = new HashSet<Search>();
        }

        [Key]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string NickName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(8)]
        public string Salt { get; set; }

        [Required]
        [StringLength(50)]
        public string SaltPassword { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AndroidInfo> AndroidInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Search> Search { get; set; }
    }
}
