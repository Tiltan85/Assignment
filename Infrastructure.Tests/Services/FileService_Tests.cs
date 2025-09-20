
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests.Services;

public class FileService_Tests
{
    private Mock<IFileRepository> _fileRepositoryMock;
    private IFileService _fileService;

    public FileService_Tests()
    {
        _fileRepositoryMock = new Mock<IFileRepository>();
        _fileService = new FileService(_fileRepositoryMock.Object);
    }

    /*
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
    public void GetContentFromFile_ShouldReturnTrueAndEmptyString_WhenNoFileFound()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);

        // Act
        var result = fileService.GetContentFromFile("");

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("", result.Content);
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
    */


    [Fact]
    public void GetContentFromFile_ShouldReturnTrueAndStringContent_WhenFileExists()
    {
        // Arrange
        _fileRepositoryMock.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
        _fileRepositoryMock.Setup(x => x.GetFileContent(It.IsAny<string>())).Returns("this is content");

        // Act
        var result = _fileService.GetContentFromFile("");

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("this is content", result.Content);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnFalseAndErrorMessage_WhenFileNotExists()
    {
        // Arrange
        _fileRepositoryMock.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);

        // Act
        var result = _fileService.GetContentFromFile("");

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("File not found.", result.Error);
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnTrue_WhenContentIsSaved()
    {
        // Arrange
        _fileRepositoryMock.Setup(x => x.SaveFileContent(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Act
        var result = _fileService.SaveContentToFile("", "");


        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnFalseAndContentAndErrorMessage_WhenContentIsNotSaved()
    {
        // Arrange
        _fileRepositoryMock.Setup(x => x.SaveFileContent(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        // Act
        var result = _fileService.SaveContentToFile("", "");


        // Assert
        Assert.False(result.Succeeded);
        Assert.NotNull(result.Content);
        Assert.Equal("Failed to save content.", result.Error);
    }
    /*
    [Fact]
    public void FileExists_ShouldReturnFalse_WhenFileNotExists()
    {
        var fileServiceMock = new Mock<IFileService>();
        var fileService = fileServiceMock.Object;

        fileServiceMock.Setup(x => x.FileExists(It.IsAny<string>()))
        .Returns(false);

        var result = fileService.FileExists("");


        Assert.False(result);
    }
    */



    /*
    [Fact]
    public void GetContentFromFile_ShouldReturnTrueWithEmptyString_WhenFileNotFound()
    {
        // Arrange
        string path = @"c:\data\testdata.json";
        
        var fileRepositoryMock = new Mock<IFileRepository>();
        fileRepositoryMock.Setup(x => x.FileExists(path)).Returns(true);
        
        //FileService _fileService = new FileService();

        // Act
        var result = _fileService.GetContentFromFile(path);


        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal("File not found.", result.Error);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnTrueWithContent_WhenFileExists()
    {
        // Arrange
        string path = @"c:\data\testdata.json";

        
        var fileServiceMock = new Mock<IFileRepository>();
        fileServiceMock.Setup(x => x.FileExists(path)).Returns(true);
        
        FileService fileService = new FileService();

        // Act
        var result = fileService.GetContentFromFile(path);


        // Assert
        Assert.True(result.Succeeded);
        Assert.NotNull(result.Content);
    }
    */
}
