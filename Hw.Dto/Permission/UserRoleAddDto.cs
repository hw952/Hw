

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
    /// 用户角色管理
    /// <summary>
    [Description("用户角色管理")]
    public class UserRoleAddDto : BaseAddDto
    {

        /// <summary>
        ///用户
        /// <summary>
        [DescriptionAttribute("用户")]
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/User/QueryAll")]
        public int? UserId { get; set; }

        /// <summary>
        ///角色
        /// <summary>
        [DescriptionAttribute("角色")]
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/Role/QueryAll")]
        public int? RoleId { get; set; }

        
    }

}
            
            