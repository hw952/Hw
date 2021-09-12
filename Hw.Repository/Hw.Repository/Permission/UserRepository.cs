

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
    /// 用户管理
    /// </summary>
    public class UserRepository : HwRepository<User>,IUserRepository
    {
        IFreeSql _fsql;
        public UserRepository(IFreeSql fsql) : base(fsql)
        {
            _fsql = fsql;
        }

    }
}


            