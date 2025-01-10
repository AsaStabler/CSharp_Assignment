using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationWPFMainApp.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _title = "Add New Contact";

    [RelayCommand]
    private void GoToContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }

    public AddContactViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}
