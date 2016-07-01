﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.Repository;
using XGY_WeiXin.Areas.Admin.Models;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    /// <summary>
    /// 文章列表
    /// </summary>
    public class ArticleController : Controller
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork();
        public ActionResult Index(PageArticleList model)
        {
            try
            {
                var articleList = _unitOfWork.ArticleRepository.Get();
                if (articleList!=null)
                {
                    model.Items = Mapper.Map<List<ArticleView>>(articleList);
                    return View(model);                     
                }
                return View(model);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }


        [HttpGet]
        public ActionResult Create(CreateArticleView model)
        {
            model.ArticleCategory = _unitOfWork.ArticleCategoryRepository.Get()
                .Where(x => x.Id != Guid.Empty)
                .Select(x => new SelectListItem
                {
                    Text=x.Name,
                    Value=x.Id.ToString()
                }).ToList();
            model.Users = _unitOfWork.UserRepository.Get()
                .Where(x => x.Id != Guid.Empty)
                .Select(x => new SelectListItem
                {
                    Text =x.LoginName,
                    Value=x.Id.ToString()
                }).ToList();
            return View(model);
        }
        [HttpPost,ActionName("Create")]
        public ActionResult CreatePost(CreateArticleView model)
        {
            if (ModelState.IsValid)
            {
                if (model!=null)
                {
                    try
                    {                        
                        var article = new Article();
                        Mapper.Map(model,article);
                        _unitOfWork.ArticleRepository.Insert(article);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {                        
                        throw new Exception(ex.Message);
                    }                    
                }
                return View();
            }
            return View("Create", model);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost,ActionName("Update")]
        public ActionResult UpdatePost()
        {
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                _unitOfWork.ArticleRepository.Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
    }
}