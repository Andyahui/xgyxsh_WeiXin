using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XGY_Model.Entity;

namespace XGY_WeiXin.Areas.Admin.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
    }
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory
    {
        public string Name { get; set; }
    }
}