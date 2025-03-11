using POS.Core.Models;

namespace POS.Core.Repository
{
    public interface IBarcodeRepository
    {
        Task<Barcode?> GetBarcodeAsync(int barcodeId);
        Task<List<Barcode>> GetBarcodesAsync();
        Task<Barcode?> GetBarcodeByProductIdAsync(int productId);
        Task AddBarcodeAsync(Barcode barcode);
        Task UpdateBarcodeAsync(Barcode barcode);
        Task<bool> DeleteBarcodeAsync(int barcodeId);
    }
}
