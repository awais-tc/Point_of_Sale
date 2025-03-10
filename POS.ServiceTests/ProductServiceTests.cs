using Moq;
using AutoMapper;
using POS.Core.Dtos.ProductDTOs;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Service;
using POS.Core.AutoMapper;
using FluentAssertions;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _productRepositoryMock;
    private IMapper _mapper;
    private ProductService _sut;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = config.CreateMapper();

        _productRepositoryMock = new Mock<IProductRepository>();

        _sut = new ProductService(_productRepositoryMock.Object, _mapper);
    }

    [Test]
    public async Task AddProductAsync_should_add_product()
    {
        // Arrange
        var productCreateDto = new ProductCreateDto
        {
            Name = "Test Product",
            Price = 100
        };

        var expectedResult = new Product
        {
            Name = "Test Product",
            Price = 100
        };

        _productRepositoryMock.Setup(r => r.AddProductAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        Func<Task> act = () => _sut.AddProductAsync(productCreateDto, CancellationToken.None);
        
        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task AddProductAsync_NullProductDto_ShouldThrowNullException()
    {
        // Arrange
        ProductCreateDto productCreateDto = null;

        // Act
        Func<Task> act = () => _sut.AddProductAsync(productCreateDto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task UpdateProductAsync_ValidProductDto_ShouldUpdateProductSuccessfully()
    {
        // Arrange
        var expectedresult = new ProductUpdateDto
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

        _productRepositoryMock.Setup(r => r.GetProductAsync(expectedresult.ProductId))
                              .ReturnsAsync(existingProduct);

        _mapper.Map(expectedresult, existingProduct);

        _productRepositoryMock.Setup(r => r.UpdateProductAsync(existingProduct))
                              .Returns(Task.CompletedTask);


        // Act
        Func<Task> act = () => _sut.UpdateProductAsync(expectedresult);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateProductAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        var product_to_be_updated = new ProductUpdateDto
        {
            ProductId = 99,
            Name = "Non-existent Product",
            Price = 200
        };

        Product existingProduct = null;  

        _productRepositoryMock.Setup(r => r.GetProductAsync(product_to_be_updated.ProductId))
                              .ReturnsAsync(existingProduct); 

        // Act
        Func<Task> act = () => _sut.UpdateProductAsync(product_to_be_updated);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task GetProductAsync_ValidProduct_ReturnsProductDto()
    {
        // Arrange
        int productId = 1; // Existing product ID

        var existingProduct = new Product
        {
            ProductId = productId,
            Name = "Test Product",
            Price = 100
        };

        var expectedProductDto = new ProductDto
        {
            ProductId = productId,
            Name = "Test Product",
            Price = 100
        };

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync(existingProduct);

        _mapper.Map<ProductDto>(existingProduct);

        // Act
        var result = await _sut.GetProductAsync(productId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedProductDto);
    }

    [Test]
    public async Task GetProductAsync_ProductNotFound_ShouldThrowException()
    {
        // Arrange
        int productId = 99;

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync((Product)null);

        // Act
        Func<Task> act = async () => await _sut.GetProductAsync(productId);

        // Assert
        await act.Should().ThrowAsync<Exception>()
                 .WithMessage("Product not found");
    }



    [Test]
    public async Task GetProductAsync_InvalidProductId_ShouldThrowArgumentException()
    {
        //Arrange
        int productId = 0; // Invalid product ID
        int negativeProductId = -1; // Negative product ID

        // Act
        Func<Task> act1 = async () => await _sut.GetProductAsync(productId); // Assuming 0 is invalid
        Func<Task> act2 = async () => await _sut.GetProductAsync(negativeProductId); // Negative ID should fail

        // Assert
        await act1.Should().ThrowAsync<ArgumentException>();
        await act2.Should().ThrowAsync<ArgumentException>();         
    }

    [Test]
    public async Task GetAllProductsAsync_ProductsExist_ReturnsListOfProductDtos()
    {
        // Arrange
        var products = new List<Product>
    {
        new Product { ProductId = 1, Name = "Product A", Price = 100 },
        new Product { ProductId = 2, Name = "Product B", Price = 200 }
    };

        var expectedProductDtos = new List<ProductDto>
    {
        new ProductDto { ProductId = 1, Name = "Product A", Price = 100 },
        new ProductDto { ProductId = 2, Name = "Product B", Price = 200 }
    };

        _productRepositoryMock.Setup(r => r.GetAllProductsAsync())
                              .ReturnsAsync(products);

        _mapper.Map<List<ProductDto>>(products);

        // Act
        var result = await _sut.GetAllProductsAsync();

        // Assert
        result.Should().NotBeNull()
              .And.HaveCount(2)
              .And.BeEquivalentTo(expectedProductDtos);
    }


    [Test]
    public async Task GetAllProductsAsync_NoProductsExist_ReturnsEmptyList()
    {
        // Arrange
        var products = new List<Product>(); // Empty list
        var expectedProductDtos = new List<ProductDto>(); // Empty mapped list

        _productRepositoryMock.Setup(r => r.GetAllProductsAsync())
                              .ReturnsAsync(products);

        _mapper.Map<List<ProductDto>>(products);

        // Act
        var result = await _sut.GetAllProductsAsync();

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty(); // Ensures the returned list is empty
    }

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
        Func<Task> act = async () => await _sut.DeleteProductAsync(productId);

        // Assert
        await act.Should().NotThrowAsync(); // Ensure method executes without exception
    }


    [Test]
    public async Task DeleteProductAsync_ProductNotFound_ThrowsException()
    {
        // Arrange
        int productId = 99;

        _productRepositoryMock.Setup(r => r.GetProductAsync(productId))
                              .ReturnsAsync((Product)null); // Simulating product not found

        // Act
        Func<Task> act = async () => await _sut.DeleteProductAsync(productId);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
