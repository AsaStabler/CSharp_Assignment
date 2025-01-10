using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationWPFMainApp.ViewModels;

public partial class ContactsViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _title = "Contacts";

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    public ContactsViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}
