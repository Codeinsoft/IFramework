using System;

namespace IFramework.Domain.History
{
    public class History
    {
        public virtual int ProcessId { get; set; }
        public virtual string ProcessInfo { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string MethodInputParameters { get; set; }
        public virtual string MethodOutput { get; set; }
        public virtual DateTime MethodExecutionTime { get; set; }

        public virtual User.User User { get; set; }
    }
}
