using BM.Core.Domain.Orders;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.Orders
{
    public partial class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.ToTable("Order");
            this.HasKey(k => new { k.Phone, k.OrderId });
            this.Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.OrderId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.OrderType).HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.Price).HasColumnType("decimal").HasPrecision(2, 2).IsRequired(); //两精度和保留两位小数
            this.Property(p => p.PayState).HasColumnType("varchar").HasMaxLength(10);
            this.Property(p => p.CreateTime).HasColumnType("datetime");;

            this.HasRequired(order => order.User)
                .WithMany()
                .HasForeignKey(order => order.Phone);
        }
    }
}
