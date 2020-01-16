namespace MyProject.Models
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 页数
        /// </summary>
        /// <param name="cur">当前页</param>
        /// <param name="size">每页条数</param>
        /// <param name="total">总条数</param>
        /// <param name="IsZeroStart">当前页是否从0开始(1是0否)</param>
        public Page(int cur, int size, int total, int IsZeroStart = 0)
        {
            this.CurrPage = cur;
            this.PageSize = size;
            this.TotalPage = total;
            int surplus = total % size;
            this.PageCount = total / size;

            if (surplus > 0)
            {
                this.PageCount = this.PageCount + 1;
            }

            if (cur < this.PageCount)
            {
                this.IsHaveNextPage = 1;
                this.NextPage = cur + 1;
            }
            else
            {
                this.IsHaveNextPage = 0;
                this.NextPage = cur;
            }

            if (cur + IsZeroStart > 1)
            {
                this.IsHavePreviousPage = 1;
                this.PreviousPage = cur - 1;
            }
            else
            {
                this.IsHavePreviousPage = 0;
                this.PreviousPage = cur;
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrPage { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public int IsHavePreviousPage { get; set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public int IsHaveNextPage { get; set; }

        /// <summary>
        /// 上一页
        /// </summary>
        public int? PreviousPage { get; set; }

        /// <summary>
        /// 下一页
        /// </summary>
        public int? NextPage { get; set; }
    }
}
