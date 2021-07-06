using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.items;

namespace Warehouse.Controllers
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
        
        [HttpGet("/spell/{id}")]
        public IEnumerable<Potion> getPotionId(int id)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            return Potion.getAll(id = id);
        }
        
        [HttpGet("/spell/{name}")]
        public IEnumerable<Potion> getPotionName(int name)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            return Potion.getAll(name = name);
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