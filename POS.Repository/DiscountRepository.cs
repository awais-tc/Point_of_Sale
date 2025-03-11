using Microsoft.EntityFrameworkCore;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly POSDbContext _context;

        public DiscountRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task<Discount?> GetDiscountAsync(int discountId)
        {
            return await _context.Discounts.FindAsync(discountId);
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetDiscountByCodeAsync(string code)
        {
            return await _context.Discounts.FirstOrDefaultAsync(d => d.Code == code);
        }

        public async Task AddDiscountAsync(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiscountAsync(Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteDiscountAsync(int discountId)
        {
            var discount = await _context.Discounts.FindAsync(discountId);
            if (discount == null) return false;

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
