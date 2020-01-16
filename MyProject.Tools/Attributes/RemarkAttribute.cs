using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Tools
{
    /// <summary>
    /// 是给枚举用  提供一个额外信息
    /// AllowMultiple特性影响编译器，AttributeTargets修饰的对象 AllowMultiple：能否重复修饰 Inherited:是否可继承
    /// 可以指定属性和字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute()
        {
        }
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }
        public string Description; //字段
        public string Remark { get; private set; }
    }
}
