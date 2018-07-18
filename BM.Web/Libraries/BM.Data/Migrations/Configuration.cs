using BM.Core.Description;

namespace BM.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BM.Data.BMObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BM.Data.BMObjectContext";
        }

        protected override void Seed(BMObjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var updater = new DescriptionUpdater<BMObjectContext>(context);
            updater.UpdateDatabaseDescriptions();
        }
    }
}
