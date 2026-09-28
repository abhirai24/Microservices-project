using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductsByBrand(string name);
        Task<IEnumerable<Product>> GetProductsByType(string name);

        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<Product> CreateProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string id);

        Task<ProductBrand> GetBrandByBrandIdAsync(string id);

        Task<ProductType> GetTypeByTypeIdAsync(string id);

        Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);

    }
}
