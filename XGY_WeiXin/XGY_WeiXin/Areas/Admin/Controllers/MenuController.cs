using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    /// <summary>
    /// 菜单管理页面Controller
    /// </summary>
    /*
     思路：
    *   这里的方法请求全部是放到了json中，返回的也是Json数据，前台页面的点击事件，发送时间，请求时间也是在js页面中，
    *   再从js文件中发送请求到controller中。
    *      感觉json好强大呀。
    */
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            //GetMenuResult result = new GetMenuResult();
            //初始化，为5行3列；
            for (int i = 0; i < 3; i++)
            {
                var subButton = new SubButton();
                for (int j = 0; j < 5; j++)
                {
                    var singleButton = new SingleClickButton();
                    subButton.sub_button.Add(singleButton);
                }
            }
            try
            {
                var appid = "";
                ViewBag.AppId = appid;
            }
            catch (Exception)
            {
                ViewBag.AppId = "";
            }

            return View();
        }
        /// <summary>
        /// 得到接口调用凭证---Token;，参数可在开发者配置环境中找到；
        /// </summary>
        /// <param name="appId">用户唯一凭证id</param>
        /// <param name="appSecret">用户的密钥</param>
        /// <returns></returns>
        public ActionResult GetToken(string appId, string appSecret)
        {
            try
            {
                if (!AccessTokenContainer.CheckRegistered(appId))
                {
                    AccessTokenContainer.Register(appId, appSecret);
                }
                var result = AccessTokenContainer.GetAccessToken(appId);
                //也可以直接一步到位：
                //var result = AccessTokenContainer.TryGetToken(appId, appSecret);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //TODO:为简化代码，这里不处理异常（如Token过期）
                return Json(new { error = "执行过程发生错误！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateMenu(string token, GetMenuResultFull resultFull)
        {
            try
            {
                ButtonGroupBase dd=new ButtonGroup();
                //重新整理按钮信息                                
                var bg = CommonApi.GetMenuFromJsonResult(resultFull,dd).menu;
                var result = CommonApi.CreateMenu(token, bg);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json);
            }
        }
        /// <summary>
        /// 利用Token，在SDK中发送请求得到菜单
        /// </summary>
        /// <param name="token">唯一凭证</param>
        /// 这里的请求时来自js文件的。返回的路径也是js'文件，在哪里进行处理。
        /// <returns></returns>
        public ActionResult GetMenu(string token)
        {
            var result = CommonApi.GetMenu(token);
            if (result == null)
            {
                return Json(new { error = "菜单不存在或验证失败！" },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult DeleteMenu(string token)
        {
            try
            {
                var result = CommonApi.DeleteMenu(token);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }
    }
}