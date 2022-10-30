
namespace IFramework.Application.Contract.Core.Response
{
    public class ErrorMessageDto 
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public ErrorType ErrorType { get; set; }
        public string PropertyName { get; set; }
    }
}
