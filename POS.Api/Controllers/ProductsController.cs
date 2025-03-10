using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Core.Dtos.ProductDTOs;
using POS.Core.Service;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCreateDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductAsync(id);  // Calls the service method

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);  // Returns HTTP 200 with the product data
        }

        [HttpPost] //EndPoint To Insert Data
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
            {
                return BadRequest("Product data is required.");
            }

            await _productService.AddProductAsync(productCreateDto, cancellationToken:default);
            return CreatedAtAction(nameof(AddProduct), new { message = "Product added successfully" });
        }

        [HttpPut("{id}")] //EndPoint to Update Product
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null || id != productUpdateDto.ProductId)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                await _productService.UpdateProductAsync(productUpdateDto);
                return Ok(new { message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]// Endpoint For Deleting Product by id
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
