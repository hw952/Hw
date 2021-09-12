using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FreeSql;

namespace Hw.IRepository
{
    public interface IHwRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : class
    {


    }

}
