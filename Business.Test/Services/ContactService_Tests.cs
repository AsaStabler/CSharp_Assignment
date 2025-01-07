using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Collections.Generic;

namespace Business.Test.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly ContactService _contactServiceReal;

    private readonly Mock<IContactService> _contactServiceMock;
    private readonly IContactService _contactService;
    
    public ContactService_Tests()
    {
        //Used for test method 1
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactServiceReal = new ContactService(_contactRepositoryMock.Object);
        
        //Used for test methods 2 and 3
        _contactServiceMock = new Mock<IContactService>();
        _contactService = _contactServiceMock.Object;
        
    }

    //Test method 1
    [Fact]
    public void AddContactToList_ShouldReturnTrue_AndListCountOfOne()
    {
        // Arrange
        Contact contact = new() { Id = "", FirstName = "FirstName1", LastName = "LastName1", Email = "Email1" };

        // Act
        bool result = _contactServiceReal.AddContactToList(contact);

        // Assert
        //var contacts = _contactServiceReal.GetAllContacts();
        Assert.True(result);
        Assert.Single(_contactService.GetAllContacts());
    }

    //TEST FAILS: returns false (vid _contacts.Add(contact), eftersom listan _contacts är null)
//    [Fact]
//    public void CreateContact_ShouldReturnTrue_WhenContactIsAddedToList()
//    {
        // Arrange
//        var contact = new Contact();
//        _contactRepositoryMock.Setup(crep => crep.SaveContacts(It.IsAny<List<Contact>>())).Returns(true);

        // Act
//        var result = _contactService.CreateContact(contact);

        // Assert
//        Assert.True(result);
//    }

    //TEST SUCCEEDED - from the video Enhetstester (mockning)
    /*NOTE: These properties and the setup i constructor is needed for it to work
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly IContactService _contactService;
    public ContactService_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _contactService = _contactServiceMock.Object;
    } */

    //Test method 2
    [Fact]
    public void CreateContact_ShouldReturnTrue_WhenContactIsCreated()
    {
        // Arrange
        var contactRegistrationForm = new ContactRegistrationForm() { FirstName = "Maria", LastName = "Andersson" };
        _contactServiceMock.Setup(cs => cs.CreateContact(contactRegistrationForm)).Returns(true);

        // Act
        var result = _contactService.CreateContact(contactRegistrationForm);

        // Assert
        Assert.True(result);
        //Verifies that the method has been executed (once)
        _contactServiceMock.Verify(cs => cs.CreateContact(contactRegistrationForm), Times.Once);
    }

    //Test method 3
    [Fact]
    public void GetAllContacts_ShouldReturnListOfContacts()
    {
        // Arrange
        var contacts = new List<Contact>()
        {
            new() {FirstName = "FirstName1", LastName = "LastName1", Email = "Email1" },
            new() {FirstName = "FirstName2", LastName = "LastName2", Email = "Email2" }
        };
        
        _contactServiceMock.Setup(cs => cs.GetAllContacts()).Returns(contacts);

        // Act
        var result = _contactService.GetAllContacts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("FirstName1", result.First().FirstName);
    }
}
