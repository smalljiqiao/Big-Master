using System;
using System.Collections.Generic;

namespace BM.Services.Infrastructure
{
    /// <summary>
    /// A staticallly compiled "singleton" used to store objects throughout the
    /// lifetime of the app domain. Not so much singleton in the pattern's
    /// sense of the word as a standardized way to store single instances.
    /// </summary>
    /// <typeparam name="T">The type of object to store.</typeparam>
    /// <remarks>Access to the instance is not syschrnoized.</remarks>
    public class Singleton<T> : Singleton
    {
        private static T _instance;

        /// <summary>
        /// The singleton instance for the specified type T.
        /// Only one instance (at the time) of this object for each type of T.
        /// </summary>
        public static T Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// Provides access to all "singletons" stored by <see cref="Singleton{T}"/>>
    /// </summary>
    public class Singleton
    {
        public static IDictionary<Type, object> AllSingletons { get; }

        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }
    }
}
