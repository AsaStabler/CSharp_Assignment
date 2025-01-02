using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(Contact contact);
    IEnumerable<Contact> GetAllContacts(); 
}



