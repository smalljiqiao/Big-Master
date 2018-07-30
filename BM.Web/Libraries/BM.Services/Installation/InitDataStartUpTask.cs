using BM.Core.Data;
using BM.Core.Domain.Localization;
using BM.Core.Infrastructure;
using BM.Core.Installation;
using System.Linq;

namespace BM.Services.Installation
{
    public class InitDataStartUpTask : IStartupTask
    {
        /// <summary>
        /// 如果codeMessage表中不存在数据，启用初始化数据任务
        /// 因为CodeMessage表会在初始化数据库数据时会添加数据
        /// </summary>
        public void Execute()
        {
            var codeMeesageRepository = EngineContext.Current.Resolve<IRepository<CodeMessage>>();

            
            var query = from c in codeMeesageRepository.Table select c;

            if (query.FirstOrDefault() != null) return;

            var installationService = EngineContext.Current.Resolve<IInstallationService>();
            installationService?.InstallData();
        }

        public int Order => -999;
    }
}
