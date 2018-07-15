using BM.Core.Data;
using BM.Core.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;

namespace BM.Data
{
    public class SqlServerDataProvider : IDataProvider
    {
        public virtual void SetDatabaseInitializer()
        {
            var context = (BMObjectContext)EngineContext.Current.Resolve<IDbContext>();

            var databaseExist = context.Database.Exists();

            if (!databaseExist)
            {
                var connectionString = context.Database.Connection.ConnectionString;
                var builder = new SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;

                //If database not exist, will throw wrong. So create connection string to 'master' database. It always exists.
                builder.InitialCatalog = "master";

                var masterCatalogConnectionString = builder.ToString();

                var script = $"CREATE DATABASE [{databaseName}]";

                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(script, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
