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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetTestInfo()
        {
            return TestBll.GetTestInfo();
        }
    }
}
