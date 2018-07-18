using System.Data.Common;

namespace BM.Core.Data
{
    public interface IDataProvider
    {
        void SetDatabaseInitializer();

        DbParameter GetParameter();
    }
}
