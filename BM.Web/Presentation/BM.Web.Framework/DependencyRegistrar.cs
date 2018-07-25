using Autofac;
using BM.Core.Data;
using BM.Core.Infrastructure;
using BM.Core.Infrastructure.DependencyManagement;
using BM.Core.Installation;
using BM.Core.Localization;
using BM.Data;
using BM.Services.Installation;
using BM.Services.Localization;

namespace BM.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<WebAppTypeFinder>().As<ITypeFinder>();
            builder.RegisterType<EfStartupTask>().As<IStartupTask>();
            builder.RegisterType<SqlServerDataProvider>().As<IDataProvider>();
            builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>();

            //注入数据库上下文类
            builder.RegisterType<BMObjectContext>().As<IDbContext>();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));

            builder.RegisterType<LocalizationService>().As<ILocalizationService>();
        }

        public int Order => 0;
    }
}
