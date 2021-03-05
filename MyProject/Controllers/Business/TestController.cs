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
