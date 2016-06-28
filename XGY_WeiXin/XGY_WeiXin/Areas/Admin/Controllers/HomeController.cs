using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XGY_Model.Entity;
using XGY_Service.Repository;

namespace XGY_WeiXin.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork=new UnitOfWork();

        public ActionResult Index()
        {
            var listArticles = unitOfWork.ArticleRepository.Get();
            return View(listArticles);
        }
        [HttpGet]
        public ActionResult CreateArticle()
        {
            return View();
        }
        [HttpPost, ActionName("CreateArticle")]
        public ActionResult CreateArticlePost()
        {
            return View();
        }
    }
}