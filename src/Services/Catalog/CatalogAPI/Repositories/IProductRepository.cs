using CatalogAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Repositories
{
    public interface IProductRepository
    {
        //get data method
        Task<IEnumerable<Product>> GetProduct();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        //crud operation
        Task CreateProduct(Product product);
        Task <bool> UpdateProduct(Product product);
        Task <bool> DeleteProduct(string id);
    }
}
