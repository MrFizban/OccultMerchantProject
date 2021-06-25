using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            SqliteConnection connection = new SqliteConnection("Data Source=../ItemsDatabase.sqlite");
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM 'Weapons';";
            List<Weapons> list = new List<Weapons>();
               
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(Weapons.fromDatabase(reader));
                }
            }
            return list;
        }
        
        [HttpPost]
        [Route("/putWeapons")]
        public async Task Test([FromBody]Weapons accounts)
        {
            var tmp = accounts;
            Console.WriteLine("post request");
            
        }
        
    }
}