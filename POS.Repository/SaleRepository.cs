using Microsoft.EntityFrameworkCore;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Repository.Context;
using System;

namespace POS.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly POSDbContext _context;

        public SaleRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale?> GetSaleAsync(int saleId)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.SaleId == saleId);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ToListAsync();
        }
    }
}
