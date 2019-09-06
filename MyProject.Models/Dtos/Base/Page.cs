namespace MyProject.Models
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrPage { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalPage { get; set; }
    }
}
