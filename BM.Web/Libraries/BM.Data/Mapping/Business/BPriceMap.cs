using System.ComponentModel.DataAnnotations.Schema;
using BM.Core.Domain.Business;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.Business
{
    public partial class BPriceMap: EntityTypeConfiguration<BPrice>
    {
        public BPriceMap()
        {
            this.ToTable("BPrice");
            this.HasKey(k => k.PriceId);
            this.Property(p => p.PriceId).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired(); //显式设置自增长，EF默认的设置
            this.Property(p => p.OriginalCost).HasColumnType("decimal").HasPrecision(2, 2).IsRequired();
            this.Property(p => p.PreferentialCost).HasColumnType("decimal").HasPrecision(2, 2).IsRequired();

            this.HasRequired(bprice => bprice.BType)
                .WithMany()
                .HasForeignKey(bprice => bprice.TypeId);
        }
    }
}
