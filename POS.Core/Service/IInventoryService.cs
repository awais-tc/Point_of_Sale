using POS.Core.Models.InventoryDTOs;
namespace POS.Core.Service
{
    public interface IInventoryService
    {
        Task AddInventoryAsync(InventoryCreateDto inventoryCreateDto);
        Task UpdateInventoryAsync(InventoryUpdateDto inventoryUpdateDto);
        Task<InventoryDto> GetInventoryAsync(int inventoryId);
        Task<List<InventoryDto>> GetInventoriesAsync();
        Task<List<InventoryDto>> GetInventoriesByProductIdAsync(int productId);
        Task<bool> DeleteInventoryAsync(int inventoryId);
    }
}
