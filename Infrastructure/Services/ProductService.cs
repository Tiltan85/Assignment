
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Text.Json;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    public readonly string _path;
    private List<Product> _productList;
    private readonly IFileService _fileService;
    private readonly IUniqueIdGenerator _uniqueIdGenerator;

    public ProductService(IFileService fileService, string filePath, IUniqueIdGenerator uniqueIdGenerator)
    {
        _path = filePath;
        _productList = [];
        _fileService = fileService;
        _uniqueIdGenerator = uniqueIdGenerator;
        
        // Lägg till allt i listan
    }

    public ProductResult AddProductToList(Product product)
    {

        try
        {
            _productList.Add(product);
            string jsonList = JsonSerializer.Serialize(_productList); 

            var result = _fileService.SaveContentToFile(_path, jsonList);
            // Lägg till product i filen
            if (result.Succeeded)
                return new ProductResult { Succeeded = true };

            // Det gick inte att spara, ta bort filen ur listan så att listan och filen matchar
            _productList.Remove(product);
            return new ProductResult { Succeeded = false, Error = result.Error};
        }
        catch (Exception ex) 
        {
            return new ProductResult { Succeeded = false, Error= ex.Message };
        }
        
        
    }

    
    public ProductResult<IEnumerable<Product>> GetAllProducts()
    {
        var result = _fileService.GetContentFromFile(_path);
        // If Successful and there is content
        if (result.Succeeded && !string.IsNullOrEmpty(result.Content))
        {
            // Desserialize Json to a list, or make an empty list
            _productList = JsonSerializer.Deserialize<List<Product>>(result.Content) ?? new List<Product>();
            // Return true and the list with content. 
            return new ProductResult<IEnumerable<Product>> { Succeeded = true, Content = _productList };
        }
        // No content found
        return new ProductResult<IEnumerable<Product>> { Succeeded = false, Error = result.Error, Content = _productList };
    }

    public Product CreateProduct(string name, decimal price)
    {
        // Generate ID
        string id = _uniqueIdGenerator.Generate();
        return new Product() { Id = id, Name = name, Price = price };
        
    }
}
