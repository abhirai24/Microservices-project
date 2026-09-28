using Catalog.Entities;
using MongoDB.Driver;
using System.Text.Json;


namespace Catalog.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(IConfiguration configuration)
        {
            var client = new MongoDB.Driver.MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            var brandsCollection = database.GetCollection<Entities.ProductBrand>(configuration["DatabaseSettings:BrandCollectionName"]);
            var typesCollection = database.GetCollection<Entities.ProductType>(configuration["DatabaseSettings:TypesCollectionName"]);
            var productsCollection = database.GetCollection<Entities.Product>(configuration["DatabaseSettings:ProductCollectionName"]);
            
            var SeedBasePath = Path.Combine("Data", "SeedData");

            List<ProductBrand> brandsList = new List<ProductBrand>();
            List<ProductType> typesList = new List<ProductType>();
            List<Product> productsList = new List<Product>(); 

            // Seed Brands
            if (await brandsCollection.CountDocumentsAsync(_ => true) == 0)
            {
                var brandsData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "brands.json"));
                brandsList  = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brandsList != null)
                {
                    await brandsCollection.InsertManyAsync(brandsList);
                }
                else
                {
                    brandsList = await brandsCollection.Find(_ => true).ToListAsync();
                }
            }

            // Seed Types
            if (await typesCollection.CountDocumentsAsync(_ => true) == 0)
            {
                var typesData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "types.json"));
                typesList = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (typesList != null)
                {
                    await typesCollection.InsertManyAsync(typesList);
                }
                else
                {
                    typesList = await typesCollection.Find(_ => true).ToListAsync();
                }
            }

            // Seed Products
            if (await productsCollection.CountDocumentsAsync(_ => true) == 0)
            {
                var productsData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "products.json"));
                productsList = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (productsList != null)
                {
                    foreach (var product in productsList)
                    {
                        product.Id = null;
                        // Default Created Date
                        if(product.CreatedDate == default)
                        {
                            product.CreatedDate = DateTimeOffset.UtcNow;
                        }
                    }
                    await productsCollection.InsertManyAsync(productsList);
                }
                else
                {
                    productsList = await productsCollection.Find(_ => true).ToListAsync();
                }
            }
        }
    }
}
