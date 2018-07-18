namespace BM.Data.Domain
{
    using System.Data.Entity;

    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=BigMaster")
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CodeMessage> CodeMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Salt)
                .IsFixedLength();
        }
    }
}
