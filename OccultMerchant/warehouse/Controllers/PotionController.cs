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
    public class PotionController: ControllerBase
    {
        private readonly ILogger<PotionController> _logger;

        public PotionController(ILogger<PotionController> logger)
        {
            _logger = logger;
        }


        [HttpGet("/potion/gettAll")]
        public IEnumerable<Potion> getAllPotion()
        {
            Console.WriteLine("[GET][SHOP] get all shop");
            return Potion.getAllPotion();
        }
        
        [HttpGet("/potion/filter")]
        public IEnumerable<Potion> getPotionFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][SHOP] get filter by name");
            Console.WriteLine(option);
            return Potion.getAllPotion(option.name);
        }

        [HttpPost("/potion")]
        public HttpResponseMessage addPotionToDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[POST][SHOP] add shop");
            potion.addToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/potion")]
        public HttpResponseMessage updatePotionDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[PUT][SHOP] update shop");
            potion.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/potion/{id}")]
        public HttpResponseMessage deletePotionDatabase([FromQuery]int id)
        {
            Console.WriteLine("[DELETE][SHOP] delete shop");
            Potion.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}