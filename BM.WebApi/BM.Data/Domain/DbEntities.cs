namespace BM.Data.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }

        public virtual DbSet<Android> Android { get; set; }
        public virtual DbSet<BPrice> BPrice { get; set; }
        public virtual DbSet<BType> BType { get; set; }
        public virtual DbSet<BurialPoint> BurialPoint { get; set; }
        public virtual DbSet<CodeMessage> CodeMessage { get; set; }
        public virtual DbSet<DreamDetail> DreamDetail { get; set; }
        public virtual DbSet<DreamTitle> DreamTitle { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderSearch> OrderSearch { get; set; }
        public virtual DbSet<Search> Search { get; set; }
        public virtual DbSet<Sms> Sms { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Android>()
                .Property(e => e.AndroidId)
                .IsUnicode(false);

            modelBuilder.Entity<Android>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<BPrice>()
                .Property(e => e.OriginalCost)
                .HasPrecision(2, 2);

            modelBuilder.Entity<BPrice>()
                .Property(e => e.PreferentialCost)
                .HasPrecision(2, 2);

            modelBuilder.Entity<BurialPoint>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CodeMessage>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DreamTitle>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<DreamTitle>()
                .HasOptional(e => e.DreamDetail)
                .WithRequired(e => e.DreamTitle);

            modelBuilder.Entity<Order>()
                .Property(e => e.AndroidId)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderType)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Price)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.PayState)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasOptional(e => e.OrderSearch)
                .WithRequired(e => e.Order);

            modelBuilder.Entity<Sms>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Sms>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DefaultName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Salt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SaltPassword)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
