using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PresentationWPFMainApp.ViewModels;

public partial class GetStartedViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;
}
