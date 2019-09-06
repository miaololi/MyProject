using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyProject.Tools
{
    public class UtilConfigHelper
    {
        private static IConfiguration config;

        public static IConfiguration Configuration//加载配置文件
        {
            get
            {
                if (config != null)
                {
                    return config;
                }
                else
                {
                    config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
                    return config;
                }
            }
            set =>config = value;
        }
    }
}
