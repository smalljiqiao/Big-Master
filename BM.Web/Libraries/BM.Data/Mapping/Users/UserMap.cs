using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Users;

namespace BM.Data.Mapping.Users
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(c => new {c.Id, c.Phone}); //复合主键
            this.Property(p => p.Nickname).HasMaxLength(20);
            this.Property(p => p.Email).HasMaxLength(50);
            this.Property(p => p.Password).HasMaxLength(20).IsRequired();
            this.Property(p => p.Salt).IsFixedLength().HasMaxLength(6).IsRequired();
            this.Property(p => p.SaltPassword).IsRequired(); //确认SHA1加密后的长度在定长
            this.Property(p => p.CreateTime).IsRequired();
        }
    }
}
