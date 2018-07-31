using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Logs;

namespace BM.Data.Mapping.Logs
{
    public partial class BurialPointMap : EntityTypeConfiguration<BurialPoint>
    {
        public BurialPointMap()
        {
            this.ToTable("BurialPoint");
            this.HasKey(k => new { k.BpId }); 
            this.Property(p => p.BpId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.FType).HasColumnType("nvarchar").HasMaxLength(10);
            this.Property(p => p.SType).HasColumnType("nvarchar").HasMaxLength(10);
            this.Property(p => p.Remark).HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(p => p.CreateTime).HasColumnType("datetime"); ;
        }
    }
}
