using IFramework.Infrastructure.Transversal.Aspect.Log;
using IFramework.Infrastructure.Transversal.Cache.Attributes;
using IFramework.Infrastructure.Transversal.Cache.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache
{
    public class CacheProvider : ICacheProvider
    {
        /// <summary>
        /// Class adı, method adı, method parametreleri ve cache keye dahil edilmeyecek property listesini alarak bunlara göre cache key oluşturur ve geri döner.
        /// </summary>
        /// <param name="invocationTargetTypeName">Aspect arguments.</param>
        /// <param name="getConcreteMethodName">Aspect arguments.</param>
        /// <param name="invocationArgs">Aspect arguments.</param>
        /// <returns>Cache key.</returns>
        public string GetCacheKey(string invocationTargetTypeName, string getConcreteMethodName, object[] invocationArgs)
        {
            const string divider = "{";

            var typeName = invocationTargetTypeName;

            var cacheKey = new StringBuilder();
            cacheKey.Append(typeName);
            cacheKey.Append(divider);
            cacheKey.Append(getConcreteMethodName);
            if (invocationArgs.Count() > 1) cacheKey.Append("{");
            foreach (var argument in invocationArgs)
            {
                cacheKey.Append("{" + argument.ToSerializeIgnoreAttribute<IgnoreCacheKey>() + "}" + ((invocationArgs.LastOrDefault() == argument) ? "" : ","));
            }
            if (invocationArgs.Count() > 1) cacheKey.Append("}");
            return cacheKey.ToString();

        }
    }
}
