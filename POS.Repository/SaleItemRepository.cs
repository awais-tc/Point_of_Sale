using Microsoft.EntityFrameworkCore;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Repository.Context;


namespace POS.Repository
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly POSDbContext _context;

        public SaleItemRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<SaleItem?> GetSaleItemAsync(int saleItemId)
        {
            return await _context.SaleItems.FindAsync(saleItemId);
        }

        public async Task<List<SaleItem>> GetAllSaleItemsAsync()
        {
            return await _context.SaleItems.ToListAsync();
        }

        public async Task<List<SaleItem>> GetSaleItemsBySaleIdAsync(int saleId)
        {
            return await _context.SaleItems.Where(s => s.SaleId == saleId).ToListAsync();
        }

        public async Task AddSaleItemAsync(SaleItem saleItem)
        {
            await _context.SaleItems.AddAsync(saleItem);
            await _context.SaveChangesAsync();
        }

        public async Task AddSaleItemsAsync(IEnumerable<SaleItem> saleItems)
        {
            await _context.SaleItems.AddRangeAsync(saleItems); // Use AddRangeAsync for lists
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleItemAsync(SaleItem saleItem)
        {
            _context.SaleItems.Update(saleItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSaleItemAsync(int saleItemId)
        {
            var saleItem = await _context.SaleItems.FindAsync(saleItemId);
            if (saleItem == null) return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
