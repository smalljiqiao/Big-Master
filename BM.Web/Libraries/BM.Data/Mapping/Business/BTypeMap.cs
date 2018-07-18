using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Business;

namespace BM.Data.Mapping.Business
{
    public partial class BTypeMap : EntityTypeConfiguration<BType>
    {
        public BTypeMap()
        {
            this.ToTable("BType");
            this.HasKey(k => k.TypeId);
            this.Property(p => p.TypeId).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired(); //显式设置自增长
            this.Property(p => p.TypeName).HasColumnType("nvarchar").HasMaxLength(10);
        }
    }
}
