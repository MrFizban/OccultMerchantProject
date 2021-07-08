using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using SecondaryLocation.Items;
using SecondaryLocation.Reposotory;

namespace SecondaryLocation.Controllers
{
    [ApiController]
    [Route("item")]
    [EnableCors("MyPolicy")]
    public class ItemController : ControllerBase
    {
        private IItemRepository itemRepository;
        private readonly ILogger<ItemController> logger;

        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            this.itemRepository = itemRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IItem>>> getAllItme()
        {
            return Ok(this.itemRepository.getAllItem());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IItem>> getItem(Guid id)
        {
            var result = await this.itemRepository.getItem(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                Console.WriteLine("found: \t" + result);
                return Ok(result);
            }
        }
        
        
        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] Filter filter)
        {
            return Ok(this.itemRepository.find(filter));
        }


        [HttpPost]
        public async Task<ActionResult<IItem>> createItem([FromBody] Item item)
        {
            item.id = Guid.NewGuid();

            await this.itemRepository.addItem(item);
            return  new ObjectResult(item){StatusCode = 201};
        }
        
        [HttpPatch]
        public async Task<ActionResult<IItem>> updateItem([FromBody] Item item)
        {
            await this.itemRepository.updateItem(item);
            return Ok(item);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteItem(Guid id)
        {
           
            await this.itemRepository.deleteItem(id);
            return Ok(id.ToString());
        }
    }
}