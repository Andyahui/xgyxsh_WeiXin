using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using XGY_Model.Entity;
using XGY_Service;
using XGY_Service.Repository;
using XGY_WeiXin.Areas.Admin.Models;
using XGY_WeiXin.WeiXinHelper;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class UserController : BaseController
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork();
        public ActionResult Index(UserListView model)
        {
            try
            {
                var entity = _unitOfWork.UserRepository.Get();
                model.Items = Mapper.Map<List<UserView>>(entity);
                return View(model);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }                        
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(CreateUserView model)
        {
            if(ModelState.IsValid) 
            {
                try
                {                    
                    model.HeadPhoto = "/User/Images/" + model.HeadPhoto;
                    var entity = Mapper.Map<User>(model);
                    _unitOfWork.UserRepository.Insert(entity);
                    _unitOfWork.Save();
                    SuccessNotification("添加成功");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
            ErrorNotification("请填写表单！");
            return View(model);
        }
        [HttpGet]
        public ActionResult Update(UpdateUserView model)
        {
            try
            {
                var entity=_unitOfWork.UserRepository.GetById(model.Id);
                if (entity!=null)
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
        public ActionResult UpdatePost(UpdateUserView model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var entity=Mapper.Map<User>(model);
                    _unitOfWork.UserRepository.Update(entity);
                    _unitOfWork.Save();
                    SuccessNotification("更新成功");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {                    
                    throw new Exception(ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Delete(List<Guid> ids )
        {
            if(ids.Count>0)
            {
                foreach (var id in ids)
                {
                    _unitOfWork.UserRepository.Delete(id);
                    SuccessNotification("删除成功");
                    _unitOfWork.Save();
                }
            }
            return RedirectToAction("Index");
        }
    }
}