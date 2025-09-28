
using Infrastructure.Interfaces;
using System.Runtime.CompilerServices;

namespace Pressentation.ConsoleApp;

public class ConsoleMainMenu(IInputService inputService, IConsoleDisplayServices consoleDisplayServices)
{
    private readonly IInputService _inputService = inputService;
    private readonly IConsoleDisplayServices _consoleDisplayServices1 = consoleDisplayServices;

    public void MainMenu()
    {
        

        while (true)
        {

            _consoleDisplayServices1.DisplayMainMenu();

            Console.Write("Choose an option");
            string option = _inputService.GetString("Choose an option from the menu");
            //string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    _consoleDisplayServices1.DisplayViewList();
                    break;

                    case "2":
                    _consoleDisplayServices1.DisplayAddProduct();
                    break;

                    default:
                    break;
            }
        }
    }
}
