using POS.Core.Models.BarcodeDTOs;

namespace POS.Core.Service
{
    public interface IBarcodeService
    {
        Task AddBarcodeAsync(BarcodeCreateDto barcodeCreateDto);
        Task<BarcodeDto> GetBarcodeAsync(int barcodeId);
        Task<IEnumerable<BarcodeDto>> GetBarcodesAsync();
        Task UpdateBarcodeAsync(BarcodeUpdateDto barcodeUpdateDto);
        Task<bool> DeleteBarcodeAsync(int barcodeId);
        Task<BarcodeDto> GetBarcodeByProductIdAsync(int productId);
    }

}
