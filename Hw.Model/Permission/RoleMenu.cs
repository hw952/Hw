

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.Permission;
namespace Hw.Model.Permission
{
     /// <summary>
    /// 角色菜单管理
    /// <summary>
    [Description("角色菜单管理")]
    public class RoleMenu : BaseModel
    {

        /// <summary>
        ///角色
        /// <summary>
        [DescriptionAttribute("角色")]
        public int? RoleId { get; set; }

        /// <summary>
        ///角色名称
        /// <summary>
        [DescriptionAttribute("角色名称")]
        public string RoleName { get; set; }

        /// <summary>
        ///菜单
        /// <summary>
        [DescriptionAttribute("菜单")]
        public int? MenuId { get; set; }

        /// <summary>
        ///菜单名称
        /// <summary>
        [DescriptionAttribute("菜单名称")]
        public string MenuName { get; set; }

        /// <summary>
        ///菜单管理
        /// <summary>
        [DescriptionAttribute("菜单管理")]
        public  Menu Menu { get; set; }

        /// <summary>
        ///角色管理
        /// <summary>
        [DescriptionAttribute("角色管理")]
        public  Role Role { get; set; }

        
    }

}
            
            