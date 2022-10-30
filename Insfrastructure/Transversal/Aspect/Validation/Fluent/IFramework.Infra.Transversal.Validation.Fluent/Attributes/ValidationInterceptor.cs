using Castle.DynamicProxy;
using Castle.MicroKernel;
using FluentValidation;
using IFramework.Application.Contract.UserDto;
using IFramework.Infra.Transversal.Validation.Attributes;
using IFramework.Infra.Transversal.Validation.Fluent.Validators;
using IFramework.Infra.Transversal.Validation.Fluent.Validators.Abstract;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infra.Transversal.Validation.Fluent.Attributes
{
    public class ValidationInterceptor : IInterceptor
    {

        public ValidationInterceptor()
        {

        }

        public void Intercept(IInvocation invocation)
        {

            if (!IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseValidation || !(invocation.TargetType.IsDefined(typeof(ValidationAttribute), true) || invocation.MethodInvocationTarget.IsDefined(typeof(ValidationAttribute), true)))
            {
                invocation.Proceed();
                return;
            }

            // arguman yoksa validasyona gerek yok
            if (invocation.Arguments?.Length == 0)
            {
                invocation.Proceed();
                return;
            }

            // birden fazla arguman olmamali 
            if (invocation.Arguments?.Length > 1)
            {
                throw new System.Exception("Argument count cannot be more than one");
            }

            var argument = invocation.Arguments[0];

            var validator = IoCResolver.Instance.ReleaseInstance<IIFrameworkValidator>();
            var result = validator.Validate(argument);
            if (result.IsValid)
            {
                invocation.Proceed();
            }
            else
            {
                // TODO: Exception turune karar ver
                string errors = "";
                result.Errors.ForEach(error =>
                {
                    errors += error.Message + Environment.NewLine;
                });
                throw new Exception("Validation Error: " + Environment.NewLine + errors);
            }

            //var validatorFactory = new FluentValidator();
            //var validator = validatorFactory.GetValidator(argument.GetType());
            //var validationResult = validator.Validate(argument);
            //if (validationResult.IsValid)
            //{
            //    invocation.Proceed();
            //}
            //else
            //{
            //    var errors = string.Join(",", validationResult.Errors);
            //    throw new Exception("Validation Error: " + Environment.NewLine + errors);
            //}
        }
    }
}
