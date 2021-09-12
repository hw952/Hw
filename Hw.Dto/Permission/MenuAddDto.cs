

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
    /// 菜单管理
    /// <summary>
    [Description("菜单管理")]
    public class MenuAddDto : BaseAddDto
    {

        /// <summary>
        ///父级Id
        /// <summary>
        [DescriptionAttribute("父级Id")]
        [AddDto(AddDtoType = AddDtoType.TreeCommbox, Url = "/Menu/QueryAll")]
        public int? ParentId { get; set; }

        /// <summary>
        ///链接地址
        /// <summary>
        [DescriptionAttribute("链接地址")]
        public string Url { get; set; }

        /// <summary>
        ///菜单类型
        /// <summary>
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/Common/GetEmunCommbox?enumName=MenuType")]
        [DescriptionAttribute("菜单类型")]
        public MenuType ActionType { get; set; }

        /// <summary>
        ///图标
        /// <summary>
        [DescriptionAttribute("图标")]
        [AddDto(AddDtoType = AddDtoType.SelectIcon)]
        public string Icon { get; set; }

        
    }

}
            
            