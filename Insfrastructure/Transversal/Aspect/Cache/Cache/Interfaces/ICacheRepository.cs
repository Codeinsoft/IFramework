using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Interfaces
{
    public interface ICacheRepository<TValue> where TValue : new()
    {
        TValue Get(string key);
        void Set(string key, object value, int duration);
        void Delete(string key);
        void DeleteKeysByPattern(string pattern);
        bool KeyExists(string key);

    }
}
