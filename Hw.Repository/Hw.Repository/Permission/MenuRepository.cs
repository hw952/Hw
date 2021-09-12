

using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Hw.IRepository;
using FreeSql;
using Hw.Model.Permission;
using Hw.IRepository.Permission;

namespace Hw.Repository.Permission
{

    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuRepository : HwRepository<Menu>,IMenuRepository
    {
        IFreeSql _fsql;
        public MenuRepository(IFreeSql fsql) : base(fsql)
        {
            _fsql = fsql;
        }

    }
}


            