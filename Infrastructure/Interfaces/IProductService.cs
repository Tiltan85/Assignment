
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    ProductResult AddProductToList(Product product);
    ProductResult<IEnumerable<Product>> GetAllProducts();
    Product CreateProduct(string newName, decimal newPrice);
}
