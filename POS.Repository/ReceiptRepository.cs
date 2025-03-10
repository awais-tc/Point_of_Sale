using Microsoft.EntityFrameworkCore;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Repository.Context;

namespace POS.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly POSDbContext _context;

        public ReceiptRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<Receipt> CreateReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<Receipt?> GetReceiptBySaleIdAsync(int saleId)
        {
            return await _context.Receipts
                .Include(r => r.Sale)
                .FirstOrDefaultAsync(r => r.SaleId == saleId);
        }

        public async Task<List<Receipt>> GetAllReceiptsAsync()
        {
            return await _context.Receipts
                .Include(r => r.Sale)
                .ToListAsync();
        }
    }

}
