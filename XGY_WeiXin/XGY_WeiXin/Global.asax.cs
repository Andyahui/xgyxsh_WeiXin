using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using XGY_Service.AutoMapper;

namespace XGY_WeiXin
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ExecuteStartupTasks();
        }
        /// <summary>
        /// AutoMapper的配置文件
        /// </summary>
        private void ExecuteStartupTasks()
        {
            List<IStartupTask> startupTasks = new List<IStartupTask>();    //申明一个List<>泛型集合
            Assembly asm;                                                                            //Assembly：是一个程序集
            string codeBase = HttpRuntime.BinDirectory;                          //得到Bin的路径
            UriBuilder url=new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(url.Path);
            string bin = Path.GetDirectoryName(path);
            string[] assemblies = Directory.GetFiles(bin, "*.dll");            //加载所有的dll文件，
            foreach (string file in assemblies)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        asm = Assembly.LoadFrom(file);
                        //寻找实现定义接口的类 <这里才是重头戏>利用linq来实现
                        var query = from t in asm.GetTypes()
                                    where t.IsClass && t.GetInterface(typeof(IStartupTask).FullName) != null
                                    select t;

                        // 添加泛型集合到启动任务列表
                        foreach (Type type in query)
                        {
                            startupTasks.Add((IStartupTask)Activator.CreateInstance(type));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            foreach (IStartupTask task in startupTasks)
            {
                task.Execute();      //这个方法中是那些配置文件，把那些全部实现初始化
            }
        }
    }
}