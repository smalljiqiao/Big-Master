using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Data.Dream;

namespace BM.Data.Mapping.Data.DreamData
{
    public partial class DreamDetailMap : EntityTypeConfiguration<DreamDetail>
    {
        public DreamDetailMap()
        {
            this.ToTable("DreamDetail");
            this.HasKey(k => k.DreamId);
            this.Property(p => p.DreamId).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); //外键DreamTitle
            this.Property(p => p.Html).IsMaxLength().IsRequired();  //nvarchar(MAX)
            this.Property(p => p.CreateTime).HasColumnType("datetime");

            //外键关联DreamTitle表 一对一关系
            this.HasRequired(dreamDetail => dreamDetail.DreamTitle).WithOptional(dreamTitle => dreamTitle.DreamDetail);
        }
    }
}
