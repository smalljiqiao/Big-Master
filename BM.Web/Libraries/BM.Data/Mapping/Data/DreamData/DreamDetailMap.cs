﻿using System.ComponentModel.DataAnnotations.Schema;
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
            this.Property(p => p.DreamId).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Html).HasColumnType("nvarchar").IsRequired();  //nvarchar(MAX)
            this.Property(p => p.CreateTime).HasColumnType("datetime");

            this.HasRequired(d => d.DreamTitle).WithOptional(d => d.DreamDetail);
        }
    }
}
