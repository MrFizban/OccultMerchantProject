using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using Warehouse.items;

namespace Warehouse.Controllers
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
        public IEnumerable<Shop> GetAllShop([FromQuery] Shop shop = null)
        {
            Console.WriteLine("[GET][SHOP] get all shop");
            if (shop == null || shop.filter.names.Count == 0)
            {
                Console.WriteLine("[GET][SHOP] get with null");
                Shop tmp = new Shop();
                return tmp.getAll();
            }
            else
            {
                Console.WriteLine("[GET][SHOP] get with filter");
                return shop.getAll();
            }
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