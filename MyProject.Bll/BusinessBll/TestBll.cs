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
            List<TestInfo> list = null;//TestDal.GetTestInfo();
            return new Result { Code = 1, Obj = list };
        }
    }
}
