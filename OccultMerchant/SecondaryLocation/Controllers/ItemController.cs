using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        public ItemController(IItemRepository itemRepository)
        {

            this.itemRepository = itemRepository;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IItem>>> getAllItme()
        {
            return  Ok(this.itemRepository.getAllItem());
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
    }
}