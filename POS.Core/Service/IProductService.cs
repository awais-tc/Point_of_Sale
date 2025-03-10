using POS.Core.Dtos.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Core.Service
{
    public interface IProductService
    {
        Task AddProductAsync(ProductCreateDto productCreateDto, CancellationToken cancellationToken);
        Task UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ProductDto> GetProductAsync(int productId);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task DeleteProductAsync(int productId);
    }

}
