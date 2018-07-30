using BM.Core.Domain.Users;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.Users
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(c => new { c.UserId });
            this.Property(p => p.UserId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.DefaultName).HasColumnType("char").HasMaxLength(6).IsFixedLength();
            this.Property(p => p.NickName).HasMaxLength(20);
            this.Property(p => p.Email).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.Password).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.Salt).HasColumnType("char").IsFixedLength().HasMaxLength(8).IsRequired();
            this.Property(p => p.SaltPassword).HasColumnType("char").HasMaxLength(50).IsRequired(); //确认SHA1加密后的长度在定长
            this.Property(p => p.RegisterTime).HasColumnType("datetime");
        }
    }
}
