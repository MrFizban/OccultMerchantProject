using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using warehouse.items;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentrallController : ControllerBase
    {
        
        private readonly ILogger<CentrallController> _logger;

        public CentrallController(ILogger<CentrallController> logger)
        {
            _logger = logger;
        }
        

        [HttpGet("/giveMeWeapons")]
        public IEnumerable<Weapons> getWeapons ()
        {
            System.Console.WriteLine("get request");
            return Weapons.weaponsList();
        }
        
        [HttpPost("/giveMeWeapons")]
        public HttpResponseMessage inserWeapon([FromBody]Weapons value)
        {
            Console.WriteLine("post request");
            value.insertToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/giveMeWeapons")]
        public HttpResponseMessage upsateWeapon([FromBody]Weapons value)
        {
            Console.WriteLine("put request");
            value.updateToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        [HttpDelete("/giveMeWeapons/{value}")]
        public HttpResponseMessage deleteWeapon(int value)
        {
            Console.WriteLine("delete request\t" +  value.ToString());
            Weapons.deleteFromDatabase(value);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

    }
}