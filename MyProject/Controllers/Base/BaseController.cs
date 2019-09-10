using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {

    }
}
