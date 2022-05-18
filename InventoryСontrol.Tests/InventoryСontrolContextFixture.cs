using InventoryСontrol.Domain;
using InventoryСontrol.Storage.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryСontrol.Tests
{
    public class InventoryСontrolContextFixture : IDisposable
    {
        public readonly InventoryСontrolContext context;

        public InventoryСontrolContextFixture()
        {
            var time = DateTime.UtcNow.Millisecond.ToString();
            var options = new DbContextOptionsBuilder<InventoryСontrolContext>()
                .UseInMemoryDatabase($"InventoryСontrol{time}")
                .Options;

            context = new InventoryСontrolContext(options);
        }

        public async Task InitUsersAsync(IEnumerable<User> users)
        {
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        public async Task InitRolesAsync(IEnumerable<IdentityRole> roles)
        {
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        public async Task InitItemsAsync(IEnumerable<Item> items)
        {
            context.Items.AddRange(items);
            await context.SaveChangesAsync();
        }

        public async Task InitCategoresAsync(IEnumerable<Category> categores)
        {
            context.Categories.AddRange(categores);
            await context.SaveChangesAsync();
        }

        public async Task InitItemCategoresAsync(IEnumerable<ItemCategory> itemCategory)
        {
            context.ItemCategories.AddRange(itemCategory);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}