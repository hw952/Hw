

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
    public class MenuListDto : BaseListDto
    {

        /// <summary>
        ///用于菜单的导航属性[Freesql特性]
        /// <summary>
        public List<MenuListDto> Children { get; set; }

        /// <summary>
        ///链接地址
        /// <summary>
        [DescriptionAttribute("链接地址")]
        public string Url { get; set; }

        /// <summary>
        ///菜单类型
        /// <summary>
        [AddDto(AddDtoType = AddDtoType.EmunCommbox, Url = "/Common/GetEmunCommbox?enumName=MenuType")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonEnumDescriptionConvert))]
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
            
            