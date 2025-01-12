using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace PresentationWPFMainApp.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    [ObservableProperty]
    private string headline = "Contact List";

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    public ContactsViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        //Populates the Contact list from file
        _contacts = new ObservableCollection<Contact>(_contactService.GetAllContacts());
    }

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToDetailsContact(Contact contact)
    {
        var detailsContactViewModel = _serviceProvider.GetRequiredService<DetailsContactViewModel>();
        detailsContactViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = detailsContactViewModel;
    }
}
