using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyProject.Dal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Text;

namespace MyProject
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 运行时调用此方法。使用此方法向容器中添加服务。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 防跨域
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
                });

                string[] Origin = Configuration["customHeaders:Origin"].Split(",");
                string Headers = Configuration["customHeaders:Headers"];
                string Methods = Configuration["customHeaders:Methods"];
                opt.AddPolicy("default", builder =>
                {
                    builder
                    .WithOrigins(Origin)
                    .WithHeaders(Headers)
                    .WithMethods(Methods)
                    .AllowCredentials();
                });
            });
            #endregion

            //全局配置Json序列化处理
            services.AddMvc().AddJsonOptions(options =>
            {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddMvc().AddWebApiConventions();

            //var connection = Configuration.GetConnectionString("Def.Writer");
            //services.AddDbContext<DbContext>(options => options.UseSqlServer(connection));

            //var mySqlconnection = Configuration.GetConnectionString("Def.MySql");
            //services.AddDbContext<MySqlDbContext>(options => options.UseMySQL(mySqlconnection));

            var csredis = new CSRedis.CSRedisClient(Configuration["RedisConnection"]);
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));

            #region swagger
            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebApi", Version = "1.0" });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "MyProjectApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        /// 运行时调用此方法。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())//判断是否是环境变量
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();// 强制实施 HTTPS 在 ASP.NET Core
            }

            app.UseCors();//防跨域

            app.UseHttpsRedirection();//调用HTTPS重定向中间件

            #region swagger
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
            });
            #endregion

            app.UseMvc();
        }
    }
}
