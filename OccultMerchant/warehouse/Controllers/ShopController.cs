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
            return Shop.getAllShop();
        }
        
        [HttpGet("/shop/filter")]
        public IEnumerable<Shop> GetShopsFilter([FromBody] Filter option)
        {
            Console.WriteLine("[GET][SHOP] get filter by name");
            Console.WriteLine(option);
            return Shop.getAllShop(option.name);
        }

        [HttpPost("/shop")]
        public HttpResponseMessage addShopToDatabase([FromBody] Shop shop)
        {
            Console.WriteLine("[POST][SHOP] add shop");
            shop.addToDatabase();
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
        public HttpResponseMessage deleteShopDatabase([FromQuery]int id)
        {
            Console.WriteLine("[DELETE][SHOP] delete shop");
            Shop.deleteToDatabase(id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        
        
        
    }
}