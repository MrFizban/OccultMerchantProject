using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using OccultMerchant.items;

namespace OccultMerchant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentrallController : ControllerBase
    {

        private readonly ILogger<CentrallController> _logger;

        public CentrallController(ILogger<CentrallController> logger)
        {
            _logger = logger;
           
        }
     
        

        [HttpGet("/giveMeWeapons")]
        public IEnumerable<Weapons> getWeapons ()
        {
            System.Console.WriteLine("[giveMeWeapons] get request");
            return Weapons.weaponsList();
        }
        
        [HttpPost("/giveMeWeapons")]
        public HttpResponseMessage inserWeapon([FromBody]Weapons value)
        {
            Console.WriteLine("[giveMeWeapons] post request");
            value.insertToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/giveMeWeapons")]
        public HttpResponseMessage upsateWeapon([FromBody]Weapons value)
        {
            Console.WriteLine("[giveMeWeapons] put request");
            value.updateToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        [HttpDelete("/giveMeWeapons/{value}")]
        public HttpResponseMessage deleteWeapon(int value)
        {
            Console.WriteLine("[giveMeWeapons] delete request\t" +  value.ToString());
            Weapons.deleteFromDatabase(value);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        
        [HttpGet("/giveMeShops")]
        public List<Shop> getShop ()
        {
            System.Console.WriteLine("[giveMeShop] get request");
            return Shop.weaponsList();
        }
        
        [HttpPost("/giveMeShops")]
        public HttpResponseMessage inserShop([FromBody]Shop value)
        {
            Console.WriteLine("[giveMeShop] post request");
            Console.WriteLine(value.weaponsItems.Count);
            value.insertToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPut("/giveMeShops")]
        public HttpResponseMessage upsateshop([FromBody]Shop value)
        {
            Console.WriteLine("[giveMeShop] put request");
            value.updateToDatabase();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        [HttpPut("/giveMeShops/addItems")]
        public HttpResponseMessage additem([FromBody]Shop value)
        {
            Console.WriteLine("[giveMeShop/addItems] put request:\t" + value.id);
            value.inserItemsToDatabase(value.id);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        [HttpDelete("/giveMeShops/{value}")]
        public HttpResponseMessage deleteshop(int value)
        {
            Console.WriteLine("[giveMeShop] delete request\t" +  value.ToString());
            Shop.deleteShopFromDatabase(value);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        [HttpDelete("/giveMeShops/item/{idShop}/{idWeapon}")]
        public HttpResponseMessage deleteitem(int idShop, int idWeapon)
        {
            Console.WriteLine("[giveMeShop] delete request\t" +  idShop.ToString()+ ":" + idWeapon.ToString());
            Shop.deleteItemFromDatabase(idShop,idWeapon);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        
        

    }
}