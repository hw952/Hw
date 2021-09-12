

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.Permission;
namespace Hw.Model.Permission
{
     /// <summary>
    /// 用户角色管理
    /// <summary>
    [Description("用户角色管理")]
    public class UserRole : BaseModel
    {

        /// <summary>
        ///用户
        /// <summary>
        [DescriptionAttribute("用户")]
        public int? UserId { get; set; }

        /// <summary>
        ///用户名称
        /// <summary>
        [DescriptionAttribute("用户名称")]
        public string UserName { get; set; }

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
        ///用户管理
        /// <summary>
        [DescriptionAttribute("用户管理")]
        public  User User { get; set; }

        /// <summary>
        ///角色管理
        /// <summary>
        [DescriptionAttribute("角色管理")]
        public  Role Role { get; set; }

        
    }

}
            
            