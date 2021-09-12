

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.Permission;
namespace Hw.Model.Permission
{
     /// <summary>
    /// 菜单管理
    /// <summary>
    [Description("菜单管理")]
    public class Menu : BaseModel
    {

        /// <summary>
        ///父级Id
        /// <summary>
        [DescriptionAttribute("父级Id")]
        public int? ParentId { get; set; }

        /// <summary>
        ///用于菜单的导航属性[Freesql特性]
        /// <summary>
        [Navigate(nameof(ParentId))]
        public List<Menu> Children { get; set; }

        /// <summary>
        ///链接地址
        /// <summary>
        [DescriptionAttribute("链接地址")]
        public string Url { get; set; }

        /// <summary>
        ///菜单类型
        /// <summary>
        [DescriptionAttribute("菜单类型")]
        public MenuType ActionType { get; set; }

        /// <summary>
        ///图标
        /// <summary>
        [DescriptionAttribute("图标")]
        public string Icon { get; set; }

        /// <summary>
        ///角色菜单管理
        /// <summary>
        [DescriptionAttribute("角色菜单管理")]
        public  List<RoleMenu> RoleMenus { get; set; }

        
    }

}
            
            