using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Text.Json;

namespace MyProject
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
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

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //#region 防跨域
            //services.AddCors(opt =>
            //{
            //    opt.AddDefaultPolicy(builder =>
            //    {
            //        builder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowAnyOrigin()
            //        .AllowCredentials();
            //    });

            //    string[] Origin = Configuration["customHeaders:Origin"].Split(",");
            //    string Headers = Configuration["customHeaders:Headers"];
            //    string Methods = Configuration["customHeaders:Methods"];
            //    opt.AddPolicy("default", builder =>
            //    {
            //        builder
            //        .WithOrigins(Origin)
            //        .WithHeaders(Headers)
            //        .WithMethods(Methods)
            //        .AllowCredentials();
            //    });
            //});
            //#endregion
            //var csredis = new CSRedis.CSRedisClient(Configuration["RedisConnection"]);
            ////初始化 RedisHelper
            //RedisHelper.Initialization(csredis);
            ////注册mvc分布式缓存
            //services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));

            //#region swagger
            ////注册Swagger生成器，定义一个和多个Swagger 文档
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "WebApi", Version = "1.0" });

            //    // 为 Swagger JSON and UI设置xml文档注释路径
            //    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
            //    var xmlPath = Path.Combine(basePath, "MyProjectApi.xml");
            //    c.IncludeXmlComments(xmlPath);

            //});
            //#endregion
            ////全局配置Json序列化处理
            //services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.WriteIndented = true;//格式化json字符串
            //    options.JsonSerializerOptions.AllowTrailingCommas = true;   //可以结尾有逗号                       
            //    options.JsonSerializerOptions.IgnoreNullValues = true;                             //可以有空值,转换json去除空值属性
            //    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;                        //忽略只读属性
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;                   //忽略大小写
            //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;    //命名方式是默认还是CamelCase
            //});

            services.AddControllers();
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors();//防跨域
            app.UseHttpsRedirection();//调用HTTPS重定向中间件
            app.UseRouting();
            app.UseAuthorization();

            #region swagger
            //启用中间件服务生成Swagger作为JSON终结点
            //app.UseSwagger();
            ////启用中间件服务对swagger-ui，指定Swagger JSON终结点
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
            //});
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
