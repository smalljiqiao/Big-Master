using System;
using System.Runtime.InteropServices.ComTypes;
using BM.Core.Infrastructure.DependencyManagement;

namespace BM.Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        void Initialize();

        T Resolve<T>() where T:class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
