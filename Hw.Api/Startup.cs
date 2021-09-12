using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hw.Extensions;
using AutoMapper.Mappers;


namespace Hw.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hw.Api", Version = "v1" });
            });

            //Freesql
            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=db1.db")
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                .Build(); //请务必定义成 Singleton 单例模式

            fsql.Aop.CurdAfter += (s, e) =>
{

    Console.WriteLine(e.Sql);
    CurdAfterLog.Current.Value?.Sb.AppendLine($"{Thread.CurrentThread.ManagedThreadId}: {e.EntityType.FullName} {e.ElapsedMilliseconds}ms, {e.Sql}");
};
            services.AddSingleton<IFreeSql>(fsql);
            services.AddScoped<CurdAfterLog>();
            //跨域
            services.AddCors(options =>
                      {
                          options.AddPolicy("AnyPolicy",
                              builder => builder
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .WithExposedHeaders("X-Pagination"));
                      });
            //添加对AutoMapper支持
            services.AddAutoMapper(typeof(CustomAutoMapperConfigs));
        }




        //autofac 新增
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 直接用Autofac注册我们自定义的
            builder.RegisterModule(new CustomAutofacModule(builder));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hw.Api v1"));
            }

            //autofac 新增
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseCors("AnyPolicy");  //允许跨域访问

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
