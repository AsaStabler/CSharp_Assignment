using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationWPFMainApp.ViewModels;

public partial class EditContactViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private string headline = "Edit Contact";

    [ObservableProperty]
    private Contact _contact = new();

    [RelayCommand]
    private void Update()
    {
        // Updates an existing Contact to file
        var result = _contactService.UpdateContact(Contact);
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
