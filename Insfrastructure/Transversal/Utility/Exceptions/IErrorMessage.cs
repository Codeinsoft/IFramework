namespace IFramework.Transversal.Core.Exceptions
{
    public interface ErrorMessageDto
    {
        string Message { get; set; }
        string Code { get; set; }
        ErrorType ErrorType { get; set; }
        string PropertyName { get; set; }
    }
}
