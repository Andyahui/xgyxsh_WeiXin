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
        private Repository<Article> articleRepository; 
        public HomeController()
        {
            //通过工作单元来初始化仓储
            articleRepository = unitOfWork.Repository<Article>();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            //var id = Guid.Parse("058ddc1e-233d-47ba-a74b-490b13821916");
            //var article = articleRepository.GetById(id);
            List<Article> listArticles = articleRepository.Table.ToList();
            return View(listArticles);
        }
        [HttpGet]
        public ActionResult CreateArticle()
        {
            return View();
        }

        public ActionResult CreateArticlePost()
        {
            return View();
        }
    }
}