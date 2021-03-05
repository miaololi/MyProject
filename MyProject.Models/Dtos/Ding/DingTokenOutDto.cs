using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Models.Dtos.Ding
{
    /// <summary>
    /// 钉钉token返回dto
    /// </summary>
    public class DingTokenOutDto
    {
        /// <summary>
        /// 错误编码 0为正常
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 到期时间（默认3天 7200）
        /// </summary>
        public int expires_in { get; set; }
    }
}
