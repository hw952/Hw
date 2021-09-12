

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
    /// 角色菜单管理
    /// <summary>
    [Description("角色菜单管理")]
    public class RoleMenuUpdateDto : BaseUpdateDto
    {

        /// <summary>
        ///角色
        /// <summary>
        [DescriptionAttribute("角色")]
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/Role/QueryAll")]
        public int? RoleId { get; set; }

        /// <summary>
        ///菜单
        /// <summary>
        [DescriptionAttribute("菜单")]
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/Menu/QueryAll")]
        public int? MenuId { get; set; }

        
    }

}
            
            