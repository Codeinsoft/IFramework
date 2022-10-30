using FluentNHibernate.Mapping;
using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class UserInfoMap : ClassMap<UserInfo>
    {
        public UserInfoMap()
        {
            Table("UserInfo");
            Id(e => e.Id).Column("Id").GeneratedBy.Identity().Not.Nullable();
            Map(u => u.Name);
            Map(u => u.LastName);
            Map(u => u.ProfileImage);



            Map(e => e.CreatedBy).Column("CreatedBy").Length(100).Default("'Kullanıcı adı bulunamadı.'");
            Map(e => e.CreatedDate).Column("CreatedDate");
            Map(e => e.Status);
            Map(e => e.UpdatedBy).Column("UpdatedBy").Length(100);
            Map(e => e.UpdatedDate).Column("UpdatedDate");

            //References(u => u.UserInfo);
            //HasMany(u => u.Address).KeyColumn("UserId").ReadOnly().Cascade.All();

            //HasMany(u => u.Claims).KeyColumn("UserId").Cascade.All();
            //HasMany(u => u.Logins).KeyColumn("UserId").Cascade.All();
        }
    }
}
