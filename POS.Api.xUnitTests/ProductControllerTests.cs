using Moq;
using POS.Api.Controllers;
using POS.Core.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using POS.Core.Models.ProductDTOs;
using POS.Core.Models;

namespace POS.Api.xUnitTests
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController _sut;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _sut = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Get_ProductsExist_ReturnsOkWithProducts()
        {
            // Arrange
            var products = new List<ProductDto>
    {
        new ProductDto { ProductId = 1, Name = "Product 1" },
        new ProductDto { ProductId = 2, Name = "Product 2" }
    };

            _productServiceMock.Setup(s => s.GetAllProductsAsync())
                               .ReturnsAsync(products);

            // Act
            Func<Task<ActionResult<List<ProductDto>>>> act = async () => await _sut.Get();
            var result = await act();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<List<ProductDto>>()
                .Which.Should().HaveCount(2);
        }

        [Fact]
        public async Task Get_NoProductsExist_ReturnsNotFound()
        {
            // Arrange
            _productServiceMock.Setup(s => s.GetAllProductsAsync())
                               .ReturnsAsync(new List<ProductDto>()); // Empty list

            // Act
            Func<Task<ActionResult<List<ProductDto>>>> act = async () => await _sut.Get();
            var result = await act();

            // Assert
            result.Result.Should().As<NotFoundResult>();
        }

        [Fact]
        public async Task Get_NullProductsReturned_ReturnsNotFound()
        {
            // Arrange
            _productServiceMock.Setup(s => s.GetAllProductsAsync())
                               .ReturnsAsync((List<ProductDto>)null);

            // Act
            Func<Task<ActionResult<List<ProductDto>>>> act = async () => await _sut.Get();
            var result = await act();

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetProductById_ValidId_ReturnsOkWithProduct()
        {
            // Arrange
            int productId = 1;
            var product = new ProductDto { Name = "Test Product", Price = 100 };

            _productServiceMock.Setup(s => s.GetProductAsync(productId))
                               .ReturnsAsync(product);

            // Act
            var result = await _sut.GetProductById(productId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                         .Which.StatusCode.Should().Be(200);

            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeOfType<ProductDto>()
                          .Which.Name.Should().Be("Test Product");

            _productServiceMock.Verify(s => s.GetProductAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetProductById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int productId = 99;
            _productServiceMock.Setup(s => s.GetProductAsync(productId))
                               .ReturnsAsync((ProductDto)null); // Simulating product not found

            // Act
            var result = await _sut.GetProductById(productId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>()
                         .Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var productCreateDto = new ProductCreateDto { Name = "Test Product", Price = 100 };

            _productServiceMock.Setup(s => s.AddProductAsync(productCreateDto, It.IsAny<CancellationToken>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.AddProduct(productCreateDto);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.ActionName.Should().Be(nameof(_sut.AddProduct));

            var responseValue = createdAtActionResult.Value;
            responseValue.Should().NotBeNull();
        }

        [Fact]
        public async Task AddProduct_NullProduct_ReturnsBadRequest()
        {
            // Arrange
            ProductCreateDto product = null;

            // Act
            var result = await _sut.AddProduct(product);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("Product data is required.");
        }

        [Fact]
        public async Task UpdateProduct_ValidProduct_ReturnsOk()
        {
            // Arrange
            var productUpdateDto = new ProductUpdateDto { ProductId = 1, Name = "Updated Product", Price = 150 };

            _productServiceMock.Setup(s => s.UpdateProductAsync(productUpdateDto))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.UpdateProduct(productUpdateDto.ProductId, productUpdateDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_ReturnsOk()
        {
            // Arrange
            int productId = 1;

            _productServiceMock.Setup(s => s.DeleteProductAsync(productId))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.DeleteProduct(productId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }
    }
}