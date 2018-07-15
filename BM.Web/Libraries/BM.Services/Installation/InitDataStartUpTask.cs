using System.Linq;
using BM.Core.Data;
using BM.Core.Domain.Users;
using BM.Core.Infrastructure;
using BM.Core.Installation;

namespace BM.Services.Installation
{
    public class InitDataStartUpTask : IStartupTask
    {
        public void Execute()
        {
            var userRepository = EngineContext.Current.Resolve<IRepository<User>>();

            var query = from c in userRepository.Table select c;

            if (query.FirstOrDefault() == null)
            {
                var installationService = EngineContext.Current.Resolve<IInstallationService>();
                installationService?.InstallData();
            }
        }

        public int Order => -999;
    }
}
