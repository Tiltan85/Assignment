
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests.Services;

public class ProductService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ProductService _productService;

    public ProductService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _productService = new ProductService(_fileServiceMock.Object, "");
    }

    /*
    public ProductService_Tests()
    {
        _fileServiceMock = new Mock<IFileRepository>();
        _fileService = new FileService(_fileServiceMock.Object);
    }
    */

    [Fact]
    public void AddproductToList_ShouldReturnTrue_WhenProductAddedToList()
    {
        // Arrange
        _fileServiceMock.Setup(x => x.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new FileResult { Succeeded = true});

        //Act
        var result = _productService.AddProductToList(new Product { Id = "1", Name = "Test Product", Price = 0 });

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public void AddproductToList_ShouldReturnFalseWithError_WhenProductNotAddedToList()
    {
        // Arrange
        _fileServiceMock.Setup(x => x.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new FileResult { Succeeded = false, Error = "Failed to save content." });

        //Act
        var result = _productService.AddProductToList(new Product { Id = "1", Name = "Test Product", Price = 0 });

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Failed to save content.", result.Error);
    }
}
