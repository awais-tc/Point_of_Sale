using Microsoft.EntityFrameworkCore;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Repository.Context;

namespace POS.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly POSDbContext _context;

        public ProductRepository(POSDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

}
