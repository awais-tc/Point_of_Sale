using AutoMapper;
using POS.Core.Models.ProductDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Core.Service;

namespace POS.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
                throw new ArgumentNullException(nameof(productCreateDto));

            var product = _mapper.Map<Product>(productCreateDto);
            await _productRepository.AddProductAsync(product);
        }


        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
                throw new ArgumentNullException(nameof(productUpdateDto));

            var product = await _productRepository.GetProductAsync(productUpdateDto.ProductId);
            if (product == null) throw new Exception("Product not found");

            _mapper.Map(productUpdateDto, product);
            await _productRepository.UpdateProductAsync(product);
        }


        public async Task<ProductDto> GetProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID", nameof(productId));

            var product = await _productRepository.GetProductAsync(productId);
            if (product == null) throw new Exception("Product not found");

            return _mapper.Map<ProductDto>(product);
        }


        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null) throw new Exception("Product not found");

            await _productRepository.DeleteProductAsync(product);
        }
    }
}
