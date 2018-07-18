using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BM.Data
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;

        int SaveChanges();

        int ExecuteSqlCommand(string sql, bool doNoteEnsureTransaction = false, int? timeout = null, params object[] parameters);

        DbEntityEntry Entry(object entity);

        void Dispose();
    }
}
