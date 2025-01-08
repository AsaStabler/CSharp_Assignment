using Business.Dtos;
using Business.Helpers;
using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactRegistrationForm Create() => new();

    public static Contact Create(ContactRegistrationForm contactRegistrationForm) => new()
    {
        Id = IdGenerator.GenerateUniqueId(),
        FirstName = contactRegistrationForm.FirstName,
        LastName = contactRegistrationForm.LastName,
        Email = contactRegistrationForm.Email,
        Phone = contactRegistrationForm.Phone,
        StreetAddress = contactRegistrationForm.StreetAddress,
        PostalCode = contactRegistrationForm.PostalCode,    
        City = contactRegistrationForm.City
    }; 
}
