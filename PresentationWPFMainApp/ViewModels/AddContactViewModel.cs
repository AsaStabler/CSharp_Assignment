using Business.Dtos;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationWPFMainApp.ViewModels;

public partial class AddContactViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private string headline = "New Contact";

    [ObservableProperty]
    private ContactRegistrationForm _contact = new();

    [RelayCommand]
    private void Save()
    {
        // Saves a new Contact to file
        var result = _contactService.CreateContact(Contact);
        if (result)
        { 
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }
}
