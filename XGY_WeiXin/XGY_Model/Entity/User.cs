using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    /// <summary>
    /// 用户类----主要是保存管理员的一些信息
    /// </summary>
    public  class User:BaseEntity
    {
        public User()
        {
            this.Articles =new Collection<Article>();
        }
        /// <summary>
        /// 用户名(真实姓名)
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string LoginPwd { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public virtual string HeadPhoto { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
    /// <summary>
    /// Fluent API
    /// </summary>
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(t=>t.Id);
            this.Property(x => x.Ip).HasMaxLength(128);
            this.Property(x => x.HeadPhoto).HasMaxLength(128);
            this.Property(x => x.LoginName).HasMaxLength(128);
            this.Property(x => x.LoginPwd).HasMaxLength(128);
            this.Property(x => x.UserName).HasMaxLength(128);
            this.HasMany(x => x.Articles);
        }
    }
}
