

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
    /// 用户管理
    /// <summary>
    [Description("用户管理")]
    public class UserListDto : BaseListDto
    {

        /// <summary>
        ///账户名
        /// <summary>
        [DescriptionAttribute("账户名")]
        public string UserName { get; set; }

        
    }

}
            
            