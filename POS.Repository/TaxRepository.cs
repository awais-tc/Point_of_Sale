using Microsoft.EntityFrameworkCore;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Repository.Context;

namespace POS.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly POSDbContext _context;

        public TaxRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<Tax?> GetTaxByRegionAsync(string region)
        {
            return await _context.Taxes.FirstOrDefaultAsync(t => t.Region == region);
        }

        public async Task<List<Tax>> GetAllTaxesAsync()
        {
            return await _context.Taxes.ToListAsync();
        }

        public async Task AddTaxAsync(Tax tax)
        {
            await _context.Taxes.AddAsync(tax);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaxAsync(Tax tax)
        {
            _context.Taxes.Update(tax);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTaxAsync(int taxId)
        {
            var tax = await _context.Taxes.FindAsync(taxId);
            if (tax == null) return false;

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Tax?> GetTaxAsync(int taxId)
        {
            return await _context.Taxes.FindAsync(taxId);
        }
    }

}
