using MyProject.Tools;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace MyProject.Dal
{
    public class LoginDal : BaseDal
    {
        public static DataTable GetEmpDT(string userName, string UserPwd)
        {
            string sqlStr = @"SELECT top 1 * FROM dbo.e_Emp
                    WHERE FUserName=@FUserName AND FPwd=@FPwd";
            var pars = new DbParameters();
            pars.Add("FUserName", userName);
            pars.Add("FPwd", UserPwd);
            DataTable dt = DbHelper.SqlObj.CreateSqlDataTable(sqlStr, pars);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDt()
        {
            string connStr = "Data Source=.;Initial Catalog=DataMip;Integrated Security=True;MultipleActiveResultSets=True";
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                var result = conn.Execute("Insert into Users values (@UserName, @Email, @Address)",
                                       new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });
            }
            return null;
        }
    }
}
