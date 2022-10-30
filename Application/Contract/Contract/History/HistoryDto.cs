using System;

namespace IFramework.Application.Contract.History
{
    public class HistoryDto
    {
        public int ProcessId { get; set; }
        public string ProcessInfo { get; set; }
        public string MethodName { get; set; }
        public Guid UserId { get; set; }
        public string MethodInputParameters { get; set; }
        public string MethodOutput { get; set; }
        public DateTime MethodExecutionTime { get; set; }

        public UserDto.UserDto User { get; set; }


    }
}
