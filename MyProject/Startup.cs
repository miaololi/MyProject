using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = serverSecret,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"]
                };
            });

            services.AddMvc().AddWebApiConventions();

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

            services.AddCors();//防跨域

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connection));
        }

        /// 运行时调用此方法。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.WithOrigins("*"));//防跨域

            app.UseAuthentication();//使用验证

            if (env.IsDevelopment())//判断是否是环境变量
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();// 强制实施 HTTPS 在 ASP.NET Core
            }

            app.UseHttpsRedirection();//调用HTTPS重定向中间件

            app.UseMvc();
        }
    }
}
