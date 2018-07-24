using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Data.Dream;

namespace BM.Data.Mapping.Data.DreamData
{
    public partial class DreamTitleMap : EntityTypeConfiguration<DreamTitle>
    {
        public DreamTitleMap()
        {
            this.ToTable("DreamTitle");
            this.HasKey(k => new { k.DreamId });
            this.Property(p => p.DreamId).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Title).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            this.Property(p=>p.SubTitle).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            this.Property(p => p.CreateTime).HasColumnType("datetime");
        }
    }
}
