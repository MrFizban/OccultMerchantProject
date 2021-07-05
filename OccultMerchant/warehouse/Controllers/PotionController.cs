using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Server;
using warehouse.Database;
using warehouse.items;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class PotionController: ControllerBase 
    {
        private readonly ILogger<PotionController> _logger;
        private Request request ;
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
        
        [HttpGet("/potion/id/{id}")]
        public Potion getPotionId(int id)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            return Potion.getPotion(id);
        }
        
        [HttpGet("/potion/{name}")]
        public IEnumerable<Potion> getPotionName(int name)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            return Potion.getAll(name = name);
        }

        
        [HttpPost("/potion")]
        public IActionResult addPotionToDatabase([FromBody] Potion potion)
        {
            Console.WriteLine("[POST][POTION] add potion");
            long tmp = potion.addToDatabase();

            MyResponse response = new MyResponse(HttpStatusCode.Created, tmp);
            return Ok(potion);
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