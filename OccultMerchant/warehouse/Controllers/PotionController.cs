using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using warehouse.items;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
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
            Console.WriteLine("[GET][POTION] get all potion");
            return Potion.getAll();
        }
        
        [HttpGet("/potion/filter")]
        public IEnumerable<Potion> getPotionFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][POTION] get filter by name");
            Console.WriteLine(option);
            return Potion.getAll(name: option.name);
        }

        [HttpPost("/potion")]
        public HttpResponseMessage addPotionToDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[POST][POTION] add potion");
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.Accepted);
            message.Content = new StringContent(potion.addToDatabase().ToString(), Encoding.UTF8, "application/json");
            return message;
        }

        [HttpPut("/potion")]
        public HttpResponseMessage updatePotionDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[PUT][POTION] update potion");
            potion.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/potion/{id}")]
        public HttpResponseMessage deletePotionDatabase(int id)
        {
            Console.WriteLine("[DELETE][POTION] delete potion");
            Potion.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}