using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGY_Model;
using XGY_Model.Entity;

namespace XGY_Service.Repository
{
    /// <summary>
    /// 单例模式：用来协助多个仓储，使得创建单一的数据上下文，用来共享。
    /// -----每个仓储属性返回一个repository实例，所有这些实例都会共享同样的context;
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly EFDbContext context;
        private bool disposed;
        

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

        #region 仓储的写入

        #region UserRepository

        private Repository<User> _userRepository;
        public Repository<User> UserRepository
        {
            get
            {
                try
                {
                    if (_userRepository == null)
                    {
                        _userRepository = new Repository<User>(context);
                    }
                    return _userRepository;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        } 

        #endregion       

        #region ArticleRepository

        private Repository<Article> _articleRepository;

        public Repository<Article> ArticleRepository
        {
            get
            {
                try
                {
                    if (_articleRepository==null)
                    {
                         _articleRepository=new Repository<Article>(context);
                    }
                    return _articleRepository;
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
        }

        #endregion

        #region ArticleCategoryRepository

        private Repository<ArticleCategory> _articleCategoryRepository;

        public Repository<ArticleCategory> ArticleCategoryRepository
        {
            get
            {
                if (_articleCategoryRepository==null)
                {
                     _articleCategoryRepository=new Repository<ArticleCategory>(context);
                }
                return _articleCategoryRepository;
            }
        }

        #endregion

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

        #region 需要在控制器的构造函数中来写。

        //private Dictionary<string, object> repositories;
        // 主要的方法--这个方法，为每一个继承自BaseEntity的实体，返回一个仓储【repository】对象

        //public Repository<T> Repository<T>() where T : BaseEntity
        //{
        //    if (repositories == null)
        //    {
        //        repositories = new Dictionary<string, object>();
        //    }
        //    var type = typeof(T).Name;
        //    if (!repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(Repository<>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
        //        repositories.Add(type, repositoryInstance);
        //    }
        //    return (Repository<T>)repositories[type];
        //} 
        #endregion
    }
}
