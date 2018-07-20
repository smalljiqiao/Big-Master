namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BPrice")]
    public partial class BPrice
    {
        [Key]
        public int PriceId { get; set; }

        public int TypeId { get; set; }

        public decimal OriginalCost { get; set; }

        public decimal PreferentialCost { get; set; }

        public virtual BType BType { get; set; }
    }
}
