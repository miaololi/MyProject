﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Models
{
    public class Result
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
        public object Obj { get; set; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public Page Page { get; set; }
    }
}
