using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.AutoMapper;

namespace XGY_WeiXin.Areas.Admin.Models
{
    public class ModelMapper:IStartupTask
    {        
        /// <summary>
        /// 利用泛型，看实际传入的是什么，从而转换什么。
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public void Create<T1, T2>()
        {
            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }
        /// <summary>
        /// 具体的转换。
        /// </summary>
        public void Execute()
        {
            Mapper.CreateMap<Article,ArticleView>();
            Mapper.CreateMap<ArticleCategory,ArticleCategoryView>();
            Mapper.CreateMap<ArticleCategoryView, ArticleCategory>();
            Mapper.CreateMap<CreateArticleView, Article>();
            Mapper.CreateMap<Article,CreateArticleView>();
        }
    }
}