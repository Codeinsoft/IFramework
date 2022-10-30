using FluentValidation;
using IFramework.Application.Contract.Core.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IFramework.Infra.Transversal.Validation.Fluent.Validators.Abstract
{
    public class BaseValidator<T> : AbstractValidator<T> where T : RequestBase
    {
        public BaseValidator()
        {
            RuleFor(b => b.FromId).NotEqual(0).WithMessage("From Id Cannot Be 0");
        }
    }
}
