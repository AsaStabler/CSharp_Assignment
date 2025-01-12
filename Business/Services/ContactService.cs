using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<Contact> _contacts = [];

    public bool CreateContact(ContactRegistrationForm contactRegistrationForm)
    {
        try
        {
            var contact = ContactFactory.Create(contactRegistrationForm);

            _contacts.Add(contact);

            return _contactRepository.SaveContacts(_contacts);
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
    
    public Contact? GetContactById(string id)
    {
        try
        { 
            Contact contact = _contacts.FirstOrDefault(x => x.Id == id)!;
            return contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    public bool UpdateContact(Contact contact)
    {
        try
        {
            int index = _contacts.FindIndex(x => x.Id == contact.Id);

            if (index > -1)
            {
                _contacts[index].FirstName = contact.FirstName;
                _contacts[index].LastName = contact.LastName;
                _contacts[index].Email = contact.Email;
                _contacts[index].Phone = contact.Phone;
                _contacts[index].StreetAddress = contact.StreetAddress;
                _contacts[index].PostalCode = contact.PostalCode;
                _contacts[index].City = contact.City;

                return _contactRepository.SaveContacts(_contacts);
            }
            else
                return false;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteContact(Contact contact)
    {
        try
        {
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
