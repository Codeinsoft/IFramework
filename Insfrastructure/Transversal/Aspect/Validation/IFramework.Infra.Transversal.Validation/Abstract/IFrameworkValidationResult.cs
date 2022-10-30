
using IFramework.Application.Contract.Core.Response;
using System.Collections.Generic;

namespace IFramework.Infra.Transversal.Validation.Abstract
{
    public class IFrameworkValidationResult
    {
        public List<ErrorMessageDto> Errors { get; set; }

        public bool IsValid
        {
            get
            {
                return 
                    this.Errors == null 
                    ? true
                    : this.Errors.Count == 0;
            }
        }
    }
}
