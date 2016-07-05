using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XGY_Model.Entity;
using XGY_Service.Repository;
using XGY_WeiXin.WeiXinHelper;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    /// <summary>
    /// 主页
    /// </summary>
    public class HomeController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}