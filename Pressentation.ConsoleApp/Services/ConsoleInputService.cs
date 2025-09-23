using Infrastructure.Interfaces;
namespace Pressentation.ConsoleApp.Services;

public class ConsoleInputService : IInputService
{
    public decimal GetDecimal(string promt)
    {
        Console.Write($"{promt}: ");
        decimal input;

        while (!decimal.TryParse(Console.ReadLine(), out input))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid number, try again.");
            Console.ResetColor();
        }

        return input;
    }

    public string GetString(string promt)
    {
        Console.Write($"{promt}: ");
        var input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input)) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input cannot be empty, try again.");
            Console.ResetColor();
        }

        return input;
    }
}
