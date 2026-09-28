using System.Security.Cryptography.X509Certificates;

namespace Basket.Responses
{
    public record ShoppingCartResponse
    {
        public string UserName { get; init; } = string.Empty;

        public List<ShoppingCartItemResponse> Items { get; init; } = new();

        public decimal TotalPrice =>
            Items.Sum(item => item.Price * item.Quantity);

        public ShoppingCartResponse()
        {
        }

        public ShoppingCartResponse(string userName)
        {
            UserName = userName;
        }

        public ShoppingCartResponse(
            string userName,
            List<ShoppingCartItemResponse> items)
        {
            UserName = userName;
            Items = items;
        }
    }

}
