using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.Tools;
using Microsoft.Extensions.Configuration;

namespace MyProject.Dal
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<TestInfo> Tests{ get; set; }
        public DbSet<EmpInfo> Emps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var mySqlconnection = "";// UtilConfigHelper.Configuration.GetConnectionString("Def.MySql");
            optionsBuilder.UseMySQL(mySqlconnection);//配置连接字符串
        }
    }
}
