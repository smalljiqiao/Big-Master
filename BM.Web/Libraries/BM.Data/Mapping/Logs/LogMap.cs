using BM.Core.Domain.Logs;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.Logs
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(k => k.LogId);
            this.Property(p => p.LogId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.Msg).HasColumnType("nvarchar").HasMaxLength(2000);
            this.Property(p => p.StackTrace).IsMaxLength();  //nvarchar(MAX)
            this.Property(p => p.CreateTime).HasColumnType("datetime"); ;
        }
    }
}
