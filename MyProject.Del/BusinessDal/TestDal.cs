using MyProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.Dal
{
    public class TestDal : BaseDal
    {
        public static List<TestInfo> GetTestInfo()
        {
            using(MySqlDbContext db=new MySqlDbContext())
            { 
                List<TestInfo> list = db.Tests.ToList();
                return list;
            }
        }
    }
}
