using Business.Models;

namespace Business.Interfaces
{
    public interface IContactRepository
    {
        List<Contact>? GetContacts();
        bool SaveContacts(List<Contact> contacts);
    }
}