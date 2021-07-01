using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using warehouse.items;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class WeaponController : ControllerBase
    {
        
        private readonly ILogger<WeaponController> _logger;

        public WeaponController(ILogger<WeaponController> logger)
        {
            _logger = logger;
        }
        

        [HttpGet("/weapon/gettAll")]
        public IEnumerable<Weapon> getAllPotion()
        {
            Console.WriteLine("[GET][WEAPON] get all weapons");
            return Weapon.getAll();
        }
        
        [HttpGet("/weapon/filter")]
        public IEnumerable<Weapon> getPotionFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][WEAPON] get filter by name");
            Console.WriteLine(option);
            return Weapon.getAll(option.name);
        }

        [HttpPost("/weapon")]
        public HttpResponseMessage addPotionToDatabase([FromBody] Weapon weapon)
        {
            Console.WriteLine("[POST][WEAPON] add weapon");
            weapon.addToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/weapon")]
        public HttpResponseMessage updatePotionDatabase([FromBody]  Weapon weapon)
        {
            Console.WriteLine("[PUT][WEAPON] update weapon");
            weapon.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/weapon/{id}")]
        public HttpResponseMessage deletePotionDatabase([FromQuery]int id)
        {
            Console.WriteLine("[DELETE][WEAPON] delete weapon");
            Weapon.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }

}
