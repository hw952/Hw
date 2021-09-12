

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
    /// 角色菜单管理
    /// </summary>
    public class RoleMenuRepository : HwRepository<RoleMenu>,IRoleMenuRepository
    {
        IFreeSql _fsql;
        public RoleMenuRepository(IFreeSql fsql) : base(fsql)
        {
            _fsql = fsql;
        }

    }
}


            