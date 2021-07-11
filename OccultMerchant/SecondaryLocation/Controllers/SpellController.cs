using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondaryLocation.Entities;


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
            var query = (from Item in this.context.Item
                join Spell in this.context.Spell on Item.id equals Spell.id
                select new {Item, Spell}).ToList();
            Console.WriteLine(query.ToString());
            List<Spell> res = new List<Spell>();
            foreach (var VARIABLE in query)
            {
                res.Add(new Spell(VARIABLE.Item, VARIABLE.Spell));
            }

            logger.Log(LogLevel.Information, "[GET] get all spell");
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> getSpell(Guid id)
        {
            var query = (from item in context.Item
                join spell in context.Spell on item.id equals spell.id
                where item.id == id
                select new {item, spell}).SingleOrDefault();


            if (query == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, "[GET] get id:\t" + id.ToString());
            Spell res = new Spell(query.item, query.spell);
            Console.WriteLine(res);

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<ISpell>> createSpell([FromBody] Spell spell)
        {
            spell.id = Guid.NewGuid();
            this.context.Item.Add(new Item(spell));
            this.context.Spell.Add(new SpellWrappper(spell));
            this.context.SaveChanges();
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

            this.context.SaveChanges();
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
            this.context.SaveChanges();
            var item =this.context.Item.Remove(new Item(id)).Entity;
            this.context.SaveChanges();
    
            return Ok(new Spell(item, spell));
        }
    }
}