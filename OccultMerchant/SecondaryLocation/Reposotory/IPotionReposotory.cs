using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public interface IPotionReposotory
    {
        Task<IEnumerable<IPotion>> getAllPotion();
        Task<IPotion> getPotion(Guid id);
        Task<IPotion> addPotion(IPotion potion);
        Task<IPotion> updatePotion(IPotion spell);
        Task<bool> deletePotion(Guid id);
    }
}