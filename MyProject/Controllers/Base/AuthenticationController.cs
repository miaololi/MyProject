using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MyProject.Api.Controllers
{
    /// <summary>
    /// 验证
    /// </summary>
    public class AuthenticationController:Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 返回token
        /// </summary>
        /// <returns></returns>
        public IActionResult Post()
        {
            var authorzationHeader = Request.Headers["Authorization"].First();
            var key = authorzationHeader.Split(' ')[1];
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(':');
            var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
            if (credentials[0] == "username" && credentials[1] == "password")
            {
                var result = new {
                    token = GenerateToken(serverSecret)
                };

                return Ok(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        private string GenerateToken(SecurityKey key)
        {
            DateTime timeNow = DateTime.UtcNow;
            string issuer = Configuration["JWT:Issuer"];
            string audience = Configuration["JWT:Audience"];
            ClaimsIdentity identity = new ClaimsIdentity();
            SigningCredentials subject =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(issuer, audience, identity, timeNow, timeNow.Add(TimeSpan.FromHours(1)), timeNow, subject);
            return handler.WriteToken(token);
        }
    }
}
