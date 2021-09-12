
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw.Dto.Dto
{
    public class AddDtoAttribute : Attribute
    {

        public AddDtoType AddDtoType { get; set; } = AddDtoType.Text;

        public string Url { get; set; }

        public string VlaueUrl { get; set; }
    }

    public enum AddDtoType
    {
        /// <summary>
        /// 文本输入
        /// </summary>
        Text,
        /// <summary>
        /// 枚举下拉框
        /// </summary>
        EmunCommbox,
        /// <summary>
        /// 树形下拉框
        /// </summary>
        TreeCommbox,
        /// <summary>
        /// Switch开关
        /// </summary>
        Switch,

        /// <summary>
        /// 树形下拉多选框
        /// </summary>
        TreeMultipleCommbox,

        /// <summary>
        /// 下拉多选框
        /// </summary>
        EmunMultipleCommbox,

        //密码输入框
        Password,
        SelectIcon,

    }
}