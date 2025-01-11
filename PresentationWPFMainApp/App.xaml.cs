using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationWPFMainApp.ViewModels;
using PresentationWPFMainApp.Views;
using System.Windows;

namespace PresentationWPFMainApp;


public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IFileService>(new FileService("Data", "contactlist.json"));
                services.AddSingleton<IContactRepository, ContactRepository>();
                services.AddSingleton<IContactService, ContactService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactsViewModel>();
                services.AddTransient<ContactsView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();

                services.AddTransient<DetailsContactViewModel>();
                services.AddTransient<DetailsContactView>();

                services.AddTransient<EditContactViewModel>();
                services.AddTransient<EditContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactsViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
