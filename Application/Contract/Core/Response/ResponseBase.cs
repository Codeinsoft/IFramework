using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IFramework.Application.Contract.Core.Response
{
    public class ResponseBase
    {
        public int ProcessId { get; set; }
        public string Token { get; set; }
        public HttpStatusCode ResultCode { get; set; }
        public IList<ErrorMessageDto> ErrorMessages { get; set; }
        public virtual void SetResponse(HttpStatusCode statusCode, params ErrorMessageDto[] errorMessages)
        {
            ResultCode = HttpStatusCode.Forbidden;
            ErrorMessages = errorMessages.ToList();
        }
    }
}
