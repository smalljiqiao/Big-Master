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
            this.Property(p => p.DName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.DSex).HasColumnType("tinyint");
            this.Property(p => p.DBirthDay).HasColumnType("datetime");
            this.Property(p => p.BSurname).HasColumnType("nvarchar").HasMaxLength(10);
            this.Property(p => p.BSex).HasColumnType("tinyint");
            this.Property(p => p.BBirthDay).HasColumnType("datetime");
            this.Property(p => p.BProvince).HasColumnType("nvarchar").HasMaxLength(20);
            this.Property(p => p.BCity).HasColumnType("nvarchar").HasMaxLength(20);
            this.Property(p => p.MManName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.MManBirthDay).HasColumnType("datetime");
            this.Property(p => p.MManTime).HasColumnType("nvarchar").HasMaxLength(20);
            this.Property(p => p.MWomanName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.MWomanBirthDay).HasColumnType("datetime");
            this.Property(p => p.MWomanTime).HasColumnType("nvarchar").HasMaxLength(20);
            this.Property(p => p.ZhouWord).HasColumnType("nvarchar").HasMaxLength(100);
            this.Property(p => p.CreateTime).HasColumnType("datetime"); 


            this.HasRequired(search => search.User)
                .WithMany()
                .HasForeignKey(search => search.Phone);
        }
    }
}
