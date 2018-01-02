using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XGY_Service.Repository;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost,ActionName("Create")]
        public ActionResult CreatePost()
        {
            return View();
        }
    }
}