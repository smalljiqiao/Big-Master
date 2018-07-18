using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace BM.Core.Description
{
    public static class DescriptionExtension
    {
        public static string GetTableName(this DbContext context, Type tableType)
        {
            var method = typeof(DescriptionExtension).GetMethod("GetTableName", new Type[] { typeof(DbContext) })
                ?.MakeGenericMethod(new Type[] { tableType });

            return (string)method.Invoke(context, new object[] { context });
        }

        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this ObjectContext context) where T : class
        {
            var sql = context.CreateObjectSet<T>().ToTraceString();
            var regex = new Regex("FROM (?<table>.*) AS");
            var match = regex.Match(sql);

            var table = match.Groups["table"].Value;
            return table;
        }
    }
}
