

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.Permission;
namespace Hw.Model.Permission
{
     /// <summary>
    /// 角色管理
    /// <summary>
    [Description("角色管理")]
    public class Role : BaseModel
    {

        /// <summary>
        ///用户角色管理
        /// <summary>
        [DescriptionAttribute("用户角色管理")]
        public  List<UserRole> UserRoles { get; set; }

        /// <summary>
        ///角色菜单管理
        /// <summary>
        [DescriptionAttribute("角色菜单管理")]
        public  List<RoleMenu> RoleMenus { get; set; }

        
    }

}
            
            