using AutoMapper;
using IFramework.Application.Contract.UserDto;
using IFramework.Domain.User;
using IFramework.Application.Contract.Authentication;

namespace IFramework.Infrastructure.Transversal.Mapper.AutoMapper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();//.ForMember(p=>p.Roles,opt=>opt.MapFrom(t=>t.Roles==null?new List<IRoleDto>(): Mapper.Map<IList<IRoleDto>>(t.Roles)));
                                        //cfg.CreateMap<User, UserDto>();
                                        //cfg.CreateMap<UserDto, User>();
            CreateMap<LoginUserRequestDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>().ForMember(x => x.RequestBase, opt => opt.Ignore());
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserInfoDto, UserInfo>();
            CreateMap<UserInfoDto, UserInfo>();
        }
    }
}
