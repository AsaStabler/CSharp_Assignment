using Business.Factories;
using Business.Models;

namespace Business.Test.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        //arrange
        ContactRegistrationForm contactRegistrationForm = new();

        //act
        ContactRegistrationForm result = ContactFactory.Create();

        //assert
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }

    /*** To Do: Add more test date in InlineData ***/
    [Theory]
    [InlineData("", "")]
    [InlineData("Karin", "")]
    [InlineData("", "Wallin")]
    [InlineData("Karin", "Wallin")]
    public void Create_ShouldReturnContact_WhenContactRegistrationFormIsSupplied(string firstName, string lastName)
    {
        //arrange
        //ContactRegistrationForm contactRegistrationForm = new() { FirstName = "Karin", LastName = "Wallin" };
        ContactRegistrationForm contactRegistrationForm = new() { FirstName = firstName, LastName = lastName };

        //act
        Contact result = ContactFactory.Create(contactRegistrationForm);

        //assert
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
