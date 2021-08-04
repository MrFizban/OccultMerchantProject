using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecondaryLocation.Entities;
using SecondaryLocation.Filters;
using Formatting = System.Xml.Formatting;


namespace SecondaryLocation.Controllers
{
    [ApiController]
    [Route("spell")]
    [EnableCors("MyPolicy")]
    public class SpellController : ControllerBase
    {
        private readonly ILogger<ItemController> logger;
        private readonly ApplicationDbContext context;

        public SpellController(ILogger<ItemController> logger, ApplicationDbContext context)
        {
            this.context = context;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spell>>> getAllSpell()
        {

            List<Spell> query2 = await context.Spell.Include(spell => spell.Item).ToListAsync();

            logger.Log(LogLevel.Information, "[GET] get all spell");
            return Ok(query2);
        }
        
        
          [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> getSpell(Guid id)
        {
            var query = await  context.Spell.Include(spell => spell.Item).Where(spell => spell.id == id).SingleOrDefaultAsync();
            
            logger.Log(LogLevel.Information, "[GET] get id:\t" + id.ToString());
            return Ok(query);
        }


        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] SpellFilter filter)
        {
            HashSet<Spell> spells = new HashSet<Spell>();
            if (filter.id != null)
            {
                //find by id
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.id == filter.id).SingleOrDefaultAsync(); 
                spells.Add(query);
            }

            if (filter.name != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.Item.name.ToUpper().Contains(filter.name.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            if (filter.description != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.Item.description.ToUpper().Contains(filter.description.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            if (filter.source != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.Item.source.ToUpper().Contains(filter.source.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }


            if (filter.price != null)
            {
                if (filter.priceOp == '=')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price == filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else if (filter.priceOp == '>')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price > filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else if (filter.priceOp == '<')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price < filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price == filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
            }

            // TODO range

            if (filter.range != null)
            {
                if (filter.rangeOp == '=')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price == filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else if (filter.rangeOp == '>')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price > filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else if (filter.rangeOp == '<')
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price < filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
                else
                {
                    var query = await context.Spell.Include(spell => spell.Item)
                        .Where(spell => spell.Item.price == filter.price)
                        .ToListAsync();
                
                    spells.UnionWith(query);
                }
            }


            // TODO target

            if (filter.target != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.Item.source.ToUpper().Contains(filter.source.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO duration

            if (filter.duration != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.duration.ToUpper().Contains(filter.duration.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO savingThrown

            if (filter.savingThrow != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.savingThrow.ToUpper().Contains(filter.savingThrow.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO spellresistence

            if (filter.spellResistence != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.spellResistence == filter.spellResistence)
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO casting

            if (filter.casting != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.casting.ToUpper().Contains(filter.casting.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO component

            if (filter.component != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.component.ToUpper().Contains(filter.component.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO school

            if (filter.school != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.school.ToUpper().Contains(filter.school.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }

            // TODO level

            if (filter.level != null)
            {
                var query = await context.Spell.Include(spell => spell.Item)
                    .Where(spell => spell.level.ToUpper().Contains(filter.level.ToUpper()))
                    .ToListAsync();
                
                spells.UnionWith(query);
            }


            logger.Log(LogLevel.Information, "[GET] find spell");
            if (spells.Count == 0)
            {
                return NotFound();
            }

            return Ok(spells);
        }


        [HttpPost]
        public async Task<ActionResult<ISpell>> createSpell([FromBody] Spell spell)
        {
            spell.id = Guid.NewGuid();
            spell.Item.id = Guid.NewGuid();
            spell.ItemId = spell.Item.id;
            var res =this.context.Spell.Add(spell).Entity;
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[POST] post spell");
            return Ok(res);
           
        }

        [HttpPatch]
        public async Task<ActionResult<ISpell>> updateSpell([FromBody] Spell spell)
        {
            if (this.context.Item.Any(item => item.id == spell.id))
            {
                spell.id = Guid.NewGuid();
                spell.Item.id = Guid.NewGuid();
                spell.ItemId = spell.Item.id;
                this.context.Spell.Add(spell);
            }
            else
            {
                this.context.Spell.Update(spell);
            }

            logger.Log(LogLevel.Information, "[PATCH] update spell");
            await this.context.SaveChangesAsync();
            return Ok(spell);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
            var res = this.context.Spell.Remove(new Spell(id)).Entity;
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[DELETE] spell spell");
            return Ok(res);
        }
        
        

    }
}