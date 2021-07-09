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
        Task<ISpell> getSpell(Guid id);
        Task<ISpell> addSpell(ISpell spell);
        Task<ISpell> updateSpell(ISpell spell);
        Task<bool> deleteSpell(Guid id);
        Task<IEnumerable<ISpell>> getAllSpellContext();
    }
}