using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using warehouse.items;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }


        [HttpGet("/shop/gettAll")]
        public IEnumerable<Shop> GetAllShop()
        {
            Console.WriteLine("[GET][SHOP] get all shop");
            return Shop.getAll();
        }

        [HttpGet("/shop/id/{id}")]
        public Shop getPotionId(int id)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            var res = Shop.getAll(id = id);
            if (res.Count == 1)
            {
                return res[0];
            }
            else
            {
                Console.WriteLine(res.Count);
            }

            return null;
        }

        [HttpGet("/shop/name/{name}")]
        public IEnumerable<Shop> getPotionName(int name)
        {
            Console.WriteLine("[GET][POTION] get filter by id");
            return Shop.getAll(name = name);
        }

        [HttpPost("/shop")]
        public IActionResult addShopToDatabase([FromBody] Shop shop)
        {
            Console.WriteLine("[POST][SHOP] add shop");
            shop.addToDatabase();
            return Ok(shop);
        }

        [HttpPost("/shop/addPotion")]
        public HttpResponseMessage addPotion([FromBody] Shop shop)
        {
            Console.WriteLine("[POST][SHOP] add potions");
            shop.addPotions();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/shop")]
        public HttpResponseMessage updateShopDatabase([FromBody] Shop shop)
        {
            Console.WriteLine("[PUT][SHOP] update shop");
            shop.saveToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete("/shop/{id}")]
        public HttpResponseMessage deleteShopDatabase(int id)
        {
            Console.WriteLine("[DELETE][SHOP] delete shop:\t" + id);
            Shop.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        [HttpDelete("/shop/potionStock/{idShop}/{idPotion}")]
        public HttpResponseMessage deletePotionFromStock(int idShop, int idPotion)
        {
            Shop.deletePotioFromStock(idShop,idPotion);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}