using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Localization;

namespace BM.Data.Mapping.Localization
{
    public partial class CodeMessageMap : EntityTypeConfiguration<CodeMessage>
    {
        public CodeMessageMap()
        {
            this.ToTable("CodeMessage");
            this.HasKey(k => k.CmId);
            this.Property(p => p.CmId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            this.Property(p => p.Code).HasColumnType("char").HasMaxLength(4).IsFixedLength().IsRequired();
            this.Property(p => p.Message).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
        }
    }
}
