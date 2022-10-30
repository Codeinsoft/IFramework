using FluentNHibernate.Mapping;

namespace IFramework.Infrastructure.Persistence.FNhibernate.Mapping
{
    public class UserMap:ClassMap<IFramework.Domain.User.User>
    {
        public UserMap()
        {
            Table("Users");
            Id(e => e.Id).Column("Id").GeneratedBy.Guid().Not.Nullable();
            Map(u => u.PasswordHash);
            Map(u => u.SecurityStamp);

            Map(u => u.Email).Length(256).Not.Nullable();


            Map(e => e.CreatedBy).Column("CreatedBy").Length(100).Default("'Kullanıcı adı bulunamadı.'");
            Map(e => e.CreatedDate).Column("CreatedDate");
            Map(e => e.Status);
            Map(e => e.UpdatedBy).Column("UpdatedBy").Length(100);
            Map(e => e.UpdatedDate).Column("UpdatedDate");



            References(u => u.UserInfo).Cascade.All();
            HasMany(u => u.Histories).Cascade.All();
            //HasMany(u => u.Address).KeyColumn("UserId").ReadOnly().Cascade.All();

            References(u => u.Role);//.Cascade.All();
            //HasManyToMany(u => u.Roles).Table("UserRole").ParentKeyColumn("RoleId").ChildKeyColumn("UserId").Cascade.SaveUpdate();//.All();//.All();
            //HasMany(u => u.Claims).KeyColumn("UserId").Cascade.All();
            //HasMany(u => u.Logins).KeyColumn("UserId").Cascade.All();


            /*
             * Cascade.All -> Tüm bağlantıda olanları siliyor.
             * Cascade.SaveUpdate -> many to many'de sadece bağlantı tablosundan siliyor.
             * 
             */
        }
    }
}
