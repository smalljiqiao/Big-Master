using BM.Data.Domain;
using BM.Services.Caching;
using System.Collections.Generic;
using System.Linq;

namespace BM.Services.Localization
{
    public partial class LocalizationService
    {
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "Exm.lsr.all";

        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues()
        {
            var cacheManager = new MemoryCacheManager();

            var db = new DbEntities();

            var key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY);
            return cacheManager.Get(key, () =>
            {
                var query = from l in db.CodeMessages
                            orderby l.Code
                            select l;
                var locales = query.ToList();
                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var locale in locales)
                {
                    var code = locale.Code.ToLowerInvariant();
                    if (!dictionary.ContainsKey(code))
                        dictionary.Add(code, new KeyValuePair<int, string>(locale.Id, locale.Message));
                }
                return dictionary;
            });
        }

        public virtual string GetResource(string resourceKey)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();

            var resources = GetAllResourceValues();
            if (resources.ContainsKey(resourceKey))
                result = resources[resourceKey].Value;

            if (string.IsNullOrEmpty(result))
            {
                //需添加日志记录
                //原样返回resourceKey好像不太好，可以返回没有找到之类的，联系管理员之类的话语。

                result = resourceKey;
            }

            return result;
        }
    }
}
