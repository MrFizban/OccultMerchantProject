using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public interface ISpellRepository
    {
        Task<IEnumerable<ISpell>> getAllSpell();
        Task<ActionResult<ISpell>> getSpell(Guid id);
        Task<ActionResult<ISpell>> addSpell(ISpell spell);
        Task<ActionResult<ISpell>> updateSpell(ISpell spell);
        Task<ActionResult<ISpell>> deleteSpell(ISpell spell);
    }
}