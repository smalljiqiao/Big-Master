using BM.Core.Domain.ShortMessage;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.ShortMessage
{
    public partial class SmsMap : EntityTypeConfiguration<Sms>
    {
        public SmsMap()
        {
            this.ToTable("Sms");
            this.HasKey(k => k.Phone);
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.Code).HasColumnType("char").HasMaxLength(6).IsFixedLength().IsRequired();
            this.Property(p => p.UpdateTime).HasColumnType("datetime");

            //一对一关系
            this.HasRequired(sms => sms.User).WithOptional(sms => sms.Sms);
        }
    }
}
