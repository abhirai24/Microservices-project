using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;

        public BrandRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _brands = database.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
        }
        public async Task<ProductBrand> GetBrandByIdAsync(string id)
        {
            return await _brands.Find(brand => brand.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return await _brands.Find(_ => true).ToListAsync();
        }
        
    }
}
