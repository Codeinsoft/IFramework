using System;
using System.Collections.Generic;

namespace IFramework.Application.Contract.Core.Response
{
    public class PagingResponse<T> : ResponseBase
    {
        public int TotalRowCount { get; set; }

        public int TotalPageNumber
        {
            get { return TotalRowCount == 0 ? 0 : (int)Math.Ceiling((decimal)TotalRowCount / PageRowCount); }
        }

        public List<T> List { get; set; }
        public int PageRowCount { get; set; }
        public int PageNumber { get; set; }
    }
}
