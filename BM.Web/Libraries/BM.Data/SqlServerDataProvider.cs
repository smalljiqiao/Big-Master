using System;
using System.Collections.Generic;
using BM.Core.Data;
using BM.Core.Infrastructure;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using BM.Core;
using BM.Data.Initializers;

namespace BM.Data
{
    public class SqlServerDataProvider : IDataProvider
    {
        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException($"Specified file doesn't exist - {filePath}");

                return new string[0];
            }

            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }


        protected virtual string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }

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


            var customCommands = new List<string>();
            customCommands.AddRange(ParseCommands(CommonHelper.MapPath("~/App_Data/Install/SqlServer.StoredProcedures.sql"), true));

            var initializer = new CreateTablesIfNotExist<BMObjectContext>(customCommands.ToArray());
            Database.SetInitializer(initializer);
        }

        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
