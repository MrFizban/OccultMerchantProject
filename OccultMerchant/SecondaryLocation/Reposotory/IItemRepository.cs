using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondaryLocation.Items;

namespace SecondaryLocation.Reposotory
{
    public interface IItemRepository
    {
        Task<IEnumerable<IItem>> getAllItem();
        Task<IItem> getItem(Guid id);
        Task<ActionResult<IItem>> addItem(IItem item);
        Task<ActionResult<IItem>> updateItem(IItem item);
        Task<ActionResult<IItem>> deleteItem(IItem item);
    }
}