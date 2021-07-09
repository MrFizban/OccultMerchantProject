using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondaryLocation.Items;
using SecondaryLocation.Reposotory;

namespace SecondaryLocation.Controllers
{
    [ApiController]
    [Route("spell")]
    [EnableCors("MyPolicy")]
    public class SpellController : ControllerBase
    {
        private ISpellRepository spellRepository;
        private readonly ILogger<ItemController> logger;
        private readonly ApplicationDbContext context;

        public SpellController(ISpellRepository spellRepository, ILogger<ItemController> logger, ApplicationDbContext context )
        {
            this.context = context;
            this.spellRepository = spellRepository;
            this.logger = logger;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spell>>> getAllSpell()
        {
            var query = (from Item in this.context.Item
                join Spell in this.context.Spell on Item.id equals Spell.id
                select new {Item,Spell}).ToList();
            Console.WriteLine(query.ToString());
            List<Spell> res = new List<Spell>();
            foreach (var VARIABLE in query)
            {
                res.Add(new Spell(VARIABLE.Item,VARIABLE.Spell));
            }
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> getSpell(Guid id)
        {
            var query = (from Item in this.context.Item
                join Spell in this.context.Spell on Item.id equals Spell.id
                
                select new {Item,Spell}).SingleOrDefault();
            
           
           Spell res =  new Spell(query.Item,query.Spell);
           Console.WriteLine(res);
           if (res == null)
           {
               return NotFound();
           }
           return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<ISpell>> createSpell([FromBody] Spell spell)
        {
            spell.id = Guid.NewGuid();

            await this.spellRepository.addSpell(spell);
            return  new ObjectResult(spell){StatusCode = 201};
        }
        
        [HttpPatch]
        public async Task<ActionResult<ISpell>> updateSpell([FromBody]Spell spell)
        {
            await this.spellRepository.updateSpell(spell);
            return Ok(spell);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
           
            await this.spellRepository.deleteSpell(id);
            return Ok(id.ToString());
        }
        
    }
}