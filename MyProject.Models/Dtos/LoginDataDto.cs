using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Models
{
    public class LoginDataDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        ///新加的用户部门ID
        /// </summary>
        public string UserFDeptID { get; set; }

        /// <summary>
        ///角色ID
        /// </summary>
        public string UserRoleIds { get; set; }

        /// <summary>
        ///角色名称
        /// </summary>
        public string UserRoleName { get; set; }

        /// <summary>
        /// 拥有的部门IDs
        /// </summary>
        public string UserDeptIds { get; set; }

        /// <summary>
        /// 拥有的部门名称
        /// </summary>
        public string UserDeptNames { get; set; }

        /// <summary>
        ///所拥有的菜单
        /// </summary>
        public List<MenuForZtree> UserMenuJson { get; set; }
    }

    public class MenuForZtree
    {
        public List<MenuForZtree> children { get; set; }
        public int id { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public string href { get; set; }
    }
}
