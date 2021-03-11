using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using MyProject.Models;
using MyProject.Tools;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
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
    
        #region 基础常用功能
        /// <summary>
        /// 获取钉钉token
        /// </summary>
        /// <returns></returns>
        public static string GetDingToken()
        {
            string token = RedisHelper.Get("DingToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/gettoken");
            OapiGettokenRequest req = new OapiGettokenRequest
            {
                Appkey = appkey,
                Appsecret = appsecret
            };
            req.SetHttpMethod("GET");
            OapiGettokenResponse rsp = client.Execute(req);
            if (rsp != null && rsp.Errcode == 0)
            {
                token = rsp.AccessToken;
                RedisHelper.Set("DingToken", token, expireSeconds: 7200);
                return token;
            }
            else {
                return rsp.Errmsg;
            }
        }
        /// <summary>
        /// 获取订单编码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetProcessCodeByName(string name)
        {
            string namecode=System.Web.HttpUtility.UrlEncode(name, Encoding.UTF8);
            string processCode = RedisHelper.Get(namecode);
            if (!string.IsNullOrWhiteSpace(processCode))
            {
                return processCode;
            }
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/topapi/process/get_by_name");
            OapiProcessGetByNameRequest req = new OapiProcessGetByNameRequest
            {
                Name = name
            };
            req.SetHttpMethod("POST");
            OapiProcessGetByNameResponse rsp = client.Execute(req, accessToken);
            if (rsp != null && rsp.Errcode == 0)
            {
                processCode = rsp.ProcessCode;
                RedisHelper.Set(namecode, processCode, expireSeconds: 36000);
                return processCode;
            }
            else
            {
                return rsp.Errmsg;
            }
        }

        /// <summary>
        /// 根据code获取客户ID
        /// </summary>
        /// <returns></returns>
        public static Result GetDingUserIDByMobile(string Mobile)
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/user/get_by_mobile");
            OapiUserGetByMobileRequest req = new OapiUserGetByMobileRequest
            {
                Mobile = Mobile
            };
            req.SetHttpMethod("GET");
            OapiUserGetByMobileResponse rsp = client.Execute(req, accessToken);
            if (rsp != null && rsp.Errcode == 0)
            {
                result.StrOjb = rsp.Userid;
            }
            else if (rsp != null && rsp.Errcode != 0)
            {
                result.Code = 0;
                result.Obj = rsp;
                result.Message = rsp.Errmsg;
            }
            else
            {
                result.Code = 0;
                result.Message = "获取钉钉客户ID有误";
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDingSsoToken()
        {
            string token = "";
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/sso/gettoken");
            OapiSsoGettokenRequest req = new OapiSsoGettokenRequest
            {
                Corpid = "dinga88c39dc20539c19f5bf40eda33b7ba0",
                Corpsecret = "a7KJpM71WbwteWrp8ntKcI4UArN7E4bVeoiSigHQ-HI5zJpHWhU-G9PI35tsYAOx",
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
        /// 根据code获取客户信息
        /// </summary>
        /// <returns></returns>
        public static OapiSnsGetuserinfoBycodeResponse GetDingUserInfoByCode()
        {
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/sns/getuserinfo_bycode");
            OapiSnsGetuserinfoBycodeRequest req = new OapiSnsGetuserinfoBycodeRequest();
            OapiSnsGetuserinfoBycodeResponse rsp = client.Execute(req, accessToken);
            return rsp;
        }

        /// <summary>
        /// 获取钉钉用户信息
        /// </summary>
        /// <param name="userId">钉钉用户ID</param>
        /// <returns></returns>
        public static Result GetDingUser(string Mobile)
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            var UserIDRsp = GetDingUserIDByMobile(Mobile);
            if (UserIDRsp.Code == 1)
            {
                IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/user/get");
                OapiUserGetRequest req = new OapiUserGetRequest
                {
                    Userid = UserIDRsp.StrOjb
                };
                req.SetHttpMethod("GET");
                OapiUserGetResponse rsp = client.Execute(req, accessToken);
                if (rsp != null && rsp.Errcode == 0)
                {
                    result.Obj = rsp;
                }
                else if (rsp != null && rsp.Errcode != 0)
                {
                    result.Code = 0;
                    result.Obj = rsp;
                    result.Message = rsp.Errmsg;
                }
                else
                {
                    result.Code = 0;
                    result.Message = "获取钉钉用户信息有误";
                }
                return result;
            }
            else
            {
                return UserIDRsp;
            }
        }

        /// <summary>
        /// 发起审批
        /// </summary>
        /// <returns></returns>
        public static Result AddApproval()
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            var resultUserId = GetDingUserIDByMobile("13968414187");
            if (resultUserId.Code == 0)
            {
                return resultUserId;
            }
            string curUser = resultUserId.StrOjb;
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
                    Value = "2021-03-06"
                },
                new FormComponentValueVoDomain
                {
                    Name = "附件",
                    Value = "[\"https://a.vpimg3.com/upload/merchandise/pdcvis/2021/01/20/162/c6180b6a-a849-4d44-88d5-bd1aca3958e8.jpg\"]"
                }
            };
            OapiProcessinstanceCreateRequest req = new OapiProcessinstanceCreateRequest
            {
                //AgentId = 1123887388, 
                ProcessCode = "PROC-CAF86280-39FE-4FC1-8177-588D2B01D8A2", // 自定义审批单，编辑那个审批单的时候在Url找！
                OriginatorUserId = curUser,
                DeptId = -1,
                Approvers = "",//审批人列表
                //CcList = curUser, //抄送人
                //CcPosition = "START",//抄送时间
                FormComponentValues_ = formComponentValues
            };
            req.SetHttpMethod("Post");
            OapiProcessinstanceCreateResponse rsp = client.Execute(req, accessToken);
            result.Obj = rsp;
            return result;
        }

        /// <summary>
        /// 根据code获取客户ID
        /// </summary>
        /// <returns></returns>
        public static Result GetFailCallBackList()
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/call_back/get_call_back_failed_result");
            OapiCallbackFailrecordListRequest req = new OapiCallbackFailrecordListRequest
            {
            };
            req.SetHttpMethod("GET");
            OapiCallbackFailrecordListResponse rsp = client.Execute(req, accessToken);
            if (rsp != null && rsp.Errcode == 0)
            {
                result.Obj = rsp;
            }
            else if (rsp != null && rsp.Errcode != 0)
            {
                result.Code = 0;
                result.Obj = rsp;
                result.Message = rsp.Errmsg;
            }
            else
            {
                result.Code = 0;
                result.Message = "获取钉钉回调失败列表有误";
            }
            return result;
        }

        /// <summary>
        /// 发送机器人推送
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result SendRobotMsg(string msg)
        {
            Result result = new Result() { Code=1};
            string robotToken = "da95d6bcd4e389e3bce4eb518eb08dc23a6f315ea5987eb247c21989f2d00376";
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/robot/send");
            OapiRobotSendRequest req = new OapiRobotSendRequest()
            {
                Msgtype = "text",
                Text_ = new OapiRobotSendRequest.TextDomain
                {
                    Content = "推送:"+ msg
                },
                At_ = new OapiRobotSendRequest.AtDomain
                {
                    AtMobiles =new List<string> { 
                        "13968414187" 
                    },
                    IsAtAll = false
                }
            };
            DateTime timeNow = DateTime.Now;
            long timestamp = timeNow.ToUnixTimestampByMilliseconds();
            String secret = "SEC9070624c833e627790c5aaad181a96c050cbc5e43c1653a09b65259f1a67f091";
            req.AddOtherParameter("sign", ExHelper.AddSign(secret, timestamp));
            req.SetHttpMethod("POST");
            OapiRobotSendResponse rsp = client.Execute(req, robotToken, timeNow);
            if (rsp != null && rsp.Errcode == 0)
            {
                result.Obj = rsp;
                result.Message = "发送成功";
            }
            else if (rsp != null && rsp.Errcode != 0)
            {
                result.Code = 0;
                result.Obj = rsp;
                result.Message = rsp.Errmsg;
            }
            else
            {
                result.Code = 0;
                result.Message = "发送机器人推送失败";
            }
            return result;
        }

        /// <summary>
        /// 获取审批列表ID
        /// </summary>
        /// <param name="ProcessCode"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Result GetApprovalListID(string name, int index,DateTime time)
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/topapi/processinstance/listids");
            OapiProcessinstanceListidsRequest req = new OapiProcessinstanceListidsRequest
            {
                Cursor = index,
                Size = 10,
                StartTime = time.ToUnixTimestampByMilliseconds(),
                EndTime =DateTime.Now.ToUnixTimestampByMilliseconds(),
                ProcessCode= GetProcessCodeByName(name),
                UseridList=""
            };
            req.SetHttpMethod("POST");
            OapiProcessinstanceListidsResponse rsp = client.Execute(req, accessToken);
            if (rsp != null && rsp.Errcode == 0)
            {
                result.Obj = rsp;
            }
            else if (rsp != null && rsp.Errcode != 0)
            {
                result.Code = 0;
                result.Obj = rsp;
                result.Message = rsp.Errmsg;
            }
            else
            {
                result.Code = 0;
                result.Message = "获取钉钉审批列表ID失败";
            }
            return result;
        }

        /// <summary>
        /// 获取审批列表ID
        /// </summary>
        /// <param name="ProcessCode"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Result GetApprovalList(string name, int index, DateTime time)
        {
            Result result = new Result() { Code = 1 };
            string accessToken = GetDingToken();
            IDingTalkClient client = new DefaultDingTalkClient(dingUrl + "/topapi/processinstance/list");
            OapiProcessinstanceListRequest req = new OapiProcessinstanceListRequest
            {
                Cursor = index,
                Size = 10,
                StartTime = time.ToUnixTimestampByMilliseconds(),
                EndTime = DateTime.Now.ToUnixTimestampByMilliseconds(),
                ProcessCode = GetProcessCodeByName(name),
                UseridList = ""
            };
            req.SetHttpMethod("POST");
            OapiProcessinstanceListResponse rsp = client.Execute(req, accessToken);
            if (rsp != null && rsp.Errcode == 0)
            {
                result.Obj = rsp;
            }
            else if (rsp != null && rsp.Errcode != 0)
            {
                result.Code = 0;
                result.Obj = rsp;
                result.Message = rsp.Errmsg;
            }
            else
            {
                result.Code = 0;
                result.Message = "获取钉钉审批列表失败";
            }
            return result;
        }
    }
}
