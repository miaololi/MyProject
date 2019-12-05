using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : BaseController
    {
        /// <summary>
        /// 测试http
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result TestHttp()
        {
            return TestBll.TestHttp();
        }

        /// <summary>
        /// 获取测试信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetTestInfo()
        {
            return TestBll.GetTestInfo();
        }

        /// <summary>
        /// 添加测试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddTestInfo(int id,string name)
        {
            return TestBll.AddTestInfo( id,  name);
        }
    }
}
