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

        public static TestInfo GetTestInfo(int id)
        {
            using (MySqlDbContext db = new MySqlDbContext())
            {
                return db.Tests.Where(w=>w.FID==id).First<TestInfo>();
            }
        }

        public static bool AddTestInfo(int id, string name)
        {
            using (MySqlDbContext db = new MySqlDbContext())
            {
                TestInfo info = new TestInfo() {
                    FName = name
                };
                db.Tests.Add(info);
                int n=db.SaveChanges();
                return n > 0;
            }
        }

        public static bool EditTestInfo(int id, string name)
        {
            using (MySqlDbContext db = new MySqlDbContext())
            {
                TestInfo info = new TestInfo()
                {
                    FID=id,
                    FName = name
                };
                db.Tests.Update(info);
                int n = db.SaveChanges();
                return n > 0;
            }
        }
    }
}
