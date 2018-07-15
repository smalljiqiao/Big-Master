using BM.Core.Data;
using BM.Core.Infrastructure;

namespace BM.Data
{
    public class EfStartupTask:IStartupTask
    {
        public void Execute()
        {
            var provider = EngineContext.Current.Resolve<IDataProvider>();
            provider?.SetDatabaseInitializer();
        }

        public int Order => -1000;
    }
}
