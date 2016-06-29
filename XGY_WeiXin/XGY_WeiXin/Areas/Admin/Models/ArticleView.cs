using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XGY_Model.Entity;

namespace XGY_WeiXin.Areas.Admin.Models
{
    public class PageArticleList
    {        
        public ICollection<ArticleView> Items { get; set; }
    }

    public class ArticleView
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public ArticleCategoryView ArticleCategory { get; set; }
    }

    public class CreateArticleView
    {
        public CreateArticleView()
        {
            this.CreateTime = DateTime.Now;
            this.ArticleCategory = new List<SelectListItem>();
            this.Users = new List<SelectListItem>();
        }
        public DateTime CreateTime { get; set; }
        [DisplayName("文章标题")]
        public string Title { get; set; }
        [DisplayName("文章描述")]
        public string Description { get; set; }
        [DisplayName("文章图片")]
        public string PicUrl { get; set; }
        [DisplayName("文章内容")]
        public string Content { get; set; }
        [DisplayName("创建人")]
        public Guid UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }        
        [DisplayName("文章分类")]
        public Guid ArticleCategoryId { get; set; }
        public IEnumerable<SelectListItem> ArticleCategory { get; set; }
    }

    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategoryView
    {
        public string Name { get; set; }
    }
}