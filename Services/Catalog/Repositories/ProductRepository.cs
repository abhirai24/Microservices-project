using Catalog.Entities;
using Catalog.Specifications;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<ProductBrand> _brands;

        private readonly IMongoCollection<ProductType> _types;

        public ProductRepository(IConfiguration config)
        {
            var client = new MongoClient(
                config["DatabaseSettings:ConnectionString"]);

            var database = client.GetDatabase(
                config["DatabaseSettings:DatabaseName"]);

            _products = database.GetCollection<Product>(
                config["DatabaseSettings:ProductCollectionName"]);

            _brands = database.GetCollection<ProductBrand>(
                config["DatabaseSettings:BrandCollectionName"]);

            _types = database.GetCollection<ProductType>(
                config["DatabaseSettings:TypesCollectionName"]);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }   
        

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _products.DeleteOneAsync(product => product.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<ProductBrand> GetBrandByBrandIdAsync(string id)
        {
            return await _brands.Find(brand => brand.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(specParams.Search))
            {
                var search = specParams.Search.Trim();
                var pattern = System.Text.RegularExpressions.Regex.Escape(search);
                var regex = new MongoDB.Bson.BsonRegularExpression(pattern, "i");
                filter &= filterBuilder.Regex(p => p.Name, regex);
            }

            if (!string.IsNullOrWhiteSpace(specParams.BrandId))
            {
                filter &= filterBuilder.Eq("Brand.Id", specParams.BrandId);
            }

            if (!string.IsNullOrWhiteSpace(specParams.TypeId))
            {
                filter &= filterBuilder.Eq("Type.Id", specParams.TypeId);
            }

            var totalItems = await _products.CountDocumentsAsync(filter);
            var data = await ApplyDataFilter(specParams, filter);

            return new Pagination<Product>(
                specParams.PageIndex,
                specParams.PageSize,
                (int)totalItems,
                data);
        }

        private async Task<IReadOnlyCollection<Product>> ApplyDataFilter(CatalogSpecParams specParams, FilterDefinition<Product> filter)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");
            if(!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "priceasc":
                        sortDefn = Builders<Product>.Sort.Ascending("Price");
                        break;
                    case "pricedesc":
                        sortDefn = Builders<Product>.Sort.Descending("Price");
                        break;
                    default:
                        sortDefn = Builders<Product>.Sort.Ascending("Name");
                        break;
                }
            }

        return await _products
        .Find(filter)
        .Sort(sortDefn)
        .Skip((specParams.PageIndex - 1) * specParams.PageSize)
        .Limit(specParams.PageSize)
        .ToListAsync(); 
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
        {
            return await _products.Find(product => product.Brand.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.Regex(product => product.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await _products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByType(string name)
        {
            return await _products.Find(product => product.Type.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<ProductType> GetTypeByTypeIdAsync(string id)
        {
            return await _types.Find(type => type.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return result.ModifiedCount > 0;
        }
    }
}
