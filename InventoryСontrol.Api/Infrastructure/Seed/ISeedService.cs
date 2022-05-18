using System.Threading.Tasks;

namespace InventoryСontrol.Api.Infrastructure.Seed
{
    internal interface ISeedService
    {
        Task SeedRolesAsync();
        Task SeedAdminAndManagerAsync();
        Task SeedCategoriesAsync();
        Task SeedItemsAsync();
        Task SeedAddCategoryToItemsAsync();
    }
}