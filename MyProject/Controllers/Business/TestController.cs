using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using MyProject.Models;
using MyProject.Tools;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetCoordinates(string address)
        {
            var result = new Result() { Code = 0 };
            try
            {
                string ak = "RXUiCCU31yZ6dEkXzMz1qdfLhxdyLjp3";
                string url = "http://api.map.baidu.com/geocoding/v3/";
                string HttpUrl = string.Format(@"{0}?address={1}&output=json&ak={2}"
                                               ,url, address , ak);
                result.Code = 1;
                result.Obj= HttpHelper.HttpGet(HttpUrl);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
