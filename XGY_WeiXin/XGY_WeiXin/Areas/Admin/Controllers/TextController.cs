using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service.Repository;
using XGY_WeiXin.Areas.Admin.Models;
using XGY_WeiXin.WeiXinHelper;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    public class TextController : BaseController
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        /// <summary>
        /// 文本素材列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageResponseTextList model)
        {
            try
            {
                var entity = _unitOfWork.ResponseTextMessageRepository.Get();
                if (entity != null)
                {
                    model.Items = Mapper.Map<List<ResponseTextMessageView>>(entity);
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(CreateResponseTextMessageView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTime = DateTime.Now.ToLocalTime();
                    var entity = Mapper.Map<ResponseTextMessage>(model);
                    _unitOfWork.ResponseTextMessageRepository.Insert(entity);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    ErrorNotification(ex.Message);
                    //throw new Exception(ex.Message);
                }
                SuccessNotification("添加成功");
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Update(UpdateResponseTextMessageView model)
        {
            try
            {
                var entity = _unitOfWork.ResponseTextMessageRepository.GetById(model.Id);
                if (entity!=null)
                {
                    Mapper.Map(entity, model);
                    return View(model);
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Update")]
        public ActionResult UpdatePost(UpdateResponseTextMessageView model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<ResponseTextMessage>(model);
                _unitOfWork.ResponseTextMessageRepository.Update(entity);
                _unitOfWork.Save();
                SuccessNotification("修改成功");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                _unitOfWork.ResponseTextMessageRepository.Delete(id);
                _unitOfWork.Save();
                SuccessNotification("删除成功");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}