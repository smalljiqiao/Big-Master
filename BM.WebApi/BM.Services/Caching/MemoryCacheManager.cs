using System;
using System.Runtime.Caching;

namespace BM.Services.Caching
{
    public partial class MemoryCacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;


        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        public virtual void Dispose()
        {
        }
    }
}
