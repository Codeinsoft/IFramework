
using IFramework.Application.Contract.Core.Response;
using IFramework.Infra.Transversal.Validation.Abstract;
using System.Collections.Generic;

namespace IFramework.Infra.Transversal.Validation.Factory
{
    public static class IFrameworkValidatorFactory
    {
        public static IFrameworkValidationResult Succeded()
        {
            var validationResult = new IFrameworkValidationResult();
            return validationResult;
        }

        public static IFrameworkValidationResult Failure(List<ErrorMessageDto> errors)
        {
            var validationResult = new IFrameworkValidationResult();
            validationResult.Errors = errors;

            return validationResult;

        }
    }
}
