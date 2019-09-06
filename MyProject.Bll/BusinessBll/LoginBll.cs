using MyProject.Models;
using MyProject.Tools;
using System;
using System.Data;

namespace MyProject.Bll
{
    public class LoginBll : BaseBll
    {
        public static Result Login(LoginDto dto)
        {
            Result result = new Result() { Code = 0 };
            try
            {
                if (dto == null)
                {
                    result.Message = "传参有误";
                    return result;
                }
                if (!string.IsNullOrEmpty(dto.UserName) && !string.IsNullOrEmpty(dto.UserPwd))
                {
                    result.Message = "用户名或密码不可为空";
                    return result;
                }
                string sqlStr = @"SELECT top 1 * FROM dbo.e_Emp
                    WHERE FUserName=@FUserName AND FPwd=@FPwd";
                var pars = new DbParameters();
                pars.Add("FUserName", dto.UserName);
                pars.Add("FPwd", ExHelper.MD5Hash(dto.UserPwd.Trim()).ToLower());
                DataTable dt = DbHelper.SqlDSN.CreateSqlDataTable(sqlStr, pars);

                if (dt == null || dt.Rows.Count <= 0)
                {
                    result.Message = "登录失败";
                    return result;
                }
                LoginDataDto dataDto = dt.ToDtDto<LoginDataDto>();
                //dataDto.UserMenuJson = MenuBll.GetMenuListForZtree(dataDto.UserId);
                var token = "";//TokenHelper.CreatToken(dataDto);
                var red = RedisHelper.Get<LoginDataDto>(token);

                if (red != null)
                {
                    result.Code = 1;
                    result.Message = "登录成功";
                    result.Obj = token;
                    return result;
                }

                if (RedisHelper.Set(token, dataDto, 43200))
                {
                    result.Code = 1;
                    result.Message = "登录成功";
                    result.Obj = token;
                }
                else
                {
                    result.Message = "缓存服务器异常";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
