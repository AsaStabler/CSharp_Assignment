using Business.Factories;
using Business.Interfaces;
using Business.Services;
using System.Net.Http.Headers;

namespace PresentationConsoleMainApp.Dialogs;

public class MenuDialog(IContactService contactService)
//public class MenuDialog()
{
    private readonly IContactService _contactService = contactService;
    //private readonly IContactService _contactService = new();

    public void ShowMenu()
    {
        /*
        var isRunning = true;
        do
        {
            
        } while (isRunning);
        */

        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1.  Add New Contact");
            Console.WriteLine("2.  View All Contacts");
            Console.WriteLine("Q.  Exit Application");
            Console.WriteLine("---------------------------");
            Console.Write("Enter option: ");

            var option = Console.ReadLine()!;

            switch (option.ToLower()!)
            {
                case "1":
                    AddNewContact();
                    break;

                case "2":
                    ViewAllContacts();
                    break;

                case "q":
                    Console.Clear();
                    OutputDialog("Press any key to exit application");
                    //isRunning = false;
                    break;

                default:
                    OutputDialog("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void AddNewContact()
    {
        var contact = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("-------- ADD NEW CONTACT --------");
        Console.Write("Enter Name: ");
        contact.Name = Console.ReadLine()!;

        var result = _contactService.CreateContact(contact);
        if (result)
            Console.WriteLine("Contact was created successfully.");
        else
            Console.WriteLine("Unable to create new contact.");

        Console.ReadKey();
    }

    public void ViewAllContacts()
    {
        Console.Clear();
        Console.WriteLine("-------- VIEW ALL CONTACTS --------");

        foreach (var contact in _contactService.GetAllContacts())
             Console.WriteLine($"{contact.Name} ({contact.Id})");

        Console.ReadKey();
    }

    public void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

}
