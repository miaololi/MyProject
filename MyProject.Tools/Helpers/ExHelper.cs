﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MyProject.Tools
{
    /// <summary>
    /// 扩展工具
    /// </summary>
    public static class ExHelper
    {
        #region dt to list
        /// <summary>
        /// DataTable转成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToDtList<T>(this DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]) && item[i].ToString() != "")
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        #endregion

        #region dt to dto
        /// <summary>
        /// DataTable转成Dto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToDtDto<T>(this DataTable dt)
        {
            T s = Activator.CreateInstance<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return s;
            }
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                if (info != null)
                {
                    try
                    {
                        if (!Convert.IsDBNull(dt.Rows[0][i]) && dt.Rows[0][i].ToString() != "")
                        {
                            object v = null;
                            if (info.PropertyType.ToString().Contains("System.Nullable"))
                            {
                                v = Convert.ChangeType(dt.Rows[0][i], Nullable.GetUnderlyingType(info.PropertyType));
                            }
                            else
                            {
                                v = Convert.ChangeType(dt.Rows[0][i], info.PropertyType);
                            }
                            info.SetValue(s, v, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                    }
                }
            }
            return s;
        }
        #endregion

        #region list to dt
        /// <summary>
        /// 将实体集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体集合</param>
        public static DataTable ToListDt<T>(List<T> entities)
        {
            var result = CreateTable<T>();
            FillData(result, entities);
            return result;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        private static DataTable CreateTable<T>()
        {
            var result = new DataTable();
            var type = typeof(T);
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                var propertyType = property.PropertyType;
                if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    propertyType = propertyType.GetGenericArguments()[0];
                result.Columns.Add(property.Name, propertyType);
            }
            return result;
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        private static void FillData<T>(DataTable dt, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                dt.Rows.Add(CreateRow(dt, entity));
            }
        }

        /// <summary>
        /// 创建行
        /// </summary>
        private static DataRow CreateRow<T>(DataTable dt, T entity)
        {
            DataRow row = dt.NewRow();
            var type = typeof(T);
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                row[property.Name] = property.GetValue(entity) ?? DBNull.Value;
            }
            return row;
        }
        #endregion

        #region md5
        /// netcore下的实现MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
        #endregion

        #region 获取备注
        /// <summary>
        /// 获取备注
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            FieldInfo field = type.GetField(enumValue.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
                return remarkAttribute.Remark;
            }
            else
            {
                return enumValue.ToString();
            }
        }
        #endregion
        #region 转换时间戳
        /// <summary>
        ///  时间戳转本地时间-时间戳精确到秒
        /// </summary> 
        public static DateTime ToLocalTimeDateBySeconds(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间转时间戳Unix-时间戳精确到秒
        /// </summary> 
        public static long ToUnixTimestampBySeconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeSeconds();
        }


        /// <summary>
        ///  时间戳转本地时间-时间戳精确到毫秒
        /// </summary> 
        public static DateTime ToLocalTimeDateByMilliseconds(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeMilliseconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间转时间戳Unix-时间戳精确到毫秒
        /// </summary> 
        public static long ToUnixTimestampByMilliseconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeMilliseconds();
        }
        #endregion

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="robotSecret"></param>
        /// <param name="zTime">当前时间戳</param>
        /// <returns></returns>
        public static string AddSign(string robotSecret, long zTime)
        {
            string stringToSign = zTime + "\n" + robotSecret;
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(robotSecret);
            byte[] messageBytes = encoding.GetBytes(stringToSign);
            using var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(hashmessage), Encoding.UTF8);
        }

        /// <summary>
        /// 宜搭加签
        /// </summary>
        /// <param name="yiDaSecretKey">SecretKey</param>
        /// <param name="method">POST/GET 大写</param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="url">HTTP URL中的路径部分，如 "/yida_epaas/demo.json"</param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static string Sign(string yiDaSecretKey, string method, string timestamp, string nonce, string url, Dictionary<string, string> pars)
        {
            #region 测试传参
            //yiDaSecretKey = "";//问宜搭工作人员要
            //method = "GET";
            //timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzzz");//utc时间ISO8601格式
            //nonce = string.Format(@$"{DateTime.Now.ToUnixTimestampByMilliseconds()}{new Random().Next(1000, 9999)}");//13位时间戳毫秒级+4位随机数
            //url = "/yida_vpc/form/searchFormDatas.json";
            //pars = new Dictionary<string, string> {
            //    { "id","1"}
            //};
            #endregion

            var encoding = new System.Text.UTF8Encoding();
            //SecretKey作为密钥
            byte[] keyByte = encoding.GetBytes(yiDaSecretKey);
            //Hmac-Sha256加密算法
            using var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte);

            //HttpRequestMethod+'\n'+HttpRequestHeaderTimestamp+'\n'+HttpRequestHeaderNonce+'\n'+CanonicalURI+'\n'+HttpRequestParams
            var strb = new StringBuilder();
            pars = pars.OrderBy(o => (o.Key, o.Value)).ToDictionary(k => k.Key, v => v.Value);
            //参数 格式 组装key1=value1&key2=value2。
            for (int i = 0; i < pars.Count; i++)
            {
                if (i != 0)
                {
                    strb.Append("&");
                }
                strb.Append($@"{pars.ElementAt(i).Key}={pars.ElementAt(i).Value}");
            }
            //计算请求签名时请将StringToSign作为消息
            string stringToSign = $"{method}\n{timestamp}\n{nonce}\n{url}\n{strb}";
            byte[] messageBytes = encoding.GetBytes(stringToSign);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }
}

