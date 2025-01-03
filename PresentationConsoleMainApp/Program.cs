
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using PresentationConsoleMainApp.Dialogs;


//var dialog = new MenuDialog();
//dialog.ShowMenu();

var ServiceProvider = new ServiceCollection()
    .AddSingleton<IFileService>(new FileService("Data", "contactlist.json"))
    .AddSingleton<IContactRepository, ContactRepository>()
    .AddSingleton<IContactService, ContactService>()
    .AddTransient<MenuDialog>()
    .BuildServiceProvider();

var dialog = ServiceProvider.GetRequiredService<MenuDialog>();
dialog.ShowMenu();
