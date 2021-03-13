namespace MyProject.Models
{
    public class ChuanYunListOutDto
    {
        /// <summary>
        /// 返回结果是否成功true/false
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 未使用，忽略
        /// </summary>
        public bool Logined { get; set; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public ReturnDataDto ReturnData { get; set; }
        /// <summary>
        /// 返回的数据类型默认0
        /// </summary>
        public int DataType { get; set; }
    }

    public class ReturnDataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public object BizObjectArray { get; set; }
        /// <summary>
        /// 所有者ID
        /// </summary>
        public object OwnerIdObject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object CreatedByObject { get; set; }
        /// <summary>
        /// 所有者部门
        /// </summary>
        public object OwnerDeptIdObject { get; set; }
    }
}
