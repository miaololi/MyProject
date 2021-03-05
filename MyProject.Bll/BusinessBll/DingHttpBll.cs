using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using MyProject.Models;
using MyProject.Models.Dtos.Ding;
using Newtonsoft.Json;

namespace MyProject.Bll
{
    /// <summary>
    /// 请求钉钉
    /// </summary>
    public class DingHttpBll
    {
        static readonly string appkey = "dingranxamcp66vp3pme";
        static readonly string appsecret = "8dehm5JwhuSflESEw-n0OWgZtHqfC3OowINLGgD41nWRoDlsTwdOQopoQ9Kp5b1j";
        static readonly string dingUrl = "https://oapi.dingtalk.com";
        static string token = "";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDingToken()
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            IDingTalkClient client = new DefaultDingTalkClient(dingUrl+"/gettoken");
            OapiGettokenRequest req = new OapiGettokenRequest
            {
                Appkey = appkey,
                Appsecret = appsecret
            };
            req.SetHttpMethod("GET");
            OapiGettokenResponse rsp = client.Execute(req);
            if (rsp.Body != null)
            {
                var dto = JsonConvert.DeserializeObject<DingTokenOutDto>(rsp.Body);
                token = dto.access_token;
                return token;
            }
            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Result GetDingTicket()
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/get_jsapi_ticket");
            OapiUserGetRequest req = new OapiUserGetRequest
            {
            };
            req.SetHttpMethod("GET");
            OapiUserGetResponse rsp = client.Execute(req, accessToken);
            if (rsp.Body != null)
            {
                //var dto = JsonConvert.DeserializeObject<DingTokenOutDto>(rsp.Body);
                result.Obj = rsp.Body;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Result GetDingUser(string userId)
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl+"/user/get");
            OapiUserGetRequest req = new OapiUserGetRequest
            {
                Userid = userId
            };
            req.SetHttpMethod("GET");
            OapiUserGetResponse rsp = client.Execute(req, accessToken);
            if (rsp.Body != null)
            {
                var dto = JsonConvert.DeserializeObject<DingTokenOutDto>(rsp.Body);
                result.Obj= dto.access_token;
            }
            return result;
        }
    }
}
