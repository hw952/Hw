

using System.Collections.Generic;          
using System.ComponentModel;
using System;
using Hw.Model;
using Hw.Dto.Permission;
using Hw.Dto.Dto;
using System.Linq;
namespace Hw.Dto.Permission
{
     /// <summary>
    /// 用户管理
    /// <summary>
    [Description("用户管理")]
    public class UserUpdateDto : BaseUpdateDto
    {

        private List<int?> _Role;

        /// <summary>
        ///角色
        /// <summary>
        [AddDto(AddDtoType = AddDtoType.EmunMultipleCommbox, Url = "/Role/QueryAll")]
        [DescriptionAttribute("角色")]
        public List<int?> Role { get { return _Role; } set { _Role = value; _UserRoles = value?.Select(d => new UserRoleAddDto() { RoleId = d }).ToList(); } }

        private List<UserRoleAddDto> _UserRoles;

        /// <summary>
        ///角色
        /// <summary>
         [NoFiled]
        [DescriptionAttribute("角色")]
        public List<UserRoleAddDto> UserRoles { get { return _UserRoles; } set { _UserRoles = value; _Role = value?.Select(d => d.RoleId).ToList(); } }

        
    }

}
            
            