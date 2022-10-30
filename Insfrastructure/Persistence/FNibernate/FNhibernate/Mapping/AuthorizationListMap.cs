using FluentNHibernate.Mapping;

using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class AuthorizationListMap : ClassMap<AuthorizationList>
    {
        public AuthorizationListMap()
        {
            Table("AuthorizationList");
            Id(e => e.Id).Column("Id").GeneratedBy.Identity();
            Map(u => u.Action);
            Map(u => u.Container);
            Map(u => u.Title);
            
            Map(e => e.CreatedBy).Column("CreatedBy").Length(100).Default("'Kullanıcı adı bulunamadı.'");
            Map(e => e.CreatedDate).Column("CreatedDate");
            Map(e => e.Status);
            Map(e => e.UpdatedBy).Column("UpdatedBy").Length(100);
            Map(e => e.UpdatedDate).Column("UpdatedDate");
        }
    }
}