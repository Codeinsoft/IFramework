using IFramework.Infrastructure.Transversal.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace IFramework.Infrastrucutre.Transversal.Cache.InMem.Service
{
    public class InMemoryCacheRepository<TValue> : ICacheRepository<TValue>
       where TValue : new()
    {

        public IMemoryCache Cache { get; set; }

        public InMemoryCacheRepository(IMemoryCache cache)
        {
            this.Cache = cache;
        }

        public void Delete(string key)
        {
            Cache.Remove(key);
        }

        public TValue Get(string key)
        {
            if (KeyExists(key))
            {
                return Cache.Get<TValue>(key);
            }
            else
            {
                return default(TValue);
            }
            
        }

        public bool KeyExists(string key)
        {
            return Cache.TryGetValue(key, out object value);
        }

        public void Set(string key, object value, int duration)
        {
            Cache.Set(key, value, DateTimeOffset.Now.AddTicks(duration));
        }

        public void DeleteKeysByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
