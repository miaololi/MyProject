using MyProject.Models;
using MyProject.Tools;

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
