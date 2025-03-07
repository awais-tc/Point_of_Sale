using POS.Core.Models;

namespace POS.Core.Repository
{
    public interface IInventoryRepository
    {
        Task AddInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task<Inventory?> GetInventoryAsync(int inventoryId);
        Task<List<Inventory>> GetInventoriesAsync();
        Task<List<Inventory>> GetInventoriesByProductIdAsync(int productId);
        Task<bool> DeleteInventoryAsync(int inventoryId);
    }

}
