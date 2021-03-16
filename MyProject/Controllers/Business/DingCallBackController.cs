using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Bll.BusinessBll;
using System.Collections.Generic;

namespace MyProject.Api.Controllers.Business
{
    /// <summary>
    /// 接受钉钉回调接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class DingCallBackController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public DingCallBackController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 钉钉回调
        /// </summary>
        /// <param name="json"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [HttpPost]
        public Dictionary<string, string> CallBack([FromBody] BodyJson json, [FromQuery] string signature = "", [FromQuery] string timestamp = "", [FromQuery] string nonce = "")
        {
            HttpRequest request = _httpContextAccessor.HttpContext.Request;
            return DingCallBackBll.CallBack(request, signature, timestamp, nonce, json.encrypt);
        }
    }

    /// <summary>
    /// json
    /// </summary>
    public class BodyJson
    {
        /// <summary>
        /// aa
        /// </summary>
        public string encrypt { get; set; }
    }
}
