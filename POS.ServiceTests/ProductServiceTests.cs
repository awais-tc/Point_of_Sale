using Moq;
using AutoMapper;
using POS.Core.Models.ProductDTOs;
using POS.Core.Models;
using POS.Core.Repository;
using POS.Service;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ProductService _productService;

    [SetUp]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
    }
    // 3 TestCases For Add Product Method
    [Test]
    public async Task AddProductAsync_ValidProductDto_CallsRepositoryWithMappedProduct()
    {
        // Arrange
        var productCreateDto = new ProductCreateDto
        {
            Name = "Test Product",
            Price = 100
        };

        var mappedProduct = new Product
        {
            Name = "Test Product",
            Price = 100
        };

        _mapperMock.Setup(m => m.Map<Product>(productCreateDto)).Returns(mappedProduct);
        _productRepositoryMock.Setup(r => r.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

        // Act
        await _productService.AddProductAsync(productCreateDto);

        // Assert
        _mapperMock.Verify(m => m.Map<Product>(productCreateDto), Times.Once);
        _productRepositoryMock.Verify(r => r.AddProductAsync(It.Is<Product>(p => p.Name == "Test Product" && p.Price == 100)), Times.Once);
    }

    [Test]
    public void AddProductAsync_NullProductDto_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _productService.AddProductAsync(null));
    }

    [Test]
    public async Task UpdateProductAsync_ValidProductDto_UpdatesProductSuccessfully()
    {
        // Arrange
        var productUpdateDto = new ProductUpdateDto
        {
            ProductId = 1,
            Name = "Updated Product",
            Price = 150
        };

        var existingProduct = new Product
        {
            ProductId = 1,
            Name = "Old Product",
            Price = 100
        };

        _productRepositoryMock.Setup(r => r.GetProductAsync(productUpdateDto.ProductId))
                              .ReturnsAsync(existingProduct);

        _mapperMock.Setup(m => m.Map(productUpdateDto, existingProduct));

        _productRepositoryMock.Setup(r => r.UpdateProductAsync(existingProduct))
                              .Returns(Task.CompletedTask);

        // Act
        await _productService.UpdateProductAsync(productUpdateDto);

        // Assert
        _productRepositoryMock.Verify(r => r.GetProductAsync(productUpdateDto.ProductId), Times.Once);
        _mapperMock.Verify(m => m.Map(productUpdateDto, existingProduct), Times.Once);
        _productRepositoryMock.Verify(r => r.UpdateProductAsync(existingProduct), Times.Once);
    }
    //2 TestCases for Update Method
    [Test]
    public async Task UpdateProductAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        var productUpdateDto = new ProductUpdateDto
        {
            ProductId = 99,
            Name = "Non-existent Product",
            Price = 200
        };

        _productRepositoryMock.Setup(r => r.GetProductAsync(productUpdateDto.ProductId))
                              .ReturnsAsync((Product)null); // Simulate product not found

        // Act & Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _productService.UpdateProductAsync(productUpdateDto));
        Assert.That(exception.Message, Is.EqualTo("Product not found"));

        _productRepositoryMock.Verify(r => r.GetProductAsync(productUpdateDto.ProductId), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<ProductUpdateDto>(), It.IsAny<Product>()), Times.Never);
        _productRepositoryMock.Verify(r => r.UpdateProductAsync(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public void UpdateProductAsync_NullProductDto_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _productService.UpdateProductAsync(null));
    }

    //3 TestCases for GetProductByID Method..
    [Test]
    public async Task GetProductAsync_ValidProductId_ReturnsProductDto()
    {
        // Arrange
        int productId = 1;
        var product = new Product
        {
            ProductId = productId,
            Name = "Test Product",
            Price = 100
        };

        var productDto = new ProductDto
        {
            ProductId = productId,
            Name = "Test Product",
            Price = 100
        };

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync(product);

        _mapperMock.Setup(m => m.Map<ProductDto>(product))
                   .Returns(productDto);

        // Act
        var result = await _productService.GetProductAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.ProductId, Is.EqualTo(productId));
        Assert.That(result.Name, Is.EqualTo("Test Product"));
        Assert.That(result.Price, Is.EqualTo(100));

        _productRepositoryMock.Verify(r => r.GetProductAsync(productId), Times.Once);
        _mapperMock.Verify(m => m.Map<ProductDto>(product), Times.Once);
    }

    [Test]
    public async Task GetProductAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        int productId = 99; // Non-existent product ID

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync((Product)null); // Simulating product not found

        // Act & Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _productService.GetProductAsync(productId));
        Assert.That(exception.Message, Is.EqualTo("Product not found"));

        _productRepositoryMock.Verify(r => r.GetProductAsync(productId), Times.Once);
        _mapperMock.Verify(m => m.Map<ProductDto>(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public void GetProductAsync_InvalidProductId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _productService.GetProductAsync(0)); // Assuming 0 is invalid
        Assert.ThrowsAsync<ArgumentException>(async () => await _productService.GetProductAsync(-1)); // Negative ID should fail
    }

    //
    [Test]
    public async Task GetAllProductsAsync_ProductsExist_ReturnsListOfProductDtos()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Product A", Price = 100 },
            new Product { ProductId = 2, Name = "Product B", Price = 200 }
        };

        var productDtos = new List<ProductDto>
        {
            new ProductDto { ProductId = 1, Name = "Product A", Price = 100 },
            new ProductDto { ProductId = 2, Name = "Product B", Price = 200 }
        };

        _productRepositoryMock.Setup(r => r.GetAllProductsAsync())
                              .ReturnsAsync(products);

        _mapperMock.Setup(m => m.Map<List<ProductDto>>(products))
                   .Returns(productDtos);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Product A"));
        Assert.That(result[0].Price, Is.EqualTo(100));
        Assert.That(result[1].Name, Is.EqualTo("Product B"));
        Assert.That(result[1].Price, Is.EqualTo(200));

        _productRepositoryMock.Verify(r => r.GetAllProductsAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<ProductDto>>(products), Times.Once);
    }

    [Test]
    public async Task GetAllProductsAsync_NoProductsExist_ReturnsEmptyList()
    {
        // Arrange
        var products = new List<Product>(); // Empty list
        var productDtos = new List<ProductDto>(); // Empty mapped list

        _productRepositoryMock.Setup(r => r.GetAllProductsAsync())
                              .ReturnsAsync(products);

        _mapperMock.Setup(m => m.Map<List<ProductDto>>(products))
                   .Returns(productDtos);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result);

        _productRepositoryMock.Verify(r => r.GetAllProductsAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<ProductDto>>(products), Times.Once);
    }

    //TestCases for DeleteProduct Method
    [Test]
    public async Task DeleteProductAsync_ValidProductId_DeletesProduct()
    {
        // Arrange
        int productId = 1;
        var product = new Product { ProductId = productId, Name = "Test Product" };

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync(product);

        _productRepositoryMock.Setup(r => r.DeleteProductAsync(product))
                              .Returns(Task.CompletedTask);

        // Act
        await _productService.DeleteProductAsync(productId);

        // Assert
        _productRepositoryMock.Verify(r => r.GetProductAsync(productId), Times.Once);
        _productRepositoryMock.Verify(r => r.DeleteProductAsync(product), Times.Once);
    }

    [Test]
    public async Task DeleteProductAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        int productId = 99;
        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync((Product)null); // No product found

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(async () => await _productService.DeleteProductAsync(productId));
        Assert.That(ex.Message, Is.EqualTo("Product not found"));

        _productRepositoryMock.Verify(r => r.GetProductAsync(productId), Times.Once);
        _productRepositoryMock.Verify(r => r.DeleteProductAsync(It.IsAny<Product>()), Times.Never);
    }

}
