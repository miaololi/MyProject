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

        public static Result AddTestInfo(int id, string name)
        {
            Result result = new Result() { Code = 0 };
            try
            {
                var info = TestDal.GetTestInfo(id);
                if (info != null&&info.FID>0)
                {
                    if (TestDal.EditTestInfo(id, name))
                    {
                        result.Code = 1;
                        result.Message = "修改成功";
                    }
                    else
                    {
                        result.Message = "修改失败";
                    }
                }
                else
                {
                    if (TestDal.AddTestInfo(id, name))
                    {
                        result.Code = 1;
                        result.Message = "添加成功";
                    }
                    else
                    {
                        result.Message = "添加失败";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
