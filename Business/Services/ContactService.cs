using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Text.Json;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private List<Contact> _contacts = [];

    public bool CreateContact(Contact contact)
    {
        contact.Id = IdGenerator.GenerateUniqueId();

        _contacts.Add(contact);

        var json = JsonSerializer.Serialize(contact);
        _fileService.SaveContentToFile(json);
        return true;
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        throw new NotImplementedException();
    }
}
