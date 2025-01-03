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
            Console.WriteLine("");
            Console.WriteLine(" -------- MAIN MENU --------");
            Console.WriteLine("");
            Console.WriteLine(" 1.  Add New Contact");
            Console.WriteLine(" 2.  View All Contacts");
            Console.WriteLine(" Q.  Exit Application");
            Console.WriteLine("");
            Console.WriteLine(" ---------------------------");
            Console.WriteLine("");
            Console.Write(" Enter option: ");

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
                    OutputDialog(" Press any key to exit application");
                    //isRunning = false;
                    break;

                default:
                    OutputDialog(" Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void AddNewContact()
    {
        var contact = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine(" -------- ADD NEW CONTACT --------");
        Console.WriteLine("");

        Console.Write(" Enter First Name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write(" Enter Last Name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write(" Enter Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write(" Enter Phone number: ");
        contact.Phone = Console.ReadLine()!;

        Console.Write(" Enter Street address: ");
        contact.StreetAddress = Console.ReadLine()!;

        Console.Write(" Enter Postal code: ");
        contact.PostalCode = Console.ReadLine()!;

        Console.Write(" Enter City: ");
        contact.City = Console.ReadLine()!;

        Console.WriteLine("");

        var result = _contactService.CreateContact(contact);
        if (result)
            Console.WriteLine(" Contact was created successfully.");
        else
            Console.WriteLine(" Unable to create new contact.");

        Console.ReadKey();
    }

    public void ViewAllContacts()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine(" -------- VIEW ALL CONTACTS --------");
        Console.WriteLine("");

        foreach (var contact in _contactService.GetAllContacts())
        { 
            Console.WriteLine($" Name:    {contact.FirstName} {contact.LastName}  ({contact.Id})");
            Console.WriteLine($" Email:   {contact.Email}");
            Console.WriteLine($" Phone:   {contact.Phone}");
            Console.WriteLine($" Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }

    public void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

}
