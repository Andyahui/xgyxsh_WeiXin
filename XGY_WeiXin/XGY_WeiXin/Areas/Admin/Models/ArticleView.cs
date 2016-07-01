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
    #region Article
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
            //this.ArticleCategory = new List<SelectListItem>();
            //this.Users = new List<SelectListItem>();
        }
        public DateTime CreateTime { get; set; }
        [DisplayName("文章标题"),Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        [DisplayName("文章描述"), Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
        [DisplayName("文章图片"), Required(ErrorMessage = "图片不能为空")]
        public string PicUrl { get; set; }
        [DisplayName("文章内容"), Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }
        [DisplayName("创建人"), Required(ErrorMessage = "创建人不能为空")]
        public Guid UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        [DisplayName("文章分类"), Required(ErrorMessage = "分类不能为空")]
        public Guid ArticleCategoryId { get; set; }
        public IEnumerable<SelectListItem> ArticleCategory { get; set; }
    }

    public class UpdateArticleView
    {
        public UpdateArticleView()
        {
            this.CreateTime = DateTime.Now;
            //this.ArticleCategory = new List<SelectListItem>();
            //this.Users = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        [DisplayName("文章标题"), Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        [DisplayName("文章描述"), Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
        [DisplayName("文章图片"), Required(ErrorMessage = "图片不能为空")]
        public string PicUrl { get; set; }
        [DisplayName("文章内容"), Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }
        [DisplayName("创建人"), Required(ErrorMessage = "创建人不能为空")]
        public Guid UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        [DisplayName("文章分类"), Required(ErrorMessage = "分类不能为空")]
        public Guid ArticleCategoryId { get; set; }
        public IEnumerable<SelectListItem> ArticleCategory { get; set; }
    }

    #endregion

    #region ArticleCatagory

    public class ArticleCategoryList
    {
        public ICollection<ArticleCategoryView> Items { get; set; }
    }

    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategoryView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateArticleCategory
    {

        [DisplayName("文章分类"),Required(ErrorMessage = "文章名称不能为空")]
        public string Name { get; set; }
    }

    public class UpdateArticleCategoryView
    {
        public Guid Id { get; set; }

        [DisplayName("文章分类"),Required(ErrorMessage = "文章名称不能为空")]
        public string Name { get; set; }
    }

    #endregion
}