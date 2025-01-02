using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    //private readonly IFileService _fileService = fileService;
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<Contact> _contacts = [];

    public bool CreateContact(Contact contact)
    {
        try
        {
            contact.Id = IdGenerator.GenerateUniqueId();

            _contacts.Add(contact);

            //var json = JsonSerializer.Serialize(contact);
            //_fileService.SaveContentToFile(json);
            //return true;

            var result = _contactRepository.SaveContacts(_contacts);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public IEnumerable<Contact> GetAllContacts()
    {
         _contacts = _contactRepository.GetContacts()!;
         return _contacts;
    }

    /*** NOT necessary to implement try-catch here - it gives the same result when returning _contacts från contactRepository.GetContacts(). ***/
    /*** ...it returns an empty list [] if something goes wrong ***/
    /*
    public IEnumerable<Contact> GetAllContacts()
    {
        try
        {
            _contacts = _contactRepository.GetContacts()!;
            return _contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }
    */
}
