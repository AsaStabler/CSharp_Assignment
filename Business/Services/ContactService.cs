using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

//BEFORE implemented reading in the list in constructor (at start of application)
//public class ContactService(IContactRepository contactRepository) : IContactService
public class ContactService : IContactService
{
    //Before ContactRepository was implemented:
    //private readonly IFileService _fileService = fileService;

    //BEFORE implemented reading in the list in constructor (at start of application)
    //private readonly IContactRepository _contactRepository = contactRepository;
    //private List<Contact> _contacts = [];

    private readonly IContactRepository _contactRepository;
    private List<Contact> _contacts;

    //Constructor, where list of contacts is populated from contactlist.json (at start of application)
    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;

        // ??? Should I handle try - catch here? Or will it be handled in the ContactRepository?
        _contacts = _contactRepository.GetContacts()!;
    }

    public bool CreateContact(Contact contact)
    {
        try
        {
            contact.Id = IdGenerator.GenerateUniqueId();

            _contacts.Add(contact);

            //Before ContactRepository was implemented:
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

    /* TO DO: Snygga till hantering av successmeddelanden och errormeddelanden */
    public Contact? GetContactById(string id)
    {
        //Not necessary to read in to _contacts - file is read at startup of application
        Contact contact = _contacts.FirstOrDefault(x => x.Id == id)!;
        //if (contact != null)
            return contact;

        //throw new KeyNotFoundException($"Contact with id {id} was not found.");
    }

    /* TO DO: Snygga till hantering av successmeddelanden och errormeddelanden */
    public bool DeleteContact(Contact contact)
    {
        //Not necessary to read in to _contacts - file is read at startup of application
        
        //Remove contact from the list of contacts, i.e. _contacts
        var result = _contacts.Remove(contact);

        //Save the updated list to file
        if (result)
        { 
            result = _contactRepository.SaveContacts(_contacts);
            return result;
        }
        return false;
    }



}
