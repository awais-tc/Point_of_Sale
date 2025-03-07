using Microsoft.AspNetCore.Mvc;
using Moq;
using POS.Api.Controllers;
using POS.Core.Models.ProductDTOs;
using POS.Core.Service;

[TestFixture]
public class ProductControllerTests
{
    private Mock<IProductService> _productServiceMock;
    private ProductsController _productController;

    [SetUp]
    public void Setup()
    {
        _productServiceMock = new Mock<IProductService>();
        _productController = new ProductsController(_productServiceMock.Object);
    }
    //[HttpGet] Controller TestCases
    [Test]
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
        var result = await _productController.Get();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.IsInstanceOf<List<ProductDto>>(okResult.Value);
        Assert.That(((List<ProductDto>)okResult.Value).Count, Is.EqualTo(2));

        _productServiceMock.Verify(s => s.GetAllProductsAsync(), Times.Once);
    }

    [Test]
    public async Task Get_NoProductsExist_ReturnsNotFound()
    {
        // Arrange
        _productServiceMock.Setup(s => s.GetAllProductsAsync())
                           .ReturnsAsync(new List<ProductDto>()); // Empty list

        // Act
        var result = await _productController.Get();

        // Assert
        var notFoundResult = result.Result as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));

        _productServiceMock.Verify(s => s.GetAllProductsAsync(), Times.Once);
    }

    [Test]
    public async Task Get_NullProductsReturned_ReturnsNotFound()
    {
        // Arrange
        _productServiceMock.Setup(s => s.GetAllProductsAsync())
                           .ReturnsAsync((List<ProductDto>)null);

        // Act
        var result = await _productController.Get();

        // Assert
        var notFoundResult = result.Result as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));

        _productServiceMock.Verify(s => s.GetAllProductsAsync(), Times.Once);
    }

    //[HttpGet("{id}")] Controller Testcases for getting Product by id.
    [Test]
    public async Task GetProductById_ValidId_ReturnsOkWithProduct()
    {
        // Arrange
        int productId = 1;
        var product = new ProductDto { Name = "Test Product", Price = 100 };

        //_productServiceMock.Setup(s => s.GetProductAsync(productId)).ReturnsAsync(product);
        _productServiceMock.Setup(s => s.GetProductAsync(productId)).ReturnsAsync(product);

        // Act
        var result = await _productController.GetProductById(productId);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.IsInstanceOf<ProductDto>(okResult.Value);
        Assert.That(((ProductDto)okResult.Value).Name, Is.EqualTo("Test Product"));

        _productServiceMock.Verify(s => s.GetProductAsync(productId), Times.Once);
    }

    [Test]
    public async Task GetProductById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        int productId = 99;
        _productServiceMock.Setup(s => s.GetProductAsync(productId))
                   .ReturnsAsync((ProductDto)null);  // Simulating product not found

        // Act
        var result = await _productController.GetProductById(productId);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        Assert.That(notFoundResult.Value, Is.EqualTo($"Product with ID {productId} not found."));

        _productServiceMock.Verify(s => s.GetProductAsync(productId), Times.Once);
    }
    //[HttpPost] EndPoint Testcases to Add Products
    [Test]
    [Ignore("Error to be resolved")]
    public async Task AddProduct_ValidProduct_ReturnsCreatedAtAction()
    {
        // Arrange
        var productCreateDto = new ProductCreateDto { Name = "Test Product", Price = 100 };

        _productServiceMock.Setup(s => s.AddProductAsync(productCreateDto))
                           .Returns(Task.CompletedTask);

        // Act
        var result = await _productController.AddProduct(productCreateDto);

        // Assert
        var createdAtActionResult = result as CreatedAtActionResult;
        Assert.IsNotNull(createdAtActionResult);
        Assert.That(createdAtActionResult.StatusCode, Is.EqualTo(201));
        Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(_productController.AddProduct)));
        Assert.That(createdAtActionResult.RouteValues["message"], Is.EqualTo("Product added successfully."));

        var responseValue = createdAtActionResult.Value as dynamic;
        Assert.IsNotNull(responseValue);
        Assert.That(responseValue.message, Is.EqualTo("Product added successfully"));

        _productServiceMock.Verify(s => s.AddProductAsync(productCreateDto), Times.Once);
    }
    
    [Test]
    public async Task AddProduct_NullProduct_ReturnsBadRequest()
    {
        // Act
        var result = await _productController.AddProduct(null);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        Assert.That(badRequestResult.Value, Is.EqualTo("Product data is required."));

        _productServiceMock.Verify(s => s.AddProductAsync(It.IsAny<ProductCreateDto>()), Times.Never);
    }
}
