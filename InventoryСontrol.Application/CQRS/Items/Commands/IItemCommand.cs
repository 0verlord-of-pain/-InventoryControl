using System;
using System.Threading.Tasks;
using InventoryСontrol.Application.CQRS.Items.Views;

namespace InventoryСontrol.Application.CQRS.Items.Commands
{
    public interface IItemCommand
    {
        public Task<ItemView> UpdateAsync(
            Guid itemId,
            string name,
            int? amount,
            int? coast);
        public Task<ItemView> AddItemAsync(
            string name,
            int amount,
            int cost);
        public Task BuyItemsAsync(
            Guid itemId,
            int amount);

        public Task PreOrderAsync(
            string UserId,
            Guid itemId,
            int amount);
    }
}