using POS.Core.Models;

namespace POS.Core.Repository
{
    public interface ISaleItemRepository
    {
        Task<SaleItem?> GetSaleItemAsync(int saleItemId);
        Task<List<SaleItem>> GetAllSaleItemsAsync();
        Task<List<SaleItem>> GetSaleItemsBySaleIdAsync(int saleId);
        Task AddSaleItemAsync(SaleItem saleItem);
        Task AddSaleItemsAsync(IEnumerable<SaleItem> saleItems);
        Task UpdateSaleItemAsync(SaleItem saleItem);
        Task<bool> DeleteSaleItemAsync(int saleItemId);
    }
}
