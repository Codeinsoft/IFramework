
namespace IFramework.Application.Contract.Core.Request
{
    public class PagingRequest : RequestBase
    {
        public int PageNo { get; set; }
        public int PageRowCount { get; set; }
    }
}
