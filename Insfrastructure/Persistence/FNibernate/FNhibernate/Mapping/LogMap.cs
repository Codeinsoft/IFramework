using FluentNHibernate.Mapping;
using IFramework.Domain.Log;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class LogMap:ClassMap<Log>
    {
        public LogMap()
        {

            Table("Log");
            Id(e => e.Id).Column("Id").GeneratedBy.Identity().Not.Nullable();
            Map(u => u.Date);
            Map(u => u.Exception);
            Map(u => u.Level);
            Map(u => u.Logger);
            Map(u => u.Message);
            Map(u => u.Thread);
            Map(u => u.Username);

            Map(e => e.CreatedBy).Column("CreatedBy").Length(100).Default("'Kullanıcı adı bulunamadı.'");
            Map(e => e.CreatedDate).Column("CreatedDate");
            Map(e => e.Status);
            Map(e => e.UpdatedBy).Column("UpdatedBy").Length(100);
            Map(e => e.UpdatedDate).Column("UpdatedDate");
        }
    }
}
