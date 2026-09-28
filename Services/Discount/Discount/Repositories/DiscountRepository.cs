using Dapper;
using Discount.Entities;
using Npgsql;

namespace Discount.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string? _connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DataBaseSettings:ConnectionSString");
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var affected = await connection.ExecuteAsync(
                "Insert Into Coupon (ProductName, Amount, Description) VALUES (@ProductName, @Amount, @Description)",
                 new {coupon.ProductName, coupon.Amounnt, coupon.Description}
                );
            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync("DELETE From Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            return affected > 0;

        }


        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * From Coupon Where ProductName = @ProductName",
                new { ProductName = productName });

            return coupon ?? new Coupon
            {
                ProductName = "No Discount",
                Amounnt = 0,
                Description = "No Discount Availabe",
            };

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amounnt = @Amount where Id = @Id", new { coupon.ProductName, coupon.Description, coupon.Amounnt });
            return affected > 0;
        }

        
    }
}
