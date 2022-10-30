using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infra.Transversal.Validation.Attributes
{

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationAttribute : Attribute
    {

    }
}
