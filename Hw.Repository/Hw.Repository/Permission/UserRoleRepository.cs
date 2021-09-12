

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
    /// 用户角色管理
    /// </summary>
    public class UserRoleRepository : HwRepository<UserRole>,IUserRoleRepository
    {
        IFreeSql _fsql;
        public UserRoleRepository(IFreeSql fsql) : base(fsql)
        {
            _fsql = fsql;
        }

    }
}


            