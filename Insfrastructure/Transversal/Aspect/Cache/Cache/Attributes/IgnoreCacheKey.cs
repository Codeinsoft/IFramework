using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
    public class IgnoreCacheKey : Attribute
    {

    }
}
