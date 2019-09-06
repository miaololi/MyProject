using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MyProject.Tools
{
    public class DbParameters
    {
        private List<SqlParameter> sqlPars;
        public DbParameters()
        {
            sqlPars = new List<SqlParameter>();
        }

        public DbParameters(string strName, object strValue)
        {
            sqlPars = new List<SqlParameter>();
            this.Add(strName, strValue);
        }

        //长度
        public int Len
        {
            get { return sqlPars.Count(); }
        }

        //索引
        public SqlParameter this[int k]
        {
            get
            {
                if (sqlPars.Contains(sqlPars[k]))
                {
                    return sqlPars[k];
                }
                else
                {
                    return null;
                }
            }
        }

        public void Add(string sName, object sValue)
        {
            sqlPars.Add(new SqlParameter()
            {
                ParameterName = sName.Trim(),
                Value = sValue ?? DBNull.Value,
                Direction=ParameterDirection.Input
            });
        }

        public void AddOut()
        {
            AddOut("@Result", "int", 4);
        }

        public void AddOut(string sName, string sDbType, int iSize)
        {
            sqlPars.Add(new SqlParameter
            {
                ParameterName=sName,
                SqlDbType=ConvertSqlDbType(sDbType),
                Size=iSize,
                Direction=ParameterDirection.Output
            });
        }

        public void AddInputOutput(string sName)
        {
            sqlPars.Add(new SqlParameter
            {
                ParameterName=sName,
                Direction=ParameterDirection.InputOutput
            });
        }

        public void AddInputOutput(string sName,string sDbType,int iSize)
        {
            sqlPars.Add(new SqlParameter
            {
                ParameterName = sName,
                SqlDbType=ConvertSqlDbType(sDbType),
                Size=iSize,
                Direction = ParameterDirection.InputOutput
            });
        }

        #region ** 参数转换函数
        //SqlDbType数据类型转换
        private SqlDbType ConvertSqlDbType(string strDbType)
        {
            SqlDbType t = new SqlDbType();
            switch (strDbType.Trim().ToLower())
            {
                case "nvarchar": t = SqlDbType.NVarChar; break;
                case "nchar": t = SqlDbType.NChar; break;
                case "varchar": t = SqlDbType.VarChar; break;
                case "char": t = SqlDbType.Char; break;
                case "int": t = SqlDbType.Int; break;
                case "datetime": t = SqlDbType.DateTime; break;
                case "decimal": t = SqlDbType.Decimal; break;
                case "bit": t = SqlDbType.Bit; break;
                case "text": t = SqlDbType.Text; break;
                case "ntext": t = SqlDbType.NText; break;
                case "money": t = SqlDbType.Money; break;
                case "float": t = SqlDbType.Float; break;
                case "binary": t = SqlDbType.Binary; break;
            }
            return t;
        }

        #endregion

        public void Clear()
        {
            sqlPars.Clear();
        }
    }
}
