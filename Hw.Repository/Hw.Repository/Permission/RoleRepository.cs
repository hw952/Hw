

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
    /// 角色管理
    /// </summary>
    public class RoleRepository : HwRepository<Role>,IRoleRepository
    {
        IFreeSql _fsql;
        public RoleRepository(IFreeSql fsql) : base(fsql)
        {
            _fsql = fsql;
        }

    }
}


            