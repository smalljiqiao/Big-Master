using System;
using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Logs;

namespace BM.Data.Mapping.Logs
{
    public partial class LogMap:EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(k => k.LogId);
            this.Property(p => p.LogId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.Mes).HasColumnType("nvarchar").HasMaxLength(2000);
            this.Property(p => p.StackTrace).HasColumnType("nvarchar");  //不显示指定长度默认为MAX
            this.Property(p => p.CreateTime).HasColumnType("datetime").IsRequired();
        }
    }
}
