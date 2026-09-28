using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<ProductType> _types;

        public TypeRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _types = database.GetCollection<ProductType>(config["DatabaseSettings:TypesCollectionName"]);
        }
        public async Task<ProductType> GetTypeByIdAsync(string id)
        {
            return await _types.Find(type => type.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            return await _types.Find(_ => true).ToListAsync();
        }
    }
}
