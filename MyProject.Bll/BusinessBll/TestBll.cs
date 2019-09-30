using MyProject.Models;
using MyProject.Tools;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
