using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Users;

namespace BM.Data.Mapping.Users
{
    public partial class AndroidInfoMap : EntityTypeConfiguration<AndroidInfo>
    {
        public AndroidInfoMap()
        {
            this.ToTable("AndroidInfo");
            this.HasKey(c => new { c.Phone, c.AndroidId });
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.AndroidId).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.CreateTime).HasColumnType("datetime");;

            this.HasRequired(android => android.User)
                .WithMany()
                .HasForeignKey(android => android.Phone);
        }
    }
}
