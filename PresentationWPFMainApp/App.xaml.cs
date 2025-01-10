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
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactsViewModel>();
                services.AddTransient<ContactsView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    { 
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
