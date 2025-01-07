using Business.Interfaces;
using Moq;

namespace Business.Test.Services;

public class FileService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IFileService _fileService; 

    public FileService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _fileService = _fileServiceMock.Object;
    }

    [Fact]
    public void SaveContentToFile_ShouldReturnTrue()
    {
        // Arrange
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act
        var result = _fileService.SaveContentToFile("");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContentAsString()
    {
        // Arrange
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns("a string");                            

        // Act
        var result = _fileService.GetContentFromFile();

        // Assert
        Assert.Equal("a string", result); 
    }

}
  