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
            var query = await (from Item in this.context.Item
                join Potion in this.context.Potion on Item.id equals Potion.id
                select new {Item, Potion}).ToListAsync();


            // lista pozioni
            List<Potion> res = new List<Potion>();
            foreach (var VARIABLE in query)
            {
                res.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
            }

            logger.Log(LogLevel.Information, "[GET] get all potions");
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IPotion>> getSpell(Guid id)
        {
            var query = await (from item in context.Item
                join potion in context.Potion on item.id equals potion.id
                where item.id == id
                select new {item, potion}).SingleOrDefaultAsync();


            if (query == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, "[GET] get id:\t" + id.ToString());
            Potion res = new Potion(query.item, query.potion);
            Console.WriteLine(res);

            return Ok(res);
        }

        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<IItem>>> findItme([FromQuery] PotionFilter filter)
        {
            HashSet<Potion> potions = new HashSet<Potion>();
            if (filter.id != null)
            {
                //find by id
                var query = await (from item in context.Item
                    join potion in context.Potion on item.id equals potion.id
                    where item.id == filter.id
                    select new {item, potion}).SingleOrDefaultAsync();


                if (query == null)
                {
                    return NotFound();
                }

                potions.Add(new Potion(query.item, query.potion));
            }

            if (filter.name != null)
            {
                var query = await (from Item in this.context.Item
                    join Potion in this.context.Potion on Item.id equals Potion.id
                    where Item.name.Contains(filter.name)
                    select new {Item, Potion}).ToListAsync();


                // lista pozioni

                foreach (var VARIABLE in query)
                {
                    potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                }
            }

            if (filter.description != null)
            {
                var query = await (from Item in this.context.Item
                    join Potion in this.context.Potion on Item.id equals Potion.id
                    where Item.description.Contains(filter.description)
                    select new {Item, Potion}).ToListAsync();


                // lista pozioni

                foreach (var VARIABLE in query)
                {
                    potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                }
            }

            if (filter.source != null)
            {
                var query = await (from Item in this.context.Item
                    join Potion in this.context.Potion on Item.id equals Potion.id
                    where Item.source.Contains(filter.source)
                    select new {Item, Potion}).ToListAsync();


                // lista pozioni

                foreach (var VARIABLE in query)
                {
                    potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                }
            }


            if (filter.price != null)
            {
                if (filter.priceOp == '=')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Item.price == filter.price
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else if (filter.priceOp == '>')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Item.price > filter.price
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else if (filter.priceOp == '<')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Item.price < filter.price
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Item.price == filter.price
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
            }

            // TODO casterLevell

            if (filter.casterLevell != null)
            {
                var query = await (from Item in this.context.Item
                    join Potion in this.context.Potion on Item.id equals Potion.id
                    where Potion.casterLevell == filter.casterLevell
                    select new {Item, Potion}).ToListAsync();


                // lista pozioni

                foreach (var VARIABLE in query)
                {
                    potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                }
            }

            // TODO wheight

            if (filter.wheight != null)
            {
                if (filter.wheightOp == '=')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Potion.wheight == filter.wheight
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else if (filter.wheightOp == '>')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Potion.wheight > filter.wheight
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else if (filter.wheightOp == '<')
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Potion.wheight < filter.wheight
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }
                else
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Potion.wheight == filter.wheight
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
                }


                // TODO idSpell

                if (filter.idSpell != null)
                {
                    var query = await (from Item in this.context.Item
                        join Potion in this.context.Potion on Item.id equals Potion.id
                        where Potion.idSpell == filter.idSpell
                        select new {Item, Potion}).ToListAsync();


                    // lista pozioni

                    foreach (var VARIABLE in query)
                    {
                        potions.Add(new Potion(VARIABLE.Item, VARIABLE.Potion));
                    }
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
            potion.ItemType = 2;
            if (this.context.Spell.Where(spell => spell.id == potion.id).SingleOrDefault() == null)
            {
                potion.spell.id = Guid.Empty;
            }

            this.context.Item.Add(new Item(potion));
            this.context.Potion.Add(new PotionWrapper(potion));
            await this.context.SaveChangesAsync();
            logger.Log(LogLevel.Information, "[POST] post new spell");

            return Ok(potion);
        }

        [HttpPatch]
        public async Task<ActionResult<IPotion>> updateSpell([FromBody] Potion potion)
        {
            if (!this.context.Item.Any(item => item.id == potion.id))
            {
                potion.id = Guid.NewGuid();
                if (!this.context.Spell.Any(spell => spell.id == potion.id))
                {
                    potion.spell.id = Guid.Empty;
                }

                logger.Log(LogLevel.Information, "[PATCH] post new spell");
                this.context.Item.Add(new Item(potion));
                this.context.Potion.Add(new PotionWrapper(potion));
            }
            else
            {
                logger.Log(LogLevel.Information, "[PATCH] udate spell");
                this.context.Item.Update(new Item(potion));
                this.context.Potion.Update(new PotionWrapper(potion));
            }

            await this.context.SaveChangesAsync();
            return Ok(potion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
            /*
            context.Database.ExecuteSqlRaw($"DELETE FROM Item WHERE id='{id.ToString()}'");
            context.Database.ExecuteSqlRaw($"DELETE FROM Potion WHERE id='{id.ToString()}'");
            this.context.SaveChanges();
            logger.Log(LogLevel.Information, "[DELETE] delete spell");
            return Ok(id.ToString());
            */

            var potion = this.context.Potion.Remove(new PotionWrapper(id)).Entity;
            await this.context.SaveChangesAsync();
            var item = this.context.Item.Remove(new Item(id)).Entity;
            await this.context.SaveChangesAsync();

            return Ok(new Potion(item, potion));
        }
    }
}