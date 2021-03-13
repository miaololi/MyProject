using MyProject.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Bll.BusinessBll
{
    public class ChuanYunHttpBll
    {
        public static Result GetWuPingLingYong()
        {
            Result result = new Result() { Code = 0 };
            var filterDto = new FilterDto
            {
                FromRowNum = 0,
                RequireCount = "false",
                ReturnItems = new List<string> {
                        "F0000008",//单据日期
                        "F0000001",//领用人
                        "Fcc9cd18599a34526b765597c7f6832cd"//资产领用明细
                    },
                SortByCollection = new List<string>(),
                ToRowNum = 1,
                Matcher = new MatcherDto
                {
                    Type = "And",
                    Matchers = new List<MatcherDto>()
                    {
                        //new MatcherDto{
                        // Name = "F0000008",
                        // Type = "Item",
                        // Operator = "2",
                        // Value = DateTime.Now.ToString("yyyy-MM-dd")
                        //}
                    }
                }
            };

            var dto = new ChuanYunInDto
            {
                ActionName = "LoadBizObjects",
                SchemaCode = "wv7s9ol1uck5pojagnxcf4zj1",//资产发放单
                Filter = Newtonsoft.Json.JsonConvert.SerializeObject(filterDto)
            };
            //string jsonDto = Newtonsoft.Json.JsonConvert.SerializeObject(dto);//postman调试用
            var res = GetChuanYuanDate(dto);
            if (res.Code == 1)
            {
                if (res.Obj.Successful == true && res.Obj.ReturnData != null)
                {
                    result.Code = 1;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(res.Obj.ReturnData.BizObjectArray);
                    var bizDto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<wv7s9ol1uck5pojagnxcf4zj1>>(json);
                    result.Obj = bizDto;
                }
                else
                {
                    res.Message = res.Obj.ErrorMessage;
                }
            }
            else
            {
                result.Message = res.Message;
                result.Obj = res.Obj;
            }
            return result;
        }

        /// <summary>
        /// 获取氚云数据
        /// </summary>
        /// <returns></returns>
        public static Result<ChuanYunListOutDto> GetChuanYuanDate(ChuanYunInDto josnBody)
        {
            Result<ChuanYunListOutDto> result = new Result<ChuanYunListOutDto>() { Code = 0 };
            try
            {
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("EngineCode", "u9nwcrxkprhtkjb26st6o9up6");
                request.AddHeader("EngineSecret", "4rscMifAu7tS4izbUkhXtv/6jkZw3PLZgRw1tiBbY4gTEq+8XDOvYg==");
                request.AddJsonBody(josnBody);
                RestClient client = new RestClient("https://www.h3yun.com/OpenApi/Invoke");
                IRestResponse restResponse = client.Execute(request);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var outDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ChuanYunListOutDto>(restResponse.Content);
                    result.Code = 1;
                    result.Obj = outDto;
                    return result;
                }
                else
                {
                    result.Message = restResponse.ErrorMessage;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
