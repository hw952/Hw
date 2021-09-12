using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Hw.IRepository;
using FreeSql;


namespace Hw.Repository
{
    public abstract class HwRepository<TEntity> : BaseRepository<TEntity, int>, IHwRepository<TEntity> where TEntity : Hw.Model.BaseModel, new()
    {
        IFreeSql _fsql;
        public HwRepository(IFreeSql fsql) : base(fsql, null, null)
        {
            _fsql = fsql;
        }

    }
}
