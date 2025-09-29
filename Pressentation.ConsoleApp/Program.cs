using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pressentation.ConsoleApp;
using Pressentation.ConsoleApp.Services;

string filePath = @"c:\data\products.json";

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IConsoleDisplayServices, ConsoleDisplayService>();
        services.AddSingleton<IFileRepository, FileRepository>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IInputService, ConsoleInputService>();
        services.AddSingleton<IUniqueIdGenerator, UniqueIdGenerator>();

        // ProductService måste ha filePath för att fungera
        services.AddSingleton<IProductService>(sp =>
            new ProductService(
                sp.GetRequiredService<IFileService>(),
                filePath,
                sp.GetRequiredService<IUniqueIdGenerator>()));



        services.AddSingleton<ConsoleMainMenu>();
    })
    .Build();



var displayMenu = host.Services.GetRequiredService<ConsoleMainMenu>();
displayMenu.MainMenu();
