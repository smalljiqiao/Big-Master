using System;

namespace BM.Services.Caching
{
    public static class CacheExtension
    {
        public static T Get<T>(this MemoryCacheManager cacheManager, string key, Func<T> acquire, int cacheTime = 60)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            var result = acquire();
            if(cacheTime>0)
                cacheManager.Set(key,result,cacheTime);

            return result;
        }
    }
}
