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
            var query = await (from Item in this.context.Item
                join Spell in this.context.Spell on Item.id equals Spell.id
                select new {Item, Spell}).ToListAsync();
            Console.WriteLine(query.ToString());
            List<Spell> res = new List<Spell>();
            foreach (var VARIABLE in query)
            {
                res.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                Console.WriteLine(res[res.Count - 1].spellResistence.ToString());
            }

            logger.Log(LogLevel.Information, "[GET] get all spell");
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> getSpell(Guid id)
        {
            var query = await (from item in context.Item
                join spell in context.Spell on item.id equals spell.id
                where item.id == id
                select new {item, spell}).SingleOrDefaultAsync();


            if (query == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, "[GET] get id:\t" + id.ToString());
            Spell res = new Spell(query.item, query.spell);
            Console.WriteLine(res);

            return Ok(res);
        }


        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] SpellFilter filter)
        {
            HashSet<Spell> spells = new HashSet<Spell>();
            if (filter.id != null)
            {
                //find by id
                var query = await (from item in context.Item
                    join spell in context.Spell on item.id equals spell.id
                    where item.id == filter.id
                    select new {item, spell}).SingleOrDefaultAsync();


                if (query == null)
                {
                    return NotFound();
                }

                spells.Add(new Spell(query.item, query.spell));
            }

            if (filter.name != null)
            {
                var query = await (from item in context.Item
                    join spell in context.Spell on item.id equals spell.id
                    where item.name.Contains(filter.name)
                    select new {item, spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.item, VARIABLE.spell));
                }
            }

            if (filter.description != null)
            {
                var query = await (from item in context.Item
                    join spell in context.Spell on item.id equals spell.id
                    where item.description.Contains(filter.description)
                    select new {item, spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.item, VARIABLE.spell));
                }
            }

            if (filter.source != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Item.source.Contains(filter.source)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }


            if (filter.price != null)
            {
                if (filter.priceOp == '=')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price == filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else if (filter.priceOp == '>')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price > filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else if (filter.priceOp == '<')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price < filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price == filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
            }

            // TODO range

            if (filter.range != null)
            {
                if (filter.rangeOp == '=')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price == filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else if (filter.rangeOp == '>')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price > filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else if (filter.rangeOp == '<')
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price < filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
                else
                {
                    var query = await (from Item in this.context.Item
                        join Spell in this.context.Spell on Item.id equals Spell.id
                        where Item.price == filter.price
                        select new {Item, Spell}).ToListAsync();


                    // lista spell

                    foreach (var VARIABLE in query)
                    {
                        spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                    }
                }
            }


            // TODO target

            if (filter.target != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.target.Contains(filter.source)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO duration

            if (filter.duration != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.duration.Contains(filter.duration)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO savingThrown

            if (filter.savingThrow != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.savingThrow.Contains(filter.savingThrow)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO spellresistence

            if (filter.spellResistence != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.spellResistence == filter.spellResistence
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO casting

            if (filter.casting != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.casting == filter.casting
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO component

            if (filter.component != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.component.ToUpper().Contains(filter.component.ToUpper())
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO school

            if (filter.school != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.school.Contains(filter.school)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }

            // TODO level

            if (filter.level != null)
            {
                var query = await (from Item in this.context.Item
                    join Spell in this.context.Spell on Item.id equals Spell.id
                    where Spell.level.Contains(filter.level)
                    select new {Item, Spell}).ToListAsync();


                // lista spell

                foreach (var VARIABLE in query)
                {
                    spells.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
                }
            }


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
            spell.ItemType = 1;
            this.context.Item.Add(new Item(spell));
            this.context.Spell.Add(new SpellWrappper(spell));
            await this.context.SaveChangesAsync();
            logger.Log(LogLevel.Information, "[POST] post new spell");

            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult<ISpell>> updateSpell([FromBody] Spell spell)
        {
            if (!this.context.Item.Any(item => item.id == spell.id))
            {
                this.context.Item.Add(new Item(spell));
                this.context.Spell.Add(new SpellWrappper(spell));
            }
            else
            {
                this.context.Item.Update(new Item(spell));
                this.context.Spell.Update(new SpellWrappper(spell));
            }

            await this.context.SaveChangesAsync();
            return Ok(spell);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
            /*
            context.Database.ExecuteSqlRaw($"DELETE FROM Spell WHERE id='{id.ToString()}'");
            context.Database.ExecuteSqlRaw($"DELETE FROM Item WHERE id='{id.ToString()}'");
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[DELETE] delete spell");
            
            return Ok(id.ToString());
            */
            var spell = this.context.Spell.Remove(new SpellWrappper(id)).Entity;
            await this.context.SaveChangesAsync();
            var item = this.context.Item.Remove(new Item(id)).Entity;
            await this.context.SaveChangesAsync();

            return Ok(new Spell(item, spell));
        }
    }
}