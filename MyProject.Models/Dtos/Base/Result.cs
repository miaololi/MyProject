using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Models
{
    public class Result
    {
        public Result()
        {
        }

        /// <summary>
        /// 0失败 1成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        public object Obj { get; set; }

        /// <summary>
        /// 扩展对象
        /// </summary>
        public object ObjEx { get; set; }

        /// <summary>
        /// 字符串
        /// </summary>
        public string StrOjb { get; set; }

        /// <summary>
        /// json
        /// </summary>
        public string JsonObj { get; set; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public Page Page { get; set; }
    }

    public class Result<T>
    {
        /// <summary>
        /// 0失败 1成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        public T Obj { get; set; }

        /// <summary>
        /// 扩展对象
        /// </summary>
        public object ObjEx { get; set; }

        /// <summary>
        /// json
        /// </summary>
        public string JsonObj { get; set; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public Page Page { get; set; }
    }
}
