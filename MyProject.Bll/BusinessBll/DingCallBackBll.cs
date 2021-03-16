
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MyProject.Bll.BusinessBll
{
    public class DingCallBackBll
    {
        readonly static string AesKey = "RCkXuCQkeGBkCs9gCqcC29AVwDWJ4UzmHbO5Pb7XPzS";
        readonly static string Token = "lIDNjQRTUWyDVZR1JPKF5oU3";
        readonly static string OwnerKey = "dingranxamcp66vp3pme"; //以应用为维度推送的，OWNER_KEY为应用的AppKey,是以企业为维度推送的，OWNER_KEY为CorpId。

        public static Dictionary<string, string> CallBack(HttpRequest request,string signature, string timeStamp, string nonce, string encrypt)
        {
            // 1. 从http请求中获取加解密参数
            signature= request.Query["msg_signature"].Count>0? request.Query["msg_signature"].ToString() : signature;

            // 2. 使用加解密类型
            Tools.Helpers.DingTalkEncryptor ding = new Tools.Helpers.DingTalkEncryptor(Token, AesKey, OwnerKey);
            //msg_signature, $data->timeStamp, $data->nonce, $data->encrypt
            string text = ding.GetDecryptMsg(signature, timeStamp, nonce, encrypt);

            // "msg_signature":"c01beb7b06384cf416e04930aed794684aae98c1","encrypt":"","timeStamp":,"nonce":""
            //{"timeStamp":"1605695694141","msg_signature":"702c953056613f5c7568b79ed134a27bd2dcd8d0","encrypt":"","nonce":"WelUQl6bCqcBa2fMc6eI"}
            //text = ding.getDecryptMsg("f36f4ba5337d426c7d4bca0dbcb06b3ddc1388fc", "1605695694141", "WelUQl6bCqcBa2fM", "X1VSe9cTJUMZu60d3kyLYTrBq5578ZRJtteU94wG0Q4Uk6E/wQYeJRIC0/UFW5Wkya1Ihz9oXAdLlyC9TRaqsQ==");

            // 3. 反序列化回调事件json数据
            dynamic eventJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(text);
            String eventType = eventJson.EventType;

            // 4. 根据EventType分类处理
            if (eventType == "check_url")
            {
                // 测试回调url的正确性
            }
            else if (eventType == "user_add_org")
            {
                // 处理通讯录用户增加时间
            }
            else if (eventType == "bpms_instance_change")
            {
                CallBackApproval(eventJson);
            }
            else
            {
                // 添加其他已注册的
            }

            // 5. 返回success的加密数据
            Dictionary<string, string> msg = ding.GetEncryptedMap("success");
            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CallBackApproval(dynamic dy)
        {

            return "";
        }
    }
}
