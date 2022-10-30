using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Interfaces
{
    public interface ICacheProvider
    {
        string GetCacheKey(string invocationTargetTypeName, string getConcreteMethodName, object[] invocationArgs);
    }
}
