using System;
using System.Collections.Generic;
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

        public SpellController(ISpellRepository spellRepository, ILogger<ItemController> logger)
        {
            this.spellRepository = spellRepository;
            this.logger = logger;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spell>>> getAllSpell()
        {
            return Ok(this.spellRepository.getAllSpell());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> getSpell(Guid id)
        {
            var result = await this.spellRepository.getSpell(id);
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