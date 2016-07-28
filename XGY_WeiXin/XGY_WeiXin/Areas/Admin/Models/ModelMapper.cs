using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.AutoMapper;

namespace XGY_WeiXin.Areas.Admin.Models
{
    public class ModelMapper : IStartupTask
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
            #region Article

            Mapper.CreateMap<Article, ArticleView>();
            Mapper.CreateMap<CreateArticleView, ArticleCategory>();
            Mapper.CreateMap<CreateArticleView, Article>();

            Mapper.CreateMap<Article, UpdateArticleView>();
            Mapper.CreateMap<ArticleCategory, UpdateArticleView>();
            #endregion

            #region ArticleCategory
            Mapper.CreateMap<ArticleCategory, ArticleCategoryView>();

            Mapper.CreateMap<CreateArticleCategory, ArticleCategory>();

            Mapper.CreateMap<ArticleCategory, UpdateArticleCategoryView>();
            Mapper.CreateMap<UpdateArticleCategoryView, ArticleCategory>();
            #endregion

            #region User

            Mapper.CreateMap<User, UserView>();
            Mapper.CreateMap<CreateUserView, User>();
            Mapper.CreateMap<User, UpdateUserView>();
            Mapper.CreateMap<UpdateUserView, User>();

            #endregion

            #region Account

            Mapper.CreateMap<User,UpdateUserAccountView>();

            #endregion

            #region ResponseTextMessage

            Mapper.CreateMap<ResponseTextMessage,ResponseTextMessageView>();
            Mapper.CreateMap<CreateResponseTextMessageView,ResponseTextMessage>();
            Mapper.CreateMap<ResponseTextMessage, UpdateResponseTextMessageView>();
            Mapper.CreateMap<UpdateResponseTextMessageView,ResponseTextMessage>();

            #endregion
        }
    }
}