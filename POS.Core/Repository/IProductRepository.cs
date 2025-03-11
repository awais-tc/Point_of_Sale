using POS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Repository
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(Product product);
        Task<Product?> GetProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task DeleteProductAsync(Product product);
    }
}
