using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Service.Repository
{
    /// <summary>
    /// 仓储的底层接口
    /// </summary>
     interface IRepositories<TEntity>where TEntity:class
    {
        IEnumerable<TEntity> Get();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Update(TEntity entity);
        IQueryable<TEntity> Table();
    }
}
