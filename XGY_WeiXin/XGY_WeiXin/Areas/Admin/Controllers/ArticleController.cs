using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.Repository;
using XGY_WeiXin.Areas.Admin.Models;
using XGY_WeiXin.WeiXinHelper;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    /// <summary>
    /// 文章列表
    /// </summary>
    public class ArticleController : BaseController
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
                    try
                    {                                            
                        var entity = Mapper.Map<Article>(model);
                        _unitOfWork.ArticleRepository.Insert(entity);
                        _unitOfWork.Save();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {                        
                        throw new Exception(ex.Message);
                    }                                                    
            }
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Update(UpdateArticleView model)
        {
            try
            {
                //model.ArticleCategory = _unitOfWork.ArticleCategoryRepository.Get()
                //.Where(x => x.Id != Guid.Empty)
                //.Select(x => new SelectListItem()
                //{
                //    Text = x.Name,
                //    Value = x.Id.ToString()
                //}).ToList();
                //model.Users = _unitOfWork.UserRepository.Get()
                //    .Where(x => x.Id != Guid.Empty)
                //    .Select(x => new SelectListItem()
                //    {
                //        Text = x.LoginName,
                //        Value = x.Id.ToString()
                //    }).ToList();
                var entity=_unitOfWork.ArticleRepository.GetById(model.Id);
                if(entity!=null)
                {                    
                    Mapper.Map(entity, model);
                    return View(model);                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }
        [HttpPost,ActionName("Update")]
        public ActionResult UpdatePost()
        {
            return View();
        }

        public ActionResult Delete(Guid ids)
        {
            try
            {
                _unitOfWork.ArticleRepository.Delete(ids);
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