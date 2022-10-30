using System.Net;

namespace IFramework.Application.Contract.Core.Response
{
    public class ResponseResult<T> : ResponseBase
    {
        public T Result { get; set; }

        public void SetResponse(HttpStatusCode statusCode, T result, params ErrorMessageDto[] errorMessages)
        {
            Result = result;
            base.SetResponse(statusCode, errorMessages);
        }
    }
}
