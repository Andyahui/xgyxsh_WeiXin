using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using XGY_Model;

namespace XGY_Service.Repository
{
    /// <summary>
    /// 泛型类。T 代表类型；
    /// 作用：解决代码的冗余，(生成多个仓储形成的)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity>:IRepositories<TEntity> where TEntity : class 
    {
        internal EFDbContext context;
        internal IDbSet<TEntity> dbSet;

        private string errorMessage = string.Empty;

        public Repository(EFDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        //条件选择的时候使用这个
        public IQueryable<TEntity> Table()
        {
            return dbSet;
        }

        //得到全部的数据
        public IEnumerable<TEntity> Get()
        {
            return dbSet.ToList();
        }
        //得到值
        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }
        //插入数据
        public void Insert(TEntity entity)
        {
            try
            {
                if (entity==null)
                {
                     throw new ArgumentNullException("entity");
                }
                dbSet.Add(entity);
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbEntityValidationResult(ex);
                throw new Exception(errorMessage,ex);
            }
        }
        //更新数据
        public void Update(TEntity entity)
        {
            try
            {
                if (entity==null)
                {
                    throw new ArgumentNullException("entity");
                }
                dbSet.Attach(entity);
                context.Entry(entity).State=EntityState.Modified;
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage =DbEntityValidationResult(ex);
                throw new Exception(errorMessage,ex);
            }
        }
        //删除数据
        public void Delete(TEntity entity)
        {
            try
            {
                if (entity==null)
                {
                    throw new ArgumentNullException("entity");
                }
                if (context.Entry(entity).State==EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbEntityValidationResult(ex);
                throw new Exception(errorMessage,ex);
            }
        }
        //删除数据
        public void Delete(object id)
        {
            var entity = dbSet.Find(id);
            Delete(entity);
        }
        /// <summary>
        /// 错误机制的封装
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public  string DbEntityValidationResult(DbEntityValidationException ex)
        {
            foreach (var errorItems in ex.EntityValidationErrors)
            {
                foreach (var errorInfo in errorItems.ValidationErrors)
                {
                    errorMessage += string.Format("属性名：{0}，错误消息：{1}", errorInfo.PropertyName, errorInfo.ErrorMessage) + Environment.NewLine;
                }
            }
            return errorMessage;
        }
    }
}
