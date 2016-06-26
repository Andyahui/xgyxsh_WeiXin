using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGY_Model;

namespace XGY_Service.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly EFDbContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        #region CTOR
        public UnitOfWork(EFDbContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new EFDbContext();
        } 
        #endregion

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        /// <summary>
        /// 主要的方法--这个方法，为每一个继承自BaseEntity的实体，返回一个仓储【repository】对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Repository<T> Repository<T>()where T:BaseEntity
        {
            if (repositories==null)
            {
                 repositories=new Dictionary<string, object>();
            }
            var type = typeof (T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)),context);
                repositories.Add(type,repositoryInstance);
            }
            return (Repository<T>) repositories[type];
        }
    }
}
