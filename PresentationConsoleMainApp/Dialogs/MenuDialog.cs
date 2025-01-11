using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace PresentationConsoleMainApp.Dialogs;

public class MenuDialog(IContactService contactService)
{
    private readonly IContactService _contactService = contactService;

    public void ShowMenu()
    {
        _contactService.GetAllContacts();

        var isRunning = true;

        //while (true)
        do
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(" -------- MAIN MENU --------");
            Console.WriteLine("");
            Console.WriteLine(" 1.  Add New Contact");
            Console.WriteLine(" 2.  View All Contacts");
            Console.WriteLine(" 3.  View Contact (by id)");
            Console.WriteLine(" 4.  Delete Contact (by id)");
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

                case "3":
                    ViewOneContact();
                    break;

                case "4":
                    DeleteContact();
                    break;

                case "q":
                    Console.Clear();
                    OutputDialog(" Press any key to exit application");
                    isRunning = false;
                    break;

                default:
                    OutputDialog(" Invalid option. Please try again.");
                    break;
            }
     // }
        } while (isRunning);
    }

    public void AddNewContact()
    {
        var form = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine(" -------- ADD NEW CONTACT --------");
        Console.WriteLine("");

        Console.Write(" Enter First Name: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write(" Enter Last Name: ");
        form.LastName = Console.ReadLine()!;

        Console.Write(" Enter Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write(" Enter Phone number: ");
        form.Phone = Console.ReadLine()!;

        Console.Write(" Enter Street address: ");
        form.StreetAddress = Console.ReadLine()!;

        Console.Write(" Enter Postal code: ");
        form.PostalCode = Console.ReadLine()!;

        Console.Write(" Enter City: ");
        form.City = Console.ReadLine()!;

        Console.WriteLine("");

        var result = _contactService.CreateContact(form);
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

        var contacts = _contactService.GetAllContacts();
   
        foreach (var contact in contacts)
        {
            ShowContact(contact);
        }
        
        Console.ReadKey();
    }

    public void ViewOneContact()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine(" -------- VIEW CONTACT (by Id) --------");
        Console.WriteLine("");

        Console.Write(" Enter Id: ");
        var Id = Console.ReadLine()!;
        Console.WriteLine("");

        var contact = _contactService.GetContactById(Id)!;

        if (contact != null)
        { 
            ShowContact(contact);
        }
        else
            Console.WriteLine($" No contact with id: ({Id}) could be found.");

        Console.ReadKey();
    }

    public void DeleteContact()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine(" -------- DELETE CONTACT (by Id) --------");
        Console.WriteLine("");

        Console.Write(" Enter Id: ");
        var Id = Console.ReadLine()!;
        Console.WriteLine("");

        var contact = _contactService.GetContactById(Id)!;

        if (contact != null)
        {
            //Console.WriteLine($" Name: {contact.FirstName} {contact.LastName}  ({contact.Id})");
            var result = _contactService.DeleteContact(contact);
            if (result)
                Console.WriteLine(" Contact was deleted successfully.");
            else
                Console.WriteLine(" Unable to delete contact.");
        }
        else
            Console.WriteLine($" No contact with id: ({Id}) could be found.");

        Console.ReadKey();
    }

    public void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public void ShowContact(Contact contact)
    {
        Console.WriteLine($" Name:    {contact.FirstName} {contact.LastName}  ({contact.Id})");
        Console.WriteLine($" Email:   {contact.Email}");
        Console.WriteLine($" Phone:   {contact.Phone}");
        Console.WriteLine($" Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
        Console.WriteLine("");
    }

}
