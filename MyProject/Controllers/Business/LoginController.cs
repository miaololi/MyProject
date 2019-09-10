using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using MyProject.Models;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Login([FromBody] LoginDto dto)
        {
            return LoginBll.Login(dto);
        }
    }
}
