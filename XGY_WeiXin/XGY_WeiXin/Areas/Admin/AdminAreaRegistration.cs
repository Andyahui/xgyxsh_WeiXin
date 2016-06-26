using System.Web.Mvc;

namespace XGY_WeiXin.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //配置后端的路由
                //namespaces: new[] { "XGY_WeiXin.Areas.Admin.Controllers" }                
            );
        }
    }
}