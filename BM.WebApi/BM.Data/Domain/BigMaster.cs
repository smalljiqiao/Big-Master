namespace BM.Data.Domain
{
    using System.Data.Entity;

    public partial class BigMaster : DbContext
    {
        public BigMaster()
            : base("name=BigMaster")
        {
        }

        public virtual DbSet<AndroidUserInfo> AndroidUserInfo { get; set; }
        public virtual DbSet<BurialPoint> BurialPoint { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderSearch> OrderSearch { get; set; }
        public virtual DbSet<Searchs> Searchs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WXUserInfo> WXUserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AndroidUserInfo>()
                .Property(e => e.AndroidID)
                .IsUnicode(false);

            modelBuilder.Entity<BurialPoint>()
                .Property(e => e.AndroidID)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.OrderType)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.PayState)
                .IsUnicode(false);

            modelBuilder.Entity<OrderSearch>()
                .Property(e => e.OrderID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OrderSearch>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderSearch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Salt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.SaltPassword)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.AndroidUserInfo)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Searchs)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.WXUserInfo)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<WXUserInfo>()
                .Property(e => e.Openid)
                .IsUnicode(false);
        }
    }
}
