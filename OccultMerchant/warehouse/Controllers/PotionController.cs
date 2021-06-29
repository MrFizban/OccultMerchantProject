using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OccultMerchant.items;

namespace OccultMerchant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PotionController: ControllerBase
    {
        private readonly ILogger<CentrallController> _logger;

        public PotionController(ILogger<CentrallController> logger)
        {
            _logger = logger;
        }


        [HttpGet("/shop/gettAll")]
        public IEnumerable<Shop> GetAllPotion()
        {
            Console.WriteLine("[GET][SHOP] get all shop");
            return Potion.getAllPotion();
        }
        
        [HttpGet("/shop/filter")]
        public IEnumerable<Shop> GetPotionFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][SHOP] get filter by name");
            Console.WriteLine(option);
            return Potion.getAllPotion(option.name);
        }

        [HttpPost("/shop")]
        public HttpResponseMessage addPotionToDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[POST][SHOP] add shop");
            potion.addToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/shop")]
        public HttpResponseMessage updatePotionDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[PUT][SHOP] update shop");
            potion.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/shop/{id}")]
        public HttpResponseMessage deletePotionDatabase([FromQuery]int id)
        {
            Console.WriteLine("[DELETE][SHOP] delete shop");
            Potion.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}