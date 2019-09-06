using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Models
{
    public class LoginDto
    {
        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 登录外网IP
        /// </summary>
        public string Host { get; set; }
    }
}
