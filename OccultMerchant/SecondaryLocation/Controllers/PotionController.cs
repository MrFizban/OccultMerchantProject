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
    [Route("potion")]
    [EnableCors("MyPolicy")]
    public class PotionController : ControllerBase
    {
        private IPotionReposotory potionReposotory;
        private readonly ILogger<ItemController> logger;

        public PotionController(IPotionReposotory potionReposotory, ILogger<ItemController> logger)
        {
            this.potionReposotory = potionReposotory;
            this.logger = logger;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IPotion>>> getAllSpell()
        {
            return Ok(this.potionReposotory.getAllPotion());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IPotion>> getSpell(Guid id)
        {
            var result = await this.potionReposotory.getPotion(id);
            if (result == null)
            {
                return NotFound();
            }
            
            Console.WriteLine("found: \t" + result);
            return Ok(result);
            
        }
        
        

        [HttpPost]
        public async Task<ActionResult<IPotion>> createSpell([FromBody] Potion potion)
        {
            potion.id = Guid.NewGuid();

            if ((await this.potionReposotory.addPotion(potion)).name == "FOREIGN KEY constraint failed")
            {
                return new ObjectResult(new {error = "Id dell oggeto spell invalido",obj = potion}) {StatusCode = 500};
            }
            return  new ObjectResult(potion){StatusCode = 201};
        }
        
        [HttpPatch]
        public async Task<ActionResult<IPotion>> updateSpell([FromBody]Potion potion)
        {
            await this.potionReposotory.updatePotion(potion);
            return Ok(potion);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> deleteSpell(Guid id)
        {
           
            await this.potionReposotory.deletePotion(id);
            return Ok(id.ToString());
        }
        
    }
}