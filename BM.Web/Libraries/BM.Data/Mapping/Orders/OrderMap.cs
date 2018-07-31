using BM.Core.Domain.Orders;
using System.Data.Entity.ModelConfiguration;

namespace BM.Data.Mapping.Orders
{
    public partial class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.ToTable("Order");
            this.HasKey(k => new {k.OrderId });
            this.Property(p => p.OrderId).HasColumnType("uniqueidentifier").IsRequired();
            this.Property(p => p.UserId).HasColumnType("uniqueidentifier");
            this.Property(p => p.AndroidId).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.OrderType).HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.Price).HasColumnType("decimal").HasPrecision(2, 2).IsRequired(); //两精度和保留两位小数
            this.Property(p => p.PayState).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            this.Property(p => p.CreateTime).HasColumnType("datetime");

            //外键关联User表UserId字段和Android表AndroidId字段
            this.HasRequired(order => order.User).WithMany().HasForeignKey(order => order.UserId);
            this.HasRequired(order => order.Android).WithMany().HasForeignKey(order => order.AndroidId);
        }
    }
}
