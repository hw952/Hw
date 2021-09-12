

using System.Collections.Generic;          
using FreeSql.DataAnnotations;
using System.ComponentModel;
using System;
namespace Hw.Model
{
    /// <summary>
    /// 基类
    /// <summary>    
    public class BaseModel
    {

        /// <summary>
        ///pk
        /// <summary>
        [DescriptionAttribute("pk")]
         [Column(IsPrimary = true, IsIdentity = true)]
        public int? Id { get; set; }

        /// <summary>
        ///乐观锁
        /// <summary>
        [DescriptionAttribute("乐观锁")]
        public int? Revision { get; set; }

        /// <summary>
        ///创建人
        /// <summary>
        [DescriptionAttribute("创建人")]
        public string CreatedBy { get; set; }

        /// <summary>
        ///创建时间
        /// <summary>
        [DescriptionAttribute("创建时间")]
         [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///更新人
        /// <summary>
        [DescriptionAttribute("更新人")]
        public string UpdatedBy { get; set; }

        /// <summary>
        ///更新时间
        /// <summary>
        [DescriptionAttribute("更新时间")]
         [Column(ServerTime = DateTimeKind.Utc)]
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        ///名称
        /// <summary>
        [DescriptionAttribute("名称")]
        public string Name { get; set; }

        /// <summary>
        ///是否启用
        /// <summary>
        [DescriptionAttribute("是否启用")]
        public bool Enables { get; set; }


    }
    

}
            
            