

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
namespace Hw.Model
{  
     /// <summary>
    /// 菜单类型
    /// <summary>
    [Description("菜单类型")]
    public enum MenuType
    {
     
        /// <summary>
        ///菜单
        /// <summary>
        [DescriptionAttribute("菜单")]
        Menu,

        /// <summary>
        ///添加
        /// <summary>
        [DescriptionAttribute("添加")]
        Add,

        /// <summary>
        ///修改
        /// <summary>
        [DescriptionAttribute("修改")]
        Update,

        /// <summary>
        ///删除
        /// <summary>
        [DescriptionAttribute("删除")]
        Del,

        /// <summary>
        ///搜索
        /// <summary>
        [DescriptionAttribute("搜索")]
        Search,

        
    }

}  