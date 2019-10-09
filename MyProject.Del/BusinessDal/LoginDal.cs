using MyProject.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyProject.Dal
{
    public class LoginDal:BaseDal
    {
        public static DataTable GetEmpDT(string userName,string UserPwd)
        {
            string sqlStr = @"SELECT top 1 * FROM dbo.e_Emp
                    WHERE FUserName=@FUserName AND FPwd=@FPwd";
            var pars = new DbParameters();
            pars.Add("FUserName", userName);
            pars.Add("FPwd", UserPwd);
            DataTable dt = DbHelper.SqlObj.CreateSqlDataTable(sqlStr, pars);
            return dt;
        }
    }
}
