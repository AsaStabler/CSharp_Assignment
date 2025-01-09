using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using Moq;
using System.Text.Json;

namespace Business.Test.Repositories;

public class ContactRepository_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactRepository _contactRepository;

    public ContactRepository_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactRepository = new ContactRepository(_fileServiceMock.Object);
    }

    [Fact]
    public void SaveContacts_ShouldReturnTrue_AndSaveListToFile()
    {
        // Arrange
        List<Contact> contacts = [new Contact { Id = "1f25dbab-4040-42a7-b405-cc69bf545fed", FirstName = "John", LastName = "Doe",
                                                Email = "john.doe@domain.com", Phone = "076-234 5678",
                                                StreetAddress = "Doe Street 1", PostalCode = "123 45", City = "Doe City" }];

        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act
        var result = _contactRepository.SaveContacts(contacts);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void GetContacts_ShouldReturnListOfContacts()
    {
        // Arrange
        List<Contact> expected = [new Contact { Id = "1f25dbab-4040-42a7-b405-cc69bf545fed", FirstName = "John", LastName = "Doe",
                                                Email = "john.doe@domain.com", Phone = "076-234 5678",
                                                StreetAddress = "Doe Street 1", PostalCode = "123 45", City = "Doe City" }];

        var json = JsonSerializer.Serialize(expected);
        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(json);

        // Act
        var result = _contactRepository.GetContacts();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expected[0].Id, result.First().Id);
        Assert.Equal(expected[0].FirstName, result.First().FirstName);
        Assert.Equal(expected[0].LastName, result.First().LastName);
        Assert.Equal(expected[0].Email, result.First().Email);
        Assert.Equal(expected[0].Phone, result.First().Phone);
        Assert.Equal(expected[0].StreetAddress, result.First().StreetAddress);
        Assert.Equal(expected[0].PostalCode, result.First().PostalCode);
        Assert.Equal(expected[0].City, result.First().City);
    }
}
