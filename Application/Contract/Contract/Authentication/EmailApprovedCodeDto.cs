using System;
using IFramework.Application.Contract.Core.Request;

namespace IFramework.Application.Contract.Authentication
{
    public class EmailApprovedCodeDto : RequestBase
    {
        public string IpAddress { get; set; }
        public Guid ApprovedCode { get; set; }
    }
}
