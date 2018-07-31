using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Users;

namespace BM.Data.Mapping.Users
{
    public partial class AndroidMap : EntityTypeConfiguration<Android>
    {
        public AndroidMap()
        {
            this.ToTable("Android");
            this.HasKey(c => new { c.AndroidId });
            this.Property(p => p.AndroidId).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.UserId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.CreateTime).HasColumnType("datetime");
        }
    }
}
