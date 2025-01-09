using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Business.Repositories;

public class ContactRepository(IFileService fileService) : IContactRepository
{
    private readonly IFileService _fileService = fileService;

    public bool SaveContacts(List<Contact> contacts)
    {
        try
        {
            var json = JsonSerializer.Serialize(contacts);
            _fileService.SaveContentToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> GetContacts()
    {
        try
        {
            var json = _fileService.GetContentFromFile();
            var contacts = JsonSerializer.Deserialize<List<Contact>>(json);

            if (contacts != null) 
                return contacts;
            else
                return [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }
}
