using FluentNHibernate.Mapping;
using IFramework.Domain.History;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class HistoryMap:ClassMap<History>
    {
        public HistoryMap()
        {
            Id(h => h.ProcessId).GeneratedBy.Identity().Not.Nullable();
            Map(h => h.MethodExecutionTime).Not.Nullable();
            Map(h => h.MethodInputParameters).Nullable();
            Map(h => h.MethodName).Not.Nullable();
            Map(h => h.MethodOutput).Nullable();
            Map(h => h.ProcessInfo).Not.Nullable();

            References(h => h.User).Cascade.All().Column("UserId");
        }
    }
}
