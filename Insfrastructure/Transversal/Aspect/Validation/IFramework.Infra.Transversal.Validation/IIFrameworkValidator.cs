using IFramework.Infra.Transversal.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infra.Transversal.Validation
{
    public interface IIFrameworkValidator
    {
        public IFrameworkValidationResult Validate(object argument);
    }
}
