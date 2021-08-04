using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondaryLocation.Entities;
using SecondaryLocation.Filters;

namespace SecondaryLocation.Controllers
{
    [ApiController]
    [Route("potion")]
    [EnableCors("MyPolicy")]
    public class PotionController : ControllerBase
    {
         private readonly ILogger<ItemController> logger;
        private readonly ApplicationDbContext context;

        public PotionController(ILogger<ItemController> logger, ApplicationDbContext context)
        {
            this.context = context;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<IPotion>>> getAllSpell()
        {
            List<Potion> query2 = await context.Potion.Include(potion  => potion.Item).ToListAsync();

            logger.Log(LogLevel.Information, "[GET] get all spell");
            return Ok(query2);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IPotion>> getSpell(Guid id)
        {
            var query = await  context.Potion.Include(potion  => potion.Item).Where(potion => potion.id == id).SingleOrDefaultAsync();
            
            logger.Log(LogLevel.Information, "[GET] get id:\t" + id.ToString());
            return Ok(query);
        }

        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] PotionFilter filter)
        {
            HashSet<Potion> potions = new HashSet<Potion>();
            if (filter.id != null)
            {
                //find by id
                var query = await context.Potion.Include(potion => potion.Item)
                    .Where(potion => potion.id == filter.id).SingleOrDefaultAsync(); 
                potions.Add(query);
            }

            if (filter.name != null)
            {
                var query = await context.Potion.Include(potion => potion.Item)
                    .Where(potion => potion.Item.name.ToUpper().Contains(filter.name.ToUpper()))
                    .ToListAsync();
                
                potions.UnionWith(query);
            }

            if (filter.description != null)
            {
                var query = await context.Potion.Include(potion => potion.Item)
                    .Where(potion => potion.Item.description.ToUpper().Contains(filter.description.ToUpper()))
                    .ToListAsync();
                
                potions.UnionWith(query);
            }

            if (filter.source != null)
            {
                var query = await context.Potion.Include(potion => potion.Item)
                    .Where(potion => potion.Item.source.ToUpper().Contains(filter.source.ToUpper()))
                    .ToListAsync();
                
                potions.UnionWith(query);
            }


            if (filter.price != null)
            {
                if (filter.priceOp == '=')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.Item.price == filter.price)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else if (filter.priceOp == '>')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.Item.price > filter.price)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else if (filter.priceOp == '<')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.Item.price < filter.price)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.Item.price == filter.price)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
            }

            // TODO casterLevell

            if (filter.casterLevell != null)
            {
                var query = await context.Potion.Include(potion => potion.Item)
                    .Where(potion => potion.casterLevell== filter.casterLevell)
                    .ToListAsync();
                
                potions.UnionWith(query);
            }

            // TODO wheight

            if (filter.wheight != null)
            {
                if (filter.wheightOp == '=')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.wheight == filter.wheight)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else if (filter.wheightOp == '>')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.wheight > filter.wheight)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else if (filter.wheightOp == '<')
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.wheight < filter.wheight)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
                else
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.wheight == filter.wheight)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }


                // TODO idSpell

                if (filter.idSpell != null)
                {
                    var query = await context.Potion.Include(potion => potion.Item)
                        .Where(potion => potion.spell.id == filter.idSpell)
                        .ToListAsync();
                
                    potions.UnionWith(query);
                }
            }

            if (potions.Count == 0)
            {
                return NotFound();
            }

            return Ok(potions);
        }


        [HttpPost]
        public async Task<ActionResult<IPotion>> createSpell([FromBody] Potion potion)
        {
            potion.id = Guid.NewGuid();
            potion.Item.id = Guid.NewGuid();
            potion.ItemId = potion.Item.id;
            Item item = potion.Item;
            potion.Item = null;
            item.potion = potion;
            var res = this.context.Item.Add(item).Entity;
            var res2 = this.context.Potion.Add(potion).Entity;
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[POST] post spell");
            return Ok(res2);
        }

        [HttpPatch]
        public async Task<ActionResult<IPotion>> updateSpell([FromBody] Potion potion)
        {
            if (this.context.Item.Any(item => item.id == potion.id))
            {
                potion.id = Guid.NewGuid();
                potion.Item.id = Guid.NewGuid();
                potion.ItemId = potion.Item.id;
                this.context.Potion.Add(potion);
            }
            else
            {
                this.context.Potion.Update(potion);
            }

            logger.Log(LogLevel.Information, "[PATCH] update spell");
            await this.context.SaveChangesAsync();
            return Ok(potion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
            var res = this.context.Potion.Remove(new Potion(id)).Entity;
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[DELETE] spell spell");
            return Ok(res);
        }
        
        
    }
}