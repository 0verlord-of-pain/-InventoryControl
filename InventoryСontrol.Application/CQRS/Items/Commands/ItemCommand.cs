using System;
using System.Threading.Tasks;
using AutoMapper;
using InventoryСontrol.Application.CQRS.Items.Views;
using InventoryСontrol.Application.Extensions;
using InventoryСontrol.Domain;
using InventoryСontrol.Storage.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryСontrol.Application.CQRS.Items.Commands
{
    public sealed class ItemCommand : IItemCommand
    {
        private static InventoryСontrolContext _context;
        private static IMapper _mapper;

        public ItemCommand(
            InventoryСontrolContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemView> UpdateAsync(
           Guid itemId,
           string name,
           int? amount,
           int? cost)
        {
            if (!await _context.TryGetAsync(itemId))
            {
                throw new ArgumentNullException("Предмет не найден");
            }

            var result = await _context.ItemCategories
                .Include(i => i.Item)
                .Include(i => i.Category)
                .FirstOrDefaultAsync();

            var item = result.Item;

            if (!string.IsNullOrWhiteSpace(name))
            {
                item.Name = name;
            }

            if (amount.HasValue)
            {
                if (amount <= 0)
                {
                    throw new ArgumentException("Количество предмета должно быть больше 0");
                }

                item.Amount = (int)amount;
            }

            if (cost.HasValue)
            {
                if (cost <= 0)
                {
                    throw new ArgumentException("Цена предмета должно быть больше 0");
                }

                item.Cost = (int)cost;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<ItemView>(item);
        }

        public async Task<ItemView> AddItemAsync(
            string name,
            int amount,
            int cost)
        {
            if (await _context.TryGetAsync(name))
            {
                throw new ArgumentException("Такой предмет уже существует");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Количество предмета должно быть больше 0");
            }
            if (cost <= 0)
            {
                throw new ArgumentException("Цена предмета должно быть больше 0");
            }

            _context.Items.Add(Item.Create(name, cost, amount));

            await _context.SaveChangesAsync();

            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Name.Equals(name));

            return _mapper.Map<ItemView>(item);
        }

        public async Task BuyItemsAsync(
            Guid itemId,
            int amount)
        {
            if (!await _context.TryGetAsync(itemId))
            {
                throw new ArgumentNullException("Предмет не найден");
            }

            var item = await _context.Items.FindAsync(itemId);

            if (item.Amount < amount) throw new ArgumentException("На складе нет такого количества товара");

            item.UpdateAmount(item.Amount - amount);
            await _context.SaveChangesAsync();
        }

        public async Task PreOrderAsync(
            string UserId,
            Guid itemId,
            int amount)
        {
            if (!await _context.TryGetAsync(itemId))
            {
                throw new ArgumentNullException("Предмет не найден");
            }

            var item = await _context.Items.FindAsync(itemId);

            if (item.Amount >= amount) throw new ArgumentException("Предмет есть на складке, оформить предзаказ не возможно");

            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id.Equals(UserId));

            var preOrder = PreOrder.Create(item, user, amount);

            _context.PreOrders.Add(preOrder);

            await _context.SaveChangesAsync();
        }
    }
}