
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Moq;

namespace Infrastructure.Tests.Services;

public class FileService_Tests
{
    [Fact]
    public void SaveContentToFile_ShouldReturnTrue_WhenContentSavedToFile()
    {
        // Arrange
        // Ställer in att fileResult är true
        var fileResult = new FileResult { Succeeded = true };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(fileResult);

        // Act
        var result = fileService.SaveContentToFile("", "");

        // Assert
        Assert.True(result.Succeeded );
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnFalseWithError_WhenContentNotSavedToFile()
    {
        // Arrange
        // Ställer in att fileResult är true
        var fileResult = new FileResult { Succeeded = false, Error = "Unable to save content." };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(fileResult);

        // Act
        var result = fileService.SaveContentToFile("", "");

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Unable to save content.", result.Error);

    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContentAsJson_WhenSuccessful()
    {
        // Arrange
        var jsonContent = "[{\"Id\": \"a218c5b1-d1c6-4735-90f5-1f6040b2c2d3\", \"Name\": \"Test Product\", \"Price\": 100.00}]";
        var fileResult = new FileResult { Succeeded = true, Content = jsonContent };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.GetContentFromFile(It.IsAny<string>()))
        .Returns(fileResult);

        // Act
        var result = fileService.GetContentFromFile("");

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal($"{jsonContent}", result.Content);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnFalseEmptyString_WhenNoFileFound()
    {
        // Arrange
        var jsonContent = "[{\"Id\": \"a218c5b1-d1c6-4735-90f5-1f6040b2c2d3\", \"Name\": \"Test Product\", \"Price\": 100.00}]";
        var fileResult = new FileResult { Succeeded = false, Content = jsonContent };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.GetContentFromFile(It.IsAny<string>()))
        .Returns(fileResult);

        // Act
        var result = fileService.GetContentFromFile("");

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal($"{jsonContent}", result.Content);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnFalseWithError_WhenException()
    {
        // Arrange
        var fileResult = new FileResult { Succeeded = false, Error = "Something went wrong!" };

        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.GetContentFromFile(It.IsAny<string>()))
        .Returns(fileResult);

        // Act
        var result = fileService.GetContentFromFile("");

        // Assert
        Assert.False(result.Succeeded);
        Assert.False(string.IsNullOrEmpty(result.Error));
    }
}
