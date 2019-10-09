using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyProject.Models
{
    [Table("t_test")]
    public class TestInfo
    {
        [Key]
        public int FID { get; set; }

        [MaxLength(50)]
        public string FName { get; set; }
    }
}
