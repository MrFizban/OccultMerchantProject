using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
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
        
        private readonly ILogger<ItemController> logger;
        private readonly ApplicationDbContext context;

        public ItemController( ILogger<ItemController> logger,ApplicationDbContext context )
        {
            
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IItem>>> getAllItme()
        {
            return Ok(this.context.Item.ToList());
        }
        
       


        [HttpGet("{id}")]
        public async Task<ActionResult<IItem>> getItem(Guid id)
        {
            //var res = this.context.Item.Where(I => I.id.CompareTo(id)>0).SingleOrDefault();
            var res = this.context.Item.FromSqlRaw($"SELECT * FROM Item WHERE id='{id.ToString()}'").SingleOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        
        
        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] Filter filter)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public async Task<ActionResult<IItem>> createItem([FromBody] Item item)
        {
            item.id = Guid.NewGuid();
            this.context.Item.Add(item);
            return item;
        }
        
        [HttpPatch]
        public async Task<ActionResult<IItem>> updateItem([FromBody] Item item)
        {
            var res = this.context.Item.Update(item).Entity;
            if (this.context.Item.FromSqlRaw($"SELECT * FROM Item WHERE id='{item.id.ToString()}'").SingleOrDefault() ==
                null)
            {
                this.context.Item.Add(item);
            }
            else
            {
                this.context.Item.Update(item);
            }
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}