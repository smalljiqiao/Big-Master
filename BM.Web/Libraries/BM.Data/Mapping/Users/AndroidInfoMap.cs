using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Users;

namespace BM.Data.Mapping.Users
{
    public partial class AndroidInfoMap : EntityTypeConfiguration<AndroidInfo>
    {
        public AndroidInfoMap()
        {
            this.ToTable("AndroidInfo");
            this.HasKey(c => new { c.AndroidId });
            this.Property(p => p.AndroidId).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.CreateTime).HasColumnType("datetime");
        }
    }
}
