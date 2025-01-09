using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Test.Services;

public class ContactService_Tests
{
    private readonly IContactService _contactService;
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    
    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);
    }

    [Fact]
    public void CreateContact_ShouldReturnTrue_AndSaveToListAndSaveToFile()
    {
        // Arrange
        var contactRegistrationForm = new ContactRegistrationForm() 
        { 
            FirstName = "Donald", 
            LastName = "Duck",
            Email = "donald.duck@domain.com", 
            Phone = "001-234 56 78",
            StreetAddress = "Duck Street 123",
            PostalCode = "123 45",
            City = "Disney Town"
        };

        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>())).Returns(true);

        // Act
        var result = _contactService.CreateContact(contactRegistrationForm);

        // Assert
        Assert.True(result);
     
        _contactRepositoryMock.Verify(cr => cr.SaveContacts(It.IsAny<List<Contact>>()), Times.Once);
        
    }

    [Fact]
    public void GetAllContacts_ShouldReturnListOfContacts()
    {
        // Arrange
        List<Contact> expected = [new Contact { Id = "1f25dbab-4040-42a7-b405-cc69bf545fed", FirstName = "John", LastName = "Doe", 
                                                Email = "john.doe@domain.com", Phone = "076-234 5678",
                                                StreetAddress = "Doe Street 1", PostalCode = "123 45", City = "Doe City" }];
        
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(expected);

        // Act
        var result = _contactService.GetAllContacts();

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

    [Fact]
    public void GetContactById_ShouldReturnCorrectContact()
    {
        var id = "1f25dbab-4040-42a7-b405-cc69bf545fed";
        List<Contact> expected = [new Contact { Id = "1f25dbab-4040-42a7-b405-cc69bf545fed", FirstName = "John", LastName = "Doe",
                                                Email = "john.doe@domain.com", Phone = "076-234 5678",
                                                StreetAddress = "Doe Street 1", PostalCode = "123 45", City = "Doe City" }];

        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(expected);
        IEnumerable<Contact> contacts = _contactService.GetAllContacts();

        // Act
        var result = _contactService.GetContactById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.First().Id, result.Id);
    }

    [Fact]
    public void UpdateContact_ShouldReturnTrue_AndUpdateChangedValuesToContactInList()
    {
        // Arrange
        var list = new List<Contact>()
        {
            new Contact()
            {
                Id = "1f25dbab-4040-42a7-b405-cc69bf545fed",
                FirstName = "Donald",
                LastName = "Duck",
                Email = "donald.duck@domain.com",
                Phone = "001-234 56 78",
                StreetAddress = "Duck Street 123",
                PostalCode = "123 45",
                City = "Disney Town"
            }
        };

        var contactWithUpdates = new Contact()
        {
            Id = "1f25dbab-4040-42a7-b405-cc69bf545fed",
            FirstName = "Daisy",                //Value to be updated
            LastName = "Duck",                   
            Email = "daisy.duck@domain.com",    //Value to be updated
            Phone = "001-222 22 22",            //Value to be updated
            StreetAddress = "Duck Street 123",
            PostalCode = "123 45",
            City = "Disney Town"
        };

        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>())).Returns(true);
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(list);
        IEnumerable<Contact> contacts = _contactService.GetAllContacts();

        // Act
        var result = _contactService.UpdateContact(contactWithUpdates);

        // Assert
        Assert.True(result);
        Assert.Equal(contacts.First().Id, contactWithUpdates.Id);
        Assert.Equal(contacts.First().FirstName, contactWithUpdates.FirstName);
        Assert.Equal(contacts.First().LastName, contactWithUpdates.LastName);
        Assert.Equal(contacts.First().Email, contactWithUpdates.Email);
        Assert.Equal(contacts.First().Phone, contactWithUpdates.Phone);
        Assert.Equal(contacts.First().StreetAddress, contactWithUpdates.StreetAddress);
        Assert.Equal(contacts.First().PostalCode, contactWithUpdates.PostalCode);
        Assert.Equal(contacts.First().City, contactWithUpdates.City);  
    }

    [Fact]
    public void DeleteContact_ShouldReturnTrue_AndRemoveContactFromList()
    {
        // Arrange
        var list = new List<Contact>()
        {
            new Contact()
            {
                Id = "1f25dbab-4040-42a7-b405-cc69bf545fed",
                FirstName = "Donald",
                LastName = "Duck",
                Email = "donald.duck@domain.com",
                Phone = "001-234 56 78",
                StreetAddress = "Duck Street 123",
                PostalCode = "123 45",
                City = "Disney Town"
            },
            new Contact()
            {
                Id = "7effb2e1-d2f2-4fc8-9083-de821b8678e7",
                FirstName = "Daisy",
                LastName = "Duck",
                Email = "daisy.duck@domain.com",
                Phone = "001-234 56 78",
                StreetAddress = "Duck Street 123",
                PostalCode = "123 45",
                City = "Disney Town"
            }
        };
        var firstContact = list.First();

        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>())).Returns(true);
        _contactRepositoryMock.Setup(cr => cr.GetContacts()).Returns(list);
        IEnumerable<Contact> contacts = _contactService.GetAllContacts();

        // Act
        var result = _contactService.DeleteContact(contacts.First());

        // Assert
        Assert.True(result);
        Assert.Single(contacts);
        Assert.DoesNotContain(firstContact, contacts);
    }   
}
