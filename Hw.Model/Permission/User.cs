

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
using Hw.Model.Permission;
namespace Hw.Model.Permission
{
     /// <summary>
    /// 用户管理
    /// <summary>
    [Description("用户管理")]
    public class User : BaseModel
    {

        /// <summary>
        ///密码
        /// <summary>
        [DescriptionAttribute("密码")]
        public string Password { get; set; }

        /// <summary>
        ///账户名
        /// <summary>
        [DescriptionAttribute("账户名")]
        public string UserName { get; set; }

        /// <summary>
        ///用户角色管理
        /// <summary>
        [DescriptionAttribute("用户角色管理")]
        public  List<UserRole> UserRoles { get; set; }

        
    }

}
            
            