using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace BM.Data.Initializers
{
    public class CreateTablesIfNotExist<T> : IDatabaseInitializer<T> where T: DbContext
    {
        private readonly string[] _customCommands;

        public CreateTablesIfNotExist(string[] customCommands)
        {
            this._customCommands = customCommands;
        }

        public void InitializeDatabase(T context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }

            if (dbExists)
            {
                var numberOfTables = 0;
                foreach (var t1 in context.Database.SqlQuery<int>(
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'"))
                {
                    numberOfTables = t1;
                }

                var createTables = numberOfTables == 0;

                if (createTables)
                {
                    var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                    context.Database.ExecuteSqlCommand(dbCreationScript);
                    context.SaveChanges();

                    if (_customCommands != null && _customCommands.Length > 0)
                    {
                        foreach (var command in _customCommands)
                            context.Database.ExecuteSqlCommand(command);
                    }
                }
            }
            else
            {
                throw new ApplicationException("No database instance");
            }
        }
    }
}
