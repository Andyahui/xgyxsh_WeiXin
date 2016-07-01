using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.Repository;
using XGY_WeiXin.Areas.Admin.Models;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    public class ArticleCategoryController : Controller
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork();
        public ActionResult Index(ArticleCategoryList model)
        {
            try
            {
                var entity=_unitOfWork.ArticleCategoryRepository.Get();                
                if (entity!=null)
                {
                    model.Items = Mapper.Map<List<ArticleCategoryView>>(entity);
                    return View(model);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(CreateArticleCategory model)
        {            
            return View(model);
        }
        [HttpPost,ActionName("Create")]
        public ActionResult CreatePost(CreateArticleCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new ArticleCategory();
                    Mapper.Map(model,entity);
                    _unitOfWork.ArticleCategoryRepository.Insert(entity);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
            return View();
        }

        public ActionResult Update(UpdateArticleCategoryView model)
        {
                try
                {
                    var entity=_unitOfWork.ArticleCategoryRepository.GetById(model.Id);
                    Mapper.Map(entity,model);
                    return View(model);
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
        [HttpPost,ActionName("Update")]
        public ActionResult UpdatePost(UpdateArticleCategoryView model)
        {
           if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<ArticleCategory>(model);
                    _unitOfWork.ArticleCategoryRepository.Update(entity);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
            return View();
        }

        public ActionResult Delete(List<Guid> ids)
        {
            if (ids.Count > 0)
            {
                try
                {
                    foreach (Guid id in ids)
                    {
                        _unitOfWork.ArticleCategoryRepository.Delete(id);
                        _unitOfWork.Save();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}