using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    public class AccountController : BaseController
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork();

        #region Account
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(UserAccountView model)
        {
            return View(model);
        }

        [HttpPost, ActionName("Index")]
        public ActionResult IndexPost(UserAccountView model)
        {
            if (ModelState.IsValid)
            {
                var entity = _unitOfWork.UserRepository.Table().FirstOrDefault(x => x.LoginName == model.LoginName);
                if (entity != null)
                {
                    //entity.LoginPwd = AccountService.ConvertPwd(entity.LoginPwd);
                    if (entity.LoginPwd == model.LoginPwd)
                    {
                        SuccessNotification("登录成功");
                        HttpCookie cookie_Account=new HttpCookie("Account",entity.LoginName);
                        Response.AppendCookie(cookie_Account);
                        return RedirectToAction("Index", "Home");
                    }
                    ErrorNotification("密码错误!");
                    return View(model);
                }
                ErrorNotification("登录名错误!");
                return View(model);
            }
            ErrorNotification("表单不能为空，请认真填写！");
            return View(model);
        } 
        #endregion

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>  
        public ActionResult Editor(UpdateUserAccountView model)
        {
            try
            {
                string account = Request.Cookies["Account"].Value;
                var entity = _unitOfWork.UserRepository.Table().FirstOrDefault(x => x.LoginName == account);
                Mapper.Map(entity, model);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
            return View(model);
        }
    }
}