using Autofac;
using Autofac.Integration.Mvc;
using BM.Core.Infrastructure.DependencyManagement;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BM.Core.Infrastructure
{
    public class BMEngine : IEngine
    {
        private ContainerManager _containerManager;

        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startupTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startupTasks = startupTaskTypes.Select(startupTaskType => (IStartupTask) Activator.CreateInstance(startupTaskType)).ToList();

            //sort
            startupTasks = startupTasks.AsQueryable().OrderBy(s => s.Order).ToList();
            foreach(var startupTask in startupTasks)
                startupTask.Execute();
        }

        protected virtual void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //dependencies
            var typeFinder = new WebAppTypeFinder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = drTypes.Select(drType => (IDependencyRegistrar) Activator.CreateInstance(drType)).ToList();

            //sort
            drInstances = drInstances.AsQueryable().OrderBy(s => s.Order).ToList();
            foreach(var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder,typeFinder);

            var container = builder.Build();
            this._containerManager = new ContainerManager(container);

            //MVC中实现DI的类，例如控制器的注入等。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public void Initialize()
        {
            //依赖注入
            RegisterDependencies();

            //运行启动任务
            RunStartupTasks();
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        public ContainerManager ContainerManager
        {
            get => _containerManager;
        }
    }
}
