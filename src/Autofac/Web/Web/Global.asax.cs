using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Sop.Framework.Repositories;
using Sop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{

    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        //private static readonly ILog logger = LogManager.GetLogger<MvcApplication>();
        //private static readonly bool customErrors = bool.Parse(ConfigurationManager.AppSettings["CustomErrors"]);

        /// <summary>
        /// 应用程序启动时执行的事件
        /// </summary>
        protected void Application_Start()
        {
            

    
            //初始化DI容器
            InitializeDIContainer();

            //初始化MVC环境
            //InitializeMVC();

            //初始化应用程序，加载基础数据
            //InitializeApplication(assemblies);


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 应用程序终止时执行的事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Application_End(Object source, EventArgs e)
        {
            //记录日志
            //logger.Info("站点已停止");
        }

        /// <summary>
        /// 自定义404、500的信息显示页面
        /// </summary>
        protected void Application_EndRequest()
        {
            //if (Response.StatusCode == 404)
            //{
            //    if (customErrors)
            //    {
            //        Response.Redirect(SiteUrls.Instance().Error404());
            //    }
            //}
            //else if (Response.StatusCode == 500)
            //{
            //    if (customErrors)
            //    {
            //        Response.Redirect(SiteUrls.Instance().SystemMessage(null, null));
            //    }
            //}

            //DIContainer.Resolve<SessionManager>().CloseSession();

        }

        /// <summary>
        /// 处理系统错误日志
        /// </summary>
        protected void Application_OnError()
        {
            //将异常记录到日志
            var exception = Server.GetLastError();
            //logger.Error(Request.Url.ToString(), exception);
        }

        /// <summary>
        /// 去除Response Header中的Server 信息
        /// http://stackoverflow.com/questions/1178831/remove-server-response-header-iis7
        /// </summary>
        protected void Application_PostReleaseRequestState()
        {
            Response.Headers.Remove("Server");
        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        private void InitializeDIContainer()
        {
            var containerBuilder = new ContainerBuilder();

            //获取当前业务相关的程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(n => n.FullName.StartsWith("Sop")).ToArray();

            //批量注册程序集中的实现
            containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Service"));
            containerBuilder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().InstancePerLifetimeScope();


            //注册Repository
            containerBuilder.RegisterGeneric(typeof(PocoRepository<>)).As(typeof(IRepository<>))
                .SingleInstance().PropertiesAutowired();

           

            //containerBuilder.RegisterAssemblyTypes(assemblies).Where(t => typeof(IStudentService).IsAssignableFrom(t))
            //   .AsImplementedInterfaces().SingleInstance()
            //   .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            //containerBuilder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).AsImplementedInterfaces();
            //containerBuilder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();



            ////注册UserAuthentication，每次请求都是一个新的实例
            //containerBuilder.Register(t => new UserAuthentication()).PropertiesAutowired().InstancePerRequest();

            //注入MVC
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);//注册所有的Controller
            containerBuilder.RegisterControllers(assemblies).PropertiesAutowired();
            containerBuilder.RegisterSource(new ViewRegistrationSource());
            containerBuilder.RegisterModelBinders(assemblies);
            containerBuilder.RegisterModelBinderProvider();
            containerBuilder.RegisterFilterProvider();
            containerBuilder.RegisterModule(new AutofacWebTypesModule());

            //注入WebApi
            //containerBuilder.RegisterApiControllers(assemblies).PropertiesAutowired();
            //containerBuilder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            //注入SignalR
            //containerBuilder.RegisterHubs(assemblies);

            IContainer container = containerBuilder.Build();

            //设置MVC的解析器
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            //设置WebApi的解析器
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //设置SignalR的解析器
            //GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            DIContainer.RegisterContainer(container);
        }

        /// <summary>
        /// 初始化MVC环境
        /// </summary>
        private void InitializeMVC()
        {
            ////自定义模型绑定，增加安全过滤特性
            //ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

            ////增加对Cookie的模型绑定
            ////ValueProviderFactories.Factories.Add(new CookieValueProviderFactory());

            ////使MVC支持应用模块的视图引擎
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new SimpleRazorViewEngine());

            ////注册全局过滤器
            //GlobalFilters.Filters.Add(new SecurityFilter());
            //GlobalFilters.Filters.Add(new OutputResultFilter());

            ////注册WebApi路由
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            ////注册MVC路由
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            ////禁止Response的header信息包含X-AspNetMvc-Version
            MvcHandler.DisableMvcResponseHeader = true;
        }

        /// <summary>
        /// 初始化应用程序，加载基础数据
        /// </summary>
        private void InitializeApplication(Assembly[] assemblies)
        {
            ////启动应用
            //var applicationStarters = DIContainer.Resolve<IEnumerable<IApplicationStarter>>();
            //foreach (var applicationStarter in applicationStarters)
            //{
            //    applicationStarter.Start(DIContainer.GetContainer(), assemblies);
            //}

            ////加载Widget
            //var widgetManager = DIContainer.Resolve<WidgetManager>();
            //widgetManager.Initialize(assemblies);

            ////注册静态文件绑定
            //var bundleRegisters = DIContainer.Resolve<IEnumerable<IBundleRegister>>();
            //foreach (var bundleRegister in bundleRegisters)
            //{
            //    bundleRegister.Register(BundleTable.Bundles);
            //}

            ////注册事件处理程序
            //var eventModules = DIContainer.Resolve<IEnumerable<IEventModule>>();
            //foreach (var eventModule in eventModules)
            //{
            //    eventModule.RegisterEventHandler();
            //}

            ////初始化全文检索的索引
            //DIContainer.Resolve<SearchConnector>().InitIndex();

        }
    }
}
