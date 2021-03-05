using System;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Tools
{
    public class DbHelper
    {
        public string connStr;

        public static DbHelper SqlObj
        {
            get
            {
                return new DbHelper();
            }
        }

        public DbHelper()
        {
            //connStr = UtilConfigHelper.Configuration.GetConnectionString("Def.Writer");
        }

        public DbHelper(string connName)
        {
            //connStr = UtilConfigHelper.Configuration.GetConnectionString(connName);
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
        /// 执行存储过程，返回comd对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <returns></returns>
        private SqlCommand CreateComd(string spName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
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
        /// 执行存储过程，返回comd对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        private SqlCommand CreateComd(string spName, DbParameters pars)
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
        /// 执行SQL语句，返回comd对象
        /// </summary>
        /// <param name="sqlStr">sqlStr</param>
        /// <returns></returns>
        private SqlCommand CreateSqlComd(string sqlStr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand comd = conn.CreateCommand();
                comd.CommandText = sqlStr;
                comd.CommandType = CommandType.Text;
                return comd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回comd对象
        /// </summary>
        /// <param name="sqlStr">sqlStr</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        private SqlCommand CreateSqlComd(string sqlStr, DbParameters pars)
        {
            try
            {
                SqlCommand comd = CreateSqlComd(sqlStr);
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
        ///  执行存储过程，返回SqlDataAdapter对象
        /// </summary>
        /// <param name="spName">存储过程名</param>
        /// <returns></returns>
        public SqlDataAdapter CreateAdapter(string spName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
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
        /// 执行存储过程，返回SqlDataAdapter对象
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
        /// 执行SQL语句，返回SqlDataAdapter对象
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public SqlDataAdapter CreateSqlAdapter(string sqlStr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                SqlDataAdapter comdAdapter = new SqlDataAdapter(sqlStr, conn);
                comdAdapter.SelectCommand.CommandType = CommandType.Text;
                return comdAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回SqlDataAdapter对象
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="pars">参数集</param>
        /// <returns></returns>
        public SqlDataAdapter CreateSqlAdapter(string sqlStr, DbParameters pars)
        {
            try
            {
                SqlDataAdapter comdAdapter = CreateSqlAdapter(sqlStr);
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

        #region  操作SqlDataReader
        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public SqlDataReader CreateDataReader(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return GetDataReader(comd);
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public SqlDataReader CreateDataReader(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return GetDataReader(comd);
        }

        /// <summary>
        /// 执行SQL语句，返回SqlDataReader对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public SqlDataReader CreateSqlDataReader(string sqlStr)
        {
            SqlCommand comd = CreateSqlComd(sqlStr);
            return GetDataReader(comd);
        }

        /// <summary>
        /// 执行SQL语句，返回SqlDataReader对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public SqlDataReader CreateSqlDataReader(string sqlStr, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
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

        #region 操作Dt对象
        /// <summary>
        /// 执行存储过程，返回dt对象
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string spName)
        {
            SqlDataAdapter adapter = CreateAdapter(spName);
            return GetDataTable(adapter);
        }

        /// <summary>
        /// 执行存储过程，返回dt对象
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
        /// 执行SQL语句,返回dt对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public DataTable CreateSqlDataTable(string sqlStr)
        {
            SqlDataAdapter adapter = CreateSqlAdapter(sqlStr);
            return GetDataTable(adapter);
        }

        /// <summary>
        /// 执行SQL语句,返回dt对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public DataTable CreateSqlDataTable(string sqlStr, DbParameters pars)
        {
            SqlDataAdapter adapter = CreateSqlAdapter(sqlStr, pars);
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
        /// 执行存储过程,返回scalar对象
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public object CreateScalar(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return GetScalar(comd);
        }

        /// <summary>
        /// 执行存储过程,返回scalar对象
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object CreateScalar(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return GetScalar(comd);
        }

        /// <summary>
        /// 执行SQL语句，返回Scalar对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public object CreateSqlScalar(string sqlStr)
        {
            SqlCommand comd = CreateSqlComd(sqlStr);
            return GetScalar(comd);
        }

        /// <summary>
        /// 执行SQL语句，返回Scalar对象
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object CreateSqlScalar(string sqlStr, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
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

        #region 执行cmd命令
        /// <summary>
        /// 执行cmd命令，返回条数
        /// </summary>
        /// <param name="comd"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 执行cmd命令，并返回结果中第一行的第一列的值
        /// </summary>
        /// <param name="comd"></param>
        /// <returns></returns>
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

        #region 返回执行条数
        /// <summary>
        /// 执行存储过程,返回执行条数
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public int Execute(string spName)
        {
            SqlCommand comd = CreateComd(spName);
            return ToExecute(comd);
        }

        /// <summary>
        /// 执行存储过程,返回执行条数
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int Execute(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
            return ToExecute(comd);
        }

        /// <summary> 
        /// 执行SQL语句,返回执行条数
        /// </summary> 
        public int ExecuteSql(string sqlStr)
        {
            SqlCommand comd = CreateSqlComd(sqlStr);
            return ToExecute(comd);
        }

        /// <summary>
        /// 执行sql语句,返回执行条数
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(string sqlStr, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
            return ToExecute(comd);
        }

        /// <summary>
        /// 执行sql语句,返回第一个值
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSqlInt(string sqlStr, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
            return ToExecuteInt(comd);
        }
        #endregion

        #region ** 执行并返回输出参数 **
        /// <summary>
        /// 执行存储过程，返回自定义输出参数
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
        /// 执行存储过程，返回默认输出参数 @Result Varchar(50)
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 执行存储过程，返回自定义输出参数
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <param name="retParam"></param>
        /// <returns></returns>
        public string ExecuteReturn(string spName, DbParameters pars, string retParam)
        {
            SqlCommand comd = CreateComd(spName, pars);
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
        /// 执行存储过程，返回默认输出参数ReturnValue
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public string ExecuteReturn(string spName, DbParameters pars)
        {
            SqlCommand comd = CreateComd(spName, pars);
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
        /// 执行SQL 返回自定义参数
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <param name="retParam"></param>
        /// <returns></returns>
        public string ExecuteSqlReturn(string sqlStr, DbParameters pars, string retParam)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
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
        /// 执行SQL 返回默认输出参数ReturnValue
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public string ExecuteSqlReturn(string sqlStr, DbParameters pars)
        {
            SqlCommand comd = CreateSqlComd(sqlStr, pars);
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
