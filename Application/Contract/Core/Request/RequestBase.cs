using System;

namespace IFramework.Application.Contract.Core.Request
{

    public class RequestBase
    {
        //[IgnoreCacheKey] // TODO
        public int ProcessId { get; set; }
        //[IgnoreCacheKey] // TODO
        public Guid UserId { get; set; }
        public int FromId { get; set; }
        public bool GetTrash { get; set; }
    }
}
