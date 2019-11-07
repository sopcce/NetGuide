using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DIAndPipe.Services.Declare;
using DIAndPipe.Services.Implement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;


namespace DIAndPipe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

         #region 内置容器

        // This method gets called by the runtime. Use this method to add services to the container.
        // 此方法用来向容器中添加服务
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            #region 内部的服务

            services.AddMvc().AddControllersAsServices().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddDbContext<EFContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EFCore"));
            });   

            #endregion
            
            #region swagger服务注入
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Swagger用法",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "API",
                        Email = string.Empty,
                        Url = "https://www.sopcce.com"
                    },
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = "https://www.sopcce.com"
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "DIAndPipe.xml");
                c.IncludeXmlComments(xmlPath);
            });
            
            #endregion

            
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            containerBuilder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
            
            var container = containerBuilder.Build();
            
            return new AutofacServiceProvider(container);
        }

        #endregion

        public void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //使用Run来终止管道
            
            //app.Run(async context => { await context.Response.WriteAsync("Hello world"); });
            
            //使用Map来针对某一路径做出响应

            //app.Map("/map1", HandleMapTest1);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //如果没有，服务器会通过302重定向来把用户引导到对应的https服务，有了它，浏览器会把当前地址
                //加入预置的HSTS列表，并且下次访问的时候，会自动使用https访问。
                app.UseHsts();
            }

            //添加跨域支持
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseAuthentication(); //启用验证

            app.UseHttpsRedirection();//Https重定向
            app.UseMvc();//使用MVC框架
            
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}