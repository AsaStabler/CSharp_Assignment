using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactRegistrationForm contactRegistrationForm);
    bool DeleteContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
    Contact? GetContactById(string id);
}



