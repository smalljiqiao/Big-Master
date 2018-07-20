using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Logs;

namespace BM.Data.Mapping.Logs
{
    public partial class SearchMap : EntityTypeConfiguration<Search>
    {
        public SearchMap()
        {
            this.ToTable("Search");
            this.HasKey(p => new { p.Phone, p.SearchId });
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.SearchId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.UserName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.Sex).HasColumnType("tinyint");
            this.Property(p => p.BirthDay).HasColumnType("datetime");
            this.Property(p => p.ManName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.ManBirthDay).HasColumnType("datetime");
            this.Property(p => p.WomanName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.WomanBirthDay).HasColumnType("datetime");
            this.Property(p => p.ZhouWord).HasColumnType("nvarchar").HasMaxLength(100);
            this.Property(p => p.CreateTime).HasColumnType("datetime");;


            this.HasRequired(search => search.User)
                .WithMany()
                .HasForeignKey(search => search.Phone);
        }
    }
}
