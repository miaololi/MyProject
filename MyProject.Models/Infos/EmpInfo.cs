using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    /// <summary>
    /// 员工表
    /// </summary>
    [Table("t_emp")]
    public class EmpInfo
    {
        [Key]
        public int FID { get; set; }

        [MaxLength(50)]
        public string FName { get; set; }
    }
}
