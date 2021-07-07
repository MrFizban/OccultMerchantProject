using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public class SpellRepository : ItemRepository, ISpellRepository
    {
 

        public Task<IEnumerable<ISpell>> getAllSpell()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ISpell>> getSpell(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ISpell>> addSpell(ISpell spell)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ISpell>> updateSpell(ISpell spell)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ISpell>> deleteSpell(ISpell spell)
        {
            throw new NotImplementedException();
        }
    }
}