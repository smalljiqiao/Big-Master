using Autofac;
using BM.Core.Data;
using BM.Core.Infrastructure;
using BM.Core.Infrastructure.DependencyManagement;
using BM.Core.Installation;
using BM.Data;
using BM.Services.Installation;

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
        }

        public int Order => 0;
    }
}
