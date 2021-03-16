using DingTalk.Api.Request;
using Microsoft.AspNetCore.Mvc;
using MyProject.Bll;
using MyProject.Models;
using System;
using System.Collections.Generic;
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
                result.Obj = DingHttpBll.GetDingUser(mobile);
            }
            catch (System.Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取钉钉Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetDingToken()
        {
            Result result = new Result() { Code = 0 };
            try
            {
                result.Code = 1;
                result.Obj = DingHttpBll.GetDingToken();
            }
            catch (System.Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取钉钉订单编号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetProcessCodeByName(string name)
        {
            Result result = new Result() { Code = 0 };
            try
            {
                result.Code = 1;
                result.Obj = DingHttpBll.GetProcessCodeByName(name);
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

        /// <summary>
        /// 获取失败回调列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetFailCallBackList()
        {
            return DingHttpBll.GetFailCallBackList();
        }

        /// <summary>
        /// 发送机器人推送
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        [HttpGet]
        public Result SendRobotMsg(string msg)
        {
            return DingHttpBll.SendRobotMsg(msg);
        }

        /// <summary>
        /// 获取审批列表ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetApprovalListID(string name, int index, DateTime time)
        {
            return DingHttpBll.GetApprovalListID(name, index, time);
        }

        /// <summary>
        /// 获取审批列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetApprovalList(string name, int index, DateTime time)
        {
            return DingHttpBll.GetApprovalList(name, index, time);
        }

        /// <summary>
        /// 获取钉钉部门id列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        [HttpGet]
        public Result<List<long>> GetDeptIDList(int deptID)
        {
            return DingHttpBll.GetDeptIDList(deptID);
        }

        /// <summary>
        /// 获取钉钉部门列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetDeptList(int deptID)
        {
            return DingHttpBll.GetDeptList(deptID);
        }

        /// <summary>
        /// 获取所有部门ID 
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetAllDeptIDList(int deptID)
        {
            Result result = new Result()
            {
                Code = 1,
                Obj = DingHttpBll.GetAllDeptIDList(deptID)
            };
            return result;
        }
        
        /// <summary>
        /// 获取员工ID根据部门
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetUserIDListByDeptID(int deptID)
        {
            Result result = new Result()
            {
                Code = 1,
                Obj = DingHttpBll.GetUserIDListByDeptID(deptID)
            };
            return result;
        }

        /// <summary>
        /// 获取钉钉员工id列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result GetAllUserIDList()
        {
            return DingHttpBll.GetAllUserIDList();
        }

        /// <summary>
        /// 获取列值
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public Result GetColumnVal(OapiAttendanceGetcolumnvalRequest req)
        {
            Result result = new Result()
            {
                Code = 1,
                Obj = DingHttpBll.GetColumnVal(req)
            };
            return result;
        }
    }
}
