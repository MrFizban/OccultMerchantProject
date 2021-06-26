using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using OccultMerchant.items;

namespace OccultMerchant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentrallController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        
        private readonly ILogger<CentrallController> _logger;

        public CentrallController(ILogger<CentrallController> logger)
        {
            _logger = logger;
           
        }
        
        

        [HttpGet("/weatherforecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 9).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
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