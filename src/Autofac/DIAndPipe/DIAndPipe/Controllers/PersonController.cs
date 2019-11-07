using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DIAndPipe.Entities;
using DIAndPipe.Services.Declare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DIAndPipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PersonController : ControllerBase
    {

        //属性注入
        //public  IPersonService _personService { get; set; }
        private IPersonService _personService;
        private IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="configuration"></param>
        public PersonController(IPersonService personService, IConfiguration configuration)
        {
            //this._efContext = efcontext;
            this._personService = personService;
            this._configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Person> Get()
        {
            /*using (_efContext)
           {
               var person = _efContext.Persons.Select(x => x).FirstOrDefault();
               if (person != null)
               {
                   return person;
               }

               return BadRequest();
           }*/
            var person = _personService.GetPerson();
            if (person != null)
            {
                return person;
            }

            return BadRequest();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("token")]

        public IActionResult Post(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss: The issuer of the token，token 是给谁的  发送者
                    audience: 接收的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                var token = new JwtSecurityToken(
                    issuer: "jwttest",
                    audience: "jwttest",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }


            return BadRequest("用户名密码错误");
        }


    }
}