using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondaryLocation.Entities;
using SecondaryLocation.Filters;


namespace SecondaryLocation.Controllers
{
    [ApiController]
    [Route("item")]
    [EnableCors("MyPolicy")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> logger;
        private readonly ApplicationDbContext context;

        public ItemController(ILogger<ItemController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IItem>>> getAllItme()
        {
            return Ok(await this.context.Item.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IItem>> getItem(Guid id)
        {
            var res = await this.context.Item.Where(I => I.id == id).SingleOrDefaultAsync();
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }


        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] ItemFilter filter)
        {
            HashSet<Item> items = new HashSet<Item>();
            if (filter.id != null)
            {
                items.Add(await this.context.Item.Where(item => item.id == filter.id).SingleOrDefaultAsync());
            }

            if (filter.name != null)
            {
                var lista = await this.context.Item.Where(item => item.name.Contains(filter.name)).ToListAsync();
                items.UnionWith(lista);
            }

            if (filter.description != null)
            {
                var lista = await this.context.Item.Where(item => item.description.Contains(filter.description))
                    .ToListAsync();
                items.UnionWith(lista);
            }

            if (filter.source != null)
            {
                var lista = await this.context.Item.Where(item => item.source.Contains(filter.source)).ToListAsync();
                items.UnionWith(lista);
            }

            if (filter.ItemType != null)
            {
                //var lista = await this.context.Item.Where(item => item.ItemType == filter.ItemType).ToListAsync();
                //items.UnionWith(lista);
            }

            if (filter.price != null)
            {
                List<Item> lista = new List<Item>();

                if (filter.priceOp == '=')
                {
                    lista = await this.context.Item.Where(item => item.price == filter.price).ToListAsync();
                }
                else if (filter.priceOp == '>')
                {
                    lista = await this.context.Item.Where(item => item.price > filter.price).ToListAsync();
                }
                else if (filter.priceOp == '<')
                {
                    lista = await this.context.Item.Where(item => item.price < filter.price).ToListAsync();
                }
                else
                {
                    lista = await this.context.Item.Where(item => item.price == filter.price).ToListAsync();
                }


                items.UnionWith(lista);
            }

            if (items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }


        [HttpPost]
        public async Task<ActionResult<IItem>> createItem([FromBody] Item item)
        {
            item.id = Guid.NewGuid();
            var res = Ok(this.context.Item.Add(item).Entity);
            await this.context.SaveChangesAsync();
            logger.Log(LogLevel.Information, "[POST] post new item");
            return res;
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

            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteItem(Guid id)
        {
            var res = Ok(this.context.Item.Remove(new Item(id)).Entity);
            await this.context.SaveChangesAsync();
            return res;
        }
    }
}