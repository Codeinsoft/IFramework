using System;
using System.Collections.Generic;
using IFramework.Transversal.Core.Exceptions;

namespace IFramework.Infrastructure.Utility.CustomExceptions
{
    public class ValidationException : Exception
    {
        public List<ErrorMessageDto> Errors { get; }

        public ValidationException(string message, string code)
        {
            Errors = new List<ErrorMessageDto>
            {
               // new ErrorMessageDto {Message = message,Code = code,ErrorType = ErrorType.Validation}
               //IoC.Get<ErrorMessageDto>();
            };
        }
    }
}
