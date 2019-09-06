using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Tools
{
    public class DbHelper
    {
        public string dsn;
        public static DbHelper SqlDSN { get { return new DbHelper(); } }

        public DbHelper()
        {
            dsn = UtilConfigHelper.Configuration.GetConnectionString("DefaultConnection");
        }

        public DbHelper(string strDSN)
        {
            dsn = UtilConfigHelper.Configuration.GetConnectionString(strDSN);
        }

        #region 操作连接
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="comd"></param>
        private void OpenConn(ref SqlCommand comd)
        {
            if (comd.Connection.State == ConnectionState.Closed)
            {
                comd.Connection.Open();
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="comd"></param>
        private void CloseConn(ref SqlCommand comd)
        {
            if (comd.Connection.State == ConnectionState.Open)
            {
                comd.Connection.Close();
            }
            comd.Dispose();
        }
        #endregion
        #region 操作commd
        /// <summary>
        /// 根据存储过程名生成comd对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <returns></returns>
        public SqlCommand CreateComd(string spName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dsn);
                SqlCommand comd = conn.CreateCommand();
                comd.CommandText = spName;
                comd.CommandType = CommandType.StoredProcedure;
                return comd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据存储过程名和参数集生成comd对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        public SqlCommand CreateComd(string spName, DbParameters pars)
        {
            try
            {
                SqlCommand comd = CreateComd(spName);
                int len = pars.Len;
                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        comd.Parameters.Add(pars[i]);
                    }
                }
                return comd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据sql生成sqlcomd对象
        /// </summary>
        /// <param name="strSql">sql</param>
        /// <returns></returns>
        public SqlCommand CreateSqlComd(string strSql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dsn);
                SqlCommand comd = conn.CreateCommand();
                comd.CommandText = strSql;
                comd.CommandType = CommandType.Text;
                return comd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据sql和参数集生成sqlcomd对象
        /// </summary>
        /// <param name="strSql">sql</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        public SqlCommand CreateSqlComd(string strSql, DbParameters pars)
        {
            try
            {
                SqlCommand comd = CreateSqlComd(strSql);
                int len = pars.Len;
                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        comd.Parameters.Add(pars[i]);
                    }
                }
                return comd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region 操作sqlDataAdapter
        /// <summary>
        ///  根据存储过程名，生成SqlDataAdapter对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <returns></returns>
        public SqlDataAdapter CreateAdapter(string spName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dsn);
                SqlDataAdapter comdAdapter = new SqlDataAdapter(spName, conn);
                comdAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                return comdAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据存储过程名和参数，生成SqlDataAdapter对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        public SqlDataAdapter CreateAdapter(string spName, DbParameters pars)
        {
            try
            {
                SqlDataAdapter comdAdapter = CreateAdapter(spName);
                int len = pars.Len;
                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        comdAdapter.SelectCommand.Parameters.Add(pars[i]);
                    }
                }
                return comdAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据sql，生成SqlDataAdapter对象
        /// </summary>
        /// <param name="strsql">sql</param>
        /// <returns></returns>
        public SqlDataAdapter CreateSqlAdapter(string strsql)
        {
            try
            {
                SqlConnection conn = new SqlConnection(dsn);
                SqlDataAdapter comdAdapter = new SqlDataAdapter(strsql, conn);
                comdAdapter.SelectCommand.CommandType = CommandType.Text;
                return comdAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据sql和参数，生成SqlDataAdapter对象
        /// </summary>
        /// <param name="strsql">sql</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        public SqlDataAdapter CreateSqlAdapter(string strsql, DbParameters pars)
        {
            try
            {
                SqlDataAdapter comdAdapter = CreateAdapter(strsql);
                int len = pars.Len;
                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        comdAdapter.SelectCommand.Parameters.Add(pars[i]);
                    }
                }
                return comdAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region  创建 DataReader 对象
        /// <summary>
        /// 根据存储过程生成生SqlDataReader
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public SqlDataReader CreateDataReader(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return GetDataReader(comd);
        }
        /// <summary>
        /// 根据存储过程和参数生成SqlDataReader
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public SqlDataReader CreateDataReader(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return GetDataReader(comd);
        }
        /// <summary>
        /// 根据SQL语句生成SqlDataReader
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public SqlDataReader CreateSqlDataReader(string strSql)
        {
            SqlCommand comd = CreateSqlComd(strSql);
            return GetDataReader(comd);
        }
        /// <summary>
        /// 根据SQL语句和参数生成SqlDataReader
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public SqlDataReader CreateSqlDataReader(string strSql, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(strSql, pars);
            return GetDataReader(comd);
        }

        #region - GetDataReader()
        //获取DataReader
        private SqlDataReader GetDataReader(SqlCommand comd)
        {
            try
            {
                OpenConn(ref comd);
                return comd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
        #region 创建 DataTable 对象
        /// <summary>
        /// 根据存储过程创建 DataTable 
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string spName)
        {
            SqlDataAdapter adapter = CreateAdapter(spName);
            return GetDataTable(adapter);
        }
        /// <summary>
        /// 根据存储过程和参数创建 DataTable
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string spName, DbParameters pars)
        {
            SqlDataAdapter adapter = CreateAdapter(spName, pars);
            return GetDataTable(adapter);
        }
        /// <summary>
        /// 根据SQL语句,创建DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable CreateSqlDataTable(string strSql)
        {
            SqlDataAdapter adapter = CreateSqlAdapter(strSql);
            return GetDataTable(adapter);
        }
        /// <summary>
        /// 根据SQL语句和参数,创建DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public DataTable CreateSqlDataTable(string strSql, DbParameters pars)
        {
            SqlDataAdapter adapter = CreateSqlAdapter(strSql, pars);
            return GetDataTable(adapter);
        }

        #region  - GetDataTable()
        private DataTable GetDataTable(SqlDataAdapter adapter)
        {
            try
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (adapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    adapter.SelectCommand.Connection.Close();
                }
                adapter.Dispose();
            }
        }
        #endregion

        #endregion
        #region 创建 Scalar 对象
        /// <summary>
        /// 创建无参数的 Scalar 对象
        /// </summary>
        public object CreateScalar(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return GetScalar(comd);
        }
        /// <summary>
        /// 有参数的 Scalar 对象
        /// </summary>
        public object CreateScalar(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return GetScalar(comd);
        }
        /// <summary>
        /// 根据SQL语句，创建Scalar对象
        /// </summary>
        public object CreateSqlScalar(string strSql)
        {
            SqlCommand comd = CreateSqlComd(strSql);
            return GetScalar(comd);
        }
        /// <summary>
        /// 根据SQL语句和参数，创建Scalar对象
        /// </summary>
        public object CreateSqlScalar(string strSql, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(strSql, pars);
            return GetScalar(comd);
        }

        #region - GetScalar()
        private object GetScalar(SqlCommand comd)
        {
            try
            {
                OpenConn(ref comd);
                object o = comd.ExecuteScalar();
                CloseConn(ref comd);

                return o;
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
        #region ** 执行数据库操作 - ToExecute() **
        /// <summary>
        /// 执行数据库操作
        /// </summary>
        private int ToExecute(SqlCommand comd)
        {
            try
            {
                OpenConn(ref comd);
                int iOk = comd.ExecuteNonQuery();
                CloseConn(ref comd);
                return iOk;
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }

        private int ToExecuteInt(SqlCommand comd)
        {
            try
            {
                OpenConn(ref comd);
                int iOk = 0;
                int.TryParse(comd.ExecuteScalar().ToString(), out iOk);
                CloseConn(ref comd);
                return iOk;
            }
            catch (System.Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ** 仅执行，不返回输出参数 **
        /// <summary>
        /// 根据存储过程执行
        /// </summary>
        public int Execute(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return ToExecute(comd);
        }
        /// <summary>
        /// 根据存储过程和参数执行
        /// </summary>
        public int Execute(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return ToExecute(comd);
        }
        /// <summary> 
        /// 执行sql语句
        /// </summary> 
        public int ExecuteSql(string sql)
        {
            SqlCommand comd = CreateSqlComd(sql);
            return ToExecute(comd);
        }

        /// <summary> 
        /// 执行带参数的SQL语句
        /// </summary> 
        public int ExecuteSqlInt(string sql, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sql, pars);
            return ToExecuteInt(comd);
        }
        public int ExecuteSql(string sql, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sql, pars);
            return ToExecute(comd);
        }

        #endregion

        #region ** 执行并返回输出参数 **
        /// <summary>
        /// 执行并返回输出参数
        /// </summary>
        public string ExecuteOut(string spName, DbParameters pars, string outParamName)
        {
            SqlCommand comd = CreateComd(spName, pars);
            //comd.Parameters.Add(new SqlParameter(outParamName, SqlDbType.VarChar, 50));
            //comd.Parameters[outParamName].Direction = ParameterDirection.Output;

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters[outParamName].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行并返回输出参数：默认输出参数 @Result Varchar(50)
        /// </summary>
        public string ExecuteOut(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            comd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar, 50));
            comd.Parameters["@Result"].Direction = ParameterDirection.Output;

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters["@Result"].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ** 执行并返回输出参数 **
        /// <summary>
        /// 执行存储过程，并返回输出参数
        /// </summary>
        public string ExecuteReturn(string spName, DbParameters p, string retParam)
        {
            SqlCommand comd = CreateComd(spName, p);
            comd.Parameters.Add(new SqlParameter(retParam, SqlDbType.VarChar, 50));
            comd.Parameters[retParam].Direction = ParameterDirection.ReturnValue;

            //comd.Parameters.Add(new SqlParameter("ReturnValue",SqlDbType.Int,4, ParameterDirection.ReturnValue, false, 0, 0,String.Empty, DataRowVersion.Default, null));

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters[retParam].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        public string ExecuteReturn(string spName, DbParameters p)
        {
            SqlCommand comd = CreateComd(spName, p);
            comd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.VarChar, 50));
            comd.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;

            //comd.Parameters.Add(new SqlParameter("ReturnValue",SqlDbType.Int,4, ParameterDirection.ReturnValue, false, 0, 0,String.Empty, DataRowVersion.Default, null));

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters["ReturnValue"].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        /// <summary> 
        /// 执行Sql语句，并返回返回值
        /// </summary> 
        public string ExecuteSqlReturn(string sql, DbParameters pars, string retParam)
        {
            SqlCommand comd = CreateSqlComd(sql, pars);
            comd.Parameters.Add(new SqlParameter(retParam, SqlDbType.VarChar, 50));
            comd.Parameters[retParam].Direction = ParameterDirection.ReturnValue;

            //comd.Parameters.Add(new SqlParameter("ReturnValue",SqlDbType.Int,4, ParameterDirection.ReturnValue, false, 0, 0,String.Empty, DataRowVersion.Default, null));

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters[retParam].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 根据Sql语句执行
        /// </summary>
        public string ExecuteSqlReturn(string sql, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sql, pars);
            comd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.VarChar, 50));
            comd.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;

            //comd.Parameters.Add(new SqlParameter("ReturnValue",SqlDbType.Int,4, ParameterDirection.ReturnValue, false, 0, 0,String.Empty, DataRowVersion.Default, null));

            try
            {
                OpenConn(ref comd);
                comd.ExecuteNonQuery();
                object o = comd.Parameters["ReturnValue"].Value;
                CloseConn(ref comd);

                return (o == null) ? "" : o.ToString();
            }
            catch (Exception ex)
            {
                CloseConn(ref comd);
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
