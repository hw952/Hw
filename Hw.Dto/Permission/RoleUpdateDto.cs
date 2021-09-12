

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
    /// 角色管理
    /// <summary>
    [Description("角色管理")]
    public class RoleUpdateDto : BaseUpdateDto
    {

        private List<int?> _Menu;

        /// <summary>
        ///菜单
        /// <summary>
        [AddDto(AddDtoType = AddDtoType.TreeMultipleCommbox, Url = "/Menu/QueryAll")]
        [DescriptionAttribute("菜单")]
        public List<int?> Menu { get { return _Menu; } set { _Menu = value; _RoleMenus = value?.Select(d => new RoleMenuAddDto() { MenuId = d }).ToList(); } }

        private List<RoleMenuAddDto> _RoleMenus;

        /// <summary>
        ///菜单
        /// <summary>
         [NoFiled]
        [DescriptionAttribute("菜单")]
        public List<RoleMenuAddDto> RoleMenus { get { return _RoleMenus; } set { _RoleMenus = value; _Menu = value?.Select(d => d.MenuId).ToList(); } }

        
    }

}
            
            