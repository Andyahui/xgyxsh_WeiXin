using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XGY_WeiXin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //这里的路由有问题。
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "XGY_WeiXin.Controllers" }
            );

            routes.MapRoute(
                name: "admin",
                url: "{areas}/{controller}/{action}/{id}",
                defaults: new { areas = "Admin", action = "Index", controller = "Home", id = UrlParameter.Optional },
                namespaces: new string[] { "XGY_WeiXin.Areas.Controllers" }
                
                );
         }
    }
}