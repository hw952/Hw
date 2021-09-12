

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
    public class RoleMenuListDto : BaseListDto
    {

        /// <summary>
        ///角色名称
        /// <summary>
        [DescriptionAttribute("角色名称")]
        public string RoleName { get; set; }

        /// <summary>
        ///菜单名称
        /// <summary>
        [DescriptionAttribute("菜单名称")]
        public string MenuName { get; set; }

        
    }

}
            
            