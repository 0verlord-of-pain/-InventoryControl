using System;
using System.Threading.Tasks;
using InventoryСontrol.Storage.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryСontrol.Application.Extensions
{
    public static class IsItemExists
    {
        public static async Task<bool> TryGetAsync(
            this InventoryСontrolContext context,
            Guid itemId)
        {
            var item = await context.Items.FindAsync(itemId);
            return item != null;
        }

        public static async Task<bool> TryGetAsync(
            this InventoryСontrolContext context,
            string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var item = await context.Items.FirstOrDefaultAsync(i => i.Name.Equals(name));
                return item != null;
            }

            return false;
        }
    }
}