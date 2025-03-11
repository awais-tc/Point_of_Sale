using POS.Core.Models.SaleItemDTOs;

namespace POS.Core.Service
{
    public interface ISaleItemService
    {
        Task AddSaleItemAsync(SaleItemCreateDto saleItemDto);
        Task<SaleItemDto> GetSaleItemAsync(int saleItemId);
        Task<IEnumerable<SaleItemDto>> GetSaleItemsAsync();
        Task UpdateSaleItemAsync(SaleItemUpdateDto saleItemDto);
        Task<bool> DeleteSaleItemAsync(int saleItemId);
    }

}
