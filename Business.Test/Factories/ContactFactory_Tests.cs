using Business.Dtos;
using Business.Factories;
using Business.Models;

namespace Business.Test.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        // Act
        var result = ContactFactory.Create();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }

    [Theory]
    [InlineData("", "", "", "", "", "", "")]
    [InlineData("Donald", "Duck", "", "", "", "123 45", "Örebro")]
    [InlineData("", "Wallin", "", "076-2345 78", "", "", "")]
    [InlineData("Karin", "Wallin", "karin.wallin@domain.com", "073 4567 89", "Torsgatan 1", "444 55", "Vallentuna")]
    public void Create_ShouldReturnContact_WhenContactRegistrationFormIsSupplied(string firstName, string lastName, string email,
                                                              string phone, string streetAddress, string postalCode, string city)
    {
        // Arrange
        ContactRegistrationForm contactRegistrationForm = new() { FirstName = firstName, LastName = lastName, Email = email,
                                         Phone = phone, StreetAddress = streetAddress, PostalCode = postalCode, City = city};

        // Act
        var result = ContactFactory.Create(contactRegistrationForm);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        Assert.Equal(contactRegistrationForm.FirstName, result.FirstName);
        Assert.Equal(contactRegistrationForm.LastName, result.LastName);
        Assert.Equal(contactRegistrationForm.Email, result.Email);
        Assert.Equal(contactRegistrationForm.Phone, result.Phone);
        Assert.Equal(contactRegistrationForm.StreetAddress, result.StreetAddress);
        Assert.Equal(contactRegistrationForm.PostalCode, result.PostalCode);
        Assert.Equal(contactRegistrationForm.City, result.City);
    }
}
