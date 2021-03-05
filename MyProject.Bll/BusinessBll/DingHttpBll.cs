using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using MyProject.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DingTalk.Api.Request.OapiProcessinstanceCreateRequest;

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
        /// 获取钉钉token
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
                token = rsp.AccessToken;
                return token;
            }
            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDingSsoToken()
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/sso/gettoken");
            OapiSsoGettokenRequest req = new OapiSsoGettokenRequest
            {
                Corpid = appkey,
                Corpsecret = appsecret,
            };
            req.SetHttpMethod("GET");
            OapiSsoGettokenResponse rsp = client.Execute(req);
            if (rsp.Body != null)
            {
                token = rsp.AccessToken;
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
            OapiGetJsapiTicketRequest req = new OapiGetJsapiTicketRequest
            {
            };
            req.SetHttpMethod("GET");
            OapiGetJsapiTicketResponse rsp = client.Execute(req, accessToken);
            if (rsp.Body != null)
            {
                result.Obj = rsp.Body;
            }
            return result;
        }

        /// <summary>
        /// 获取钉钉用户信息
        /// </summary>
        /// <param name="userId">钉钉用户ID</param>
        /// <returns></returns>
        public static OapiUserGetResponse GetDingUser(string userId)
        {
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl+"/user/get");
            OapiUserGetRequest req = new OapiUserGetRequest
            {
                Userid = userId
            };
            req.SetHttpMethod("GET");
            OapiUserGetResponse rsp = client.Execute(req, accessToken);
            return rsp;
        }

        /// <summary>
        /// 发起审批
        /// </summary>
        /// <returns></returns>
        public static Result AddApproval()
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            OapiUserGetResponse curUser = GetDingUser("1");
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/topapi/processinstance/create");

            // 用于整个表单组件的
            List<FormComponentValueVoDomain> formComponentValues = new List<FormComponentValueVoDomain>()
            {
                new FormComponentValueVoDomain
                {
                    Name = "客户",
                    Value = "张三"
                },
                new FormComponentValueVoDomain
                {
                    Name = "结果",
                    Value = "同意"
                },
                new FormComponentValueVoDomain
                {
                    Name = "原因",
                    Value = "可以"
                }
                ,
                new FormComponentValueVoDomain
                {
                    Name = "日期",
                    Value = "2021-3-5 16:06:49"
                },
                new FormComponentValueVoDomain
                {
                    Name = "附件",
                    Value = ""
                }
            };
            OapiProcessinstanceCreateRequest req = new OapiProcessinstanceCreateRequest
            {
                AgentId = 1123887388,  //微应用的AgentId，你申请的那个
                ProcessCode = "PROC-201910E5-64DF-4CA3-A036-CC75E664490A", // 自定义审批单，编辑那个审批单的时候在Url找！
                OriginatorUserId = curUser.Userid,
                DeptId = -1,
                Approvers = "111019246426142261",
                CcList = curUser.Userid,
                CcPosition = "START",
                FormComponentValues_= formComponentValues
            };

            req.SetHttpMethod("Post");
            OapiProcessinstanceCreateResponse rsp = client.Execute(req, accessToken);
            result.Obj = rsp;
            return result;
        }
    }
}
