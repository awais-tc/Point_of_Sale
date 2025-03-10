using Microsoft.EntityFrameworkCore;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Repository.Context;

namespace POS.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly POSDbContext _context;

        public InventoryRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<Inventory?> GetInventoryAsync(int inventoryId)
        {
            return await _context.Inventories.FindAsync(inventoryId);
        }

        public async Task<List<Inventory>> GetInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<List<Inventory>> GetInventoriesByProductIdAsync(int productId)
        {
            return await _context.Inventories.Where(i => i.ProductId == productId).ToListAsync();
        }

        public async Task<bool> DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory == null) return false;

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
