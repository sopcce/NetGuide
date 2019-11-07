using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using AutofactMVC.Models;
using StackExchange.Profiling;

namespace AutofactMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MiniProfilerEF.Initialize();
            //Autofac初始化过程
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);//注册所有的Controller
            //开发环境下，使用Stub类
            //builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly).Where(
            //    t => t.Name.EndsWith("Repository") && t.Name.StartsWith("Stub")).AsImplementedInterfaces();
            //发布环境下，使用真实的数据访问层
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).AsImplementedInterfaces();

            //注册builder, 实现one context per request
            builder.RegisterType<SchoolContext>().As<DbContext>().InstancePerHttpRequest();
            builder.RegisterType<SchoolContext>().InstancePerHttpRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //其它的初始化过程
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ModelBinders.Binders.Add(typeof(Student), new StudentModelBinder());
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            } 
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}