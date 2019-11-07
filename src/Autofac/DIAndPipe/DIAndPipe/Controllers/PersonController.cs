using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIAndPipe.Entities;
using DIAndPipe.Services.Declare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DIAndPipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
       
        //属性注入
        public  IPersonService _personService { get; set; }

        // GET api/values
        [HttpGet]
        public ActionResult<Person> Get()
        {
            var person = _personService.GetPerson();
            if (person != null)
            {
                return person;
            }

            return BadRequest();
        }
        
       

    }
}