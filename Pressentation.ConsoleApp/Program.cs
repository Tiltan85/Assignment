using Infrastructure.Interfaces;
using Pressentation.ConsoleApp;
using Pressentation.ConsoleApp.Services;

IInputService inputService = new ConsoleInputService();
IConsoleDisplayServices consoleDisplay = new ConsoleDisplayService();

var displayMenu = new ConsoleMainMenu(inputService, consoleDisplay);
displayMenu.MainMenu();
