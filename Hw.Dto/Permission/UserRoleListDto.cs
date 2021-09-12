

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
    public class UserRoleListDto : BaseListDto
    {

        /// <summary>
        ///用户名称
        /// <summary>
        [DescriptionAttribute("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        ///角色名称
        /// <summary>
        [DescriptionAttribute("角色名称")]
        public string RoleName { get; set; }

        
    }

}
            
            