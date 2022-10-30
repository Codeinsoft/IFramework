using FluentValidation;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infra.Transversal.Validation.Abstract;
using IFramework.Infra.Transversal.Validation.Factory;
using IFramework.Infrastructure.Constants.Exception;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IFramework.Infra.Transversal.Validation.Fluent
{
    public class FluentValidator : ValidatorFactoryBase, IValidatorFactory, IIFrameworkValidator
    {
        public IValidator GetValidator(Type type)
        {
            IValidator validator;
            try
            {
                validator = CreateInstance(typeof(IValidator<>).MakeGenericType(type));
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(IoCContainerExceptionConstants.VALIDATOR_NOT_IMPLEMENTED, ex);
            }
            return validator;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return IoCResolver.Instance.ReleaseInstance(validatorType) as IValidator;
        }

        public IFrameworkValidationResult Validate(object argument)
        {
            var validator = this.GetValidator(argument.GetType());
            var validationResult = validator.Validate(argument);
            if (!validationResult.IsValid)
            {
                var errorMessages = new List<ErrorMessageDto>();
                foreach (var error in validationResult.Errors)
                {
                    var errorMessage = IoCResolver.Instance.ReleaseInstance<ErrorMessageDto>();
                    errorMessage.Code = "Validation";
                    errorMessage.ErrorType = ErrorType.Validation;
                    errorMessage.Message = error.ErrorMessage;

                    errorMessages.Add(errorMessage);
                }

                return IFrameworkValidatorFactory.Failure(errorMessages);
            }
            else
            {
                return IFrameworkValidatorFactory.Succeded();
            }
        }
    }
}
