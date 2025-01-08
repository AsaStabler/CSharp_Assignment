using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private List<Contact> _contacts = [];

    //Constructor, where list of contacts is populated from contactlist.json (at startup of application)
    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;

        // To Do: try - catch? 
        _contacts = _contactRepository.GetContacts()!;
    }

    public bool CreateContact(ContactRegistrationForm contactRegistrationForm)
    {
        try
        {
            var contact = ContactFactory.Create(contactRegistrationForm);

            _contacts.Add(contact);

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
    

    /* TO DO: Snygga till hantering av successmeddelanden och errormeddelanden, impl. try-catch */
    public Contact? GetContactById(string id)
    {
        //Not necessary to read in to _contacts - file is read at startup of application
        Contact contact = _contacts.FirstOrDefault(x => x.Id == id)!;
        //if (contact != null)
            return contact;

        //throw new KeyNotFoundException($"Contact with id {id} was not found.");
    }

    /* TO DO: Snygga till hantering av successmeddelanden och errormeddelanden, impl. try-catch */
    public bool DeleteContact(Contact contact)
    {
        //Not necessary to read in to _contacts, since file is read at startup of application
        
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
