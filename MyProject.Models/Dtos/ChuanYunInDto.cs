using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class ChuanYunInDto
    {
        /// <summary>
        /// 动作名 //LoadBizObjects多个//LoadBizObject单个
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 设计订单编号
        /// </summary>
        public string SchemaCode { get; set; }

        /// <summary>
        /// 数据订单唯一ID
        /// </summary>
        public string BizObjectId { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }

    }
    /// <summary>
    /// 过滤条件
    /// </summary>
    public class FilterDto
    {
        /// <summary>
        /// 起始行号
        /// </summary>
        public int FromRowNum { get; set; }
        /// <summary>
        /// 行号结束
        /// </summary>
        public int ToRowNum { get; set; }
        /// <summary>
        /// 查询的总行数 false
        /// </summary>
        public String RequireCount { get; set; }

        /// <summary>
        /// 返回的字段，不填返回所有
        /// </summary>
        public List<string> ReturnItems { get; set; }
        /// <summary>
        /// 排序字段，目前不支持使用，默认置空
        /// </summary>
        public List<string> SortByCollection { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public MatcherDto Matcher { get; set; }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    public class MatcherDto 
    {
        /// <summary>
        /// 关联类型 And or
        /// </summary>
        public string Type { get; set; }

        public List<MatcherDto> Matchers { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 运算符：0 =大于，1=大于等于，2=等于，3=小于等于，4=小于，5=不等于，6=在某个范围内，7=不在某个范围内。
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
