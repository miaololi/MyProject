using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using MyProject.Models;
using System.Net.Http;

namespace MyProject.Api.Controllers.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class DingHttpController : BaseController
    {
        /// <summary>
        /// 获取钉钉jsapi票据信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetDingTicket()
        {
            return DingHttpBll.GetDingTicket();
        }

        /// <summary>
        /// 获取钉钉用户信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetDingUser(string mobile)
        {
            Result result = new Result() { Code = 0 };
            try
            {
                result.Code = 1;
                result.Obj= DingHttpBll.GetDingUser(mobile);
            }
            catch (System.Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 发起钉钉审批
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result AddApproval()
        {
            return DingHttpBll.AddApproval();
        }
    }
}
