using FluentNHibernate.Mapping;
using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class RoleMap:ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Role");
            Map(u => u.Name).Column("Name").Length(256);
            Map(e => e.AuthorizationListJson);
            Id(e => e.Id).Column("Id");
            Map(e => e.CreatedBy).Column("CreatedBy").Length(100).Default("'Kullanıcı adı bulunamadı.'");
            Map(e => e.CreatedDate).Column("CreatedDate");
            Map(e => e.Status);
            Map(e => e.UpdatedBy).Column("UpdatedBy").Length(100);
            Map(e => e.UpdatedDate).Column("UpdatedDate");
            //HasMany(u => u.Users).ReadOnly().Cascade.All();
        }
    }
}
