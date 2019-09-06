using Microsoft.AspNetCore.Mvc;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController: ControllerBase
    {
        /// <summary>
        /// Put value by id and value
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">value</param>
        /// PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
