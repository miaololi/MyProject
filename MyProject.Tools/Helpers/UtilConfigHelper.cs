using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyProject.Tools
{
    public static class UtilConfigHelper
    {
        private static readonly IConfigurationRoot Configuration;
        static UtilConfigHelper()
        {
            var builder = new ConfigurationBuilder();//创建config的builder
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");//设置配置文件所在的路径加载配置文件信息
            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string key)
        {
            var res = Configuration.GetValue<T>(key);
            return (T)Convert.ChangeType(res, typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectionStr"></param>
        /// <returns></returns>
        public static string GetSection(string SectionStr)
        {
            return Configuration.GetSection(SectionStr).Value;
        }
    }
}