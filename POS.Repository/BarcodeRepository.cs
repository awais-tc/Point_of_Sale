using Microsoft.EntityFrameworkCore;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Repository.Context;

namespace POS.Repository
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly POSDbContext _context;

        public BarcodeRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<Barcode?> GetBarcodeAsync(int barcodeId)
        {
            return await _context.Barcodes.FindAsync(barcodeId);
        }

        public async Task<List<Barcode>> GetBarcodesAsync()
        {
            return await _context.Barcodes.ToListAsync();
        }

        public async Task<Barcode?> GetBarcodeByProductIdAsync(int productId)
        {
            return await _context.Barcodes.FirstOrDefaultAsync(b => b.ProductId == productId);
        }

        public async Task AddBarcodeAsync(Barcode barcode)
        {
            await _context.Barcodes.AddAsync(barcode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBarcodeAsync(Barcode barcode)
        {
            _context.Barcodes.Update(barcode);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBarcodeAsync(int barcodeId)
        {
            var barcode = await _context.Barcodes.FindAsync(barcodeId);
            if (barcode == null) return false;

            _context.Barcodes.Remove(barcode);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
