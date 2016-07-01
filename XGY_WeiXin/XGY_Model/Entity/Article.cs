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
    /// 文章
    /// </summary>
    public class Article:BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 文章描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public virtual string PicUrl { get; set; }
        //文章分类
        public virtual Guid ArticleCategoryId { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
        //作者分类
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory:BaseEntity
    {
        /// <summary>
        /// 分类名字
        /// </summary>
        public virtual string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }

    #region Fluent API
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.ToTable("Article");
            this.HasKey(x => x.Id);
            this.Property(x => x.Title).IsRequired().HasMaxLength(128);
            this.Property(x => x.PicUrl).HasMaxLength(128);
            this.Property(x => x.Description).HasMaxLength(256);
            this.Property(x => x.Content).IsMaxLength();
            //配置一对多
            this.HasRequired(x => x.ArticleCategory).WithMany(x=>x.Articles).HasForeignKey(x => x.ArticleCategoryId);
            this.HasRequired(x => x.User).WithMany(x => x.Articles).HasForeignKey(x => x.UserId);
        }
    }

    public class ArticleCategoryMap : EntityTypeConfiguration<ArticleCategory>
    {
        public ArticleCategoryMap()
        {
            this.ToTable("ArticleCategory");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(128);
            this.HasMany(x => x.Articles);
        }
    }
    #endregion
}
