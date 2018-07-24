using System.Data.Entity.ModelConfiguration;
using BM.Core.Domain.Orders;

namespace BM.Data.Mapping.Orders
{
    public partial class OrderSearchMap : EntityTypeConfiguration<OrderSearch>
    {
        public OrderSearchMap()
        {
            this.ToTable("OrderSearch");
            this.HasKey(k => k.OrderId);
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.OrderId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.UserName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.Sex).HasColumnType("tinyint");
            this.Property(p => p.BirthDay).HasColumnType("datetime");
            this.Property(p => p.ManName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.ManBirthDay).HasColumnType("datetime");
            this.Property(p => p.WomanName).HasColumnType("nvarchar").HasMaxLength(40);
            this.Property(p => p.WomanBirthDay).HasColumnType("datetime");

            this.HasRequired(orderSearch => orderSearch.Order).WithOptional(orderSearch => orderSearch.OrderSearch);
        }
    }
}
