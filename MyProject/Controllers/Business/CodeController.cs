using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using System.Net.Http;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class CodeController : BaseController
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns>返回验证码图片</returns>
        [HttpGet]
        public HttpResponseMessage GetCode(string guid)
        {
            return CodeBll.GetCode(guid);
        }
    }
}
