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
    public class SpellController
    {
        private readonly ILogger<SpellController> _logger;

        public SpellController(ILogger<SpellController> logger)
        {
            _logger = logger;
        }


        
        [HttpGet("/spell/gettAll")]
        public IEnumerable<Spell> GetAllShop()
        {
            Console.WriteLine("[GET][SPELL] get all spell");
            return Spell.getAll();
        }
        
        [HttpGet("/spell/filter")]
        public IEnumerable<Spell> GetShopsFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][SPELL] get filter by name");
            Console.WriteLine(option);
            return Spell.getAll(option.name);
        }

        [HttpPost("/spell")]
        public HttpResponseMessage addShopToDatabase([FromBody] Spell spell)
        {
            Console.WriteLine("[POST][SPELL] add spell");
            spell.addToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/spell")]
        public HttpResponseMessage updateShopDatabase([FromBody] Spell spell)
        {
            Console.WriteLine("[PUT][SPELL] update spell");
            spell.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/spell/{id}")]
        public HttpResponseMessage deleteShopDatabase([FromQuery]int id)
        {
            Console.WriteLine("[DELETE][SPELL] delete spell");
            Shop.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }



    }
}