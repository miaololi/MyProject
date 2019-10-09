using MyProject.Dal;
using MyProject.Models;
using MyProject.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyProject.Bll
{
    public class TestBll : BaseBll
    {
        public static Result TestHttp()
        {
            string baseUrl = "https://api.ooopn.com/ciba/api.php";
            Result result = HttpHelper.HttpGet(baseUrl);
            return result;
        }

        public static Result GetTestInfo()
        {
            Result result = new Result() { Code = 0 };
            try
            {
                List<TestInfo> list = TestDal.GetTestInfo();
                result.Code = 1;
                result.Obj = list;
                return result;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
