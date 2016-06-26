using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XGY_Model.Entity;

namespace XGY_Model
{
    public class EFDbContext : DbContext
    {
        /// <summary>
        /// 数据上下文类
        /// </summary>
        public EFDbContext()
            : base("name=XGYWeixin")
        {
        }
        #region 实体类
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }

        #endregion
        /// <summary>
        /// 重写的基类----使用Code-First方式，配置实体类的
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
.Where(type => !String.IsNullOrEmpty(type.Namespace))
.Where(type => type.BaseType != null && type.BaseType.IsGenericType
    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
