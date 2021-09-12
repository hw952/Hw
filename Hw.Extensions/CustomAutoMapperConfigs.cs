

using AutoMapper;
using Hw.Dto.Permission;
using Hw.Model.Permission;


namespace Hw.Extensions
{
    /// <summary>
    /// Automapper 自动映射
    /// </summary>
    public class CustomAutoMapperConfigs : Profile
    {

        public CustomAutoMapperConfigs()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserListDto>();
            CreateMap<UserRoleAddDto, UserRole>();
            CreateMap<UserRoleUpdateDto, UserRole>();
            CreateMap<UserRole, UserRoleListDto>();
            CreateMap<RoleAddDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
            CreateMap<Role, RoleListDto>();
            CreateMap<MenuAddDto, Menu>();
            CreateMap<MenuUpdateDto, Menu>();
            CreateMap<Menu, MenuListDto>();
            CreateMap<RoleMenuAddDto, RoleMenu>();
            CreateMap<RoleMenuUpdateDto, RoleMenu>();
            CreateMap<RoleMenu, RoleMenuListDto>();
         

        }
    }

}


            