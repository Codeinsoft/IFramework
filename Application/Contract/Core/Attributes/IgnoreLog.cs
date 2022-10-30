using System;

namespace IFramework.Application.Contract.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
    public class IgnoreLog : Attribute
    {

    }
}
