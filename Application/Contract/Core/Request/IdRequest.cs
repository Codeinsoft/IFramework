
namespace IFramework.Application.Contract.Core.Request
{
    public class IdRequest<TPrimaryKey> : RequestBase
    {
        public TPrimaryKey Id { get; set; }
    }
}
