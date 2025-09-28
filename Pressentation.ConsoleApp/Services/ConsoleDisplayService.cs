using Infrastructure.Interfaces;

namespace Pressentation.ConsoleApp.Services;

public class ConsoleDisplayService(IProductService productService, IInputService inputService) : IConsoleDisplayServices
{
    private readonly IProductService _productService;
    private readonly IInputService _inputService;
    public void DisplayAddProduct()
    {
        Console.Clear();
        Console.WriteLine("==== Add a product ====");
        string name = _inputService.GetString("Enter product name:");
        decimal price = _inputService.GetDecimal("Enter product price:");

        var product = _productService.CreateProduct(name, price);
        var saveResult = _productService.AddProductToList(product);

        if (saveResult.Succeeded)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product saved!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(saveResult.Error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        }

    public void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("==== Main Menu ====");
        Console.WriteLine("1. View Product List");
        Console.WriteLine("2. Add a product");
    }

    public void DisplayViewList()
    {
        Console.Clear();
        Console.WriteLine("==== All products in the list ====");
        var productResult = _productService.GetAllProducts();
        if (productResult.Succeeded && productResult.Content != null)
        {
            Console.WriteLine("Id Name Price");
            foreach (var product in productResult.Content)
            {
                Console.WriteLine($"{product.Id} : {product.Name} : {product.Price}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to retrieve products: {productResult.Error}");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
