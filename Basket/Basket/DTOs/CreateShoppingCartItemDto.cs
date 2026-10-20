namespace Basket.DTOs
{
    public record ShoppingCartDto(
       string UserName,
       List<ShoppingCartItemDto> Items,
       decimal TotalPrice

      );

    public record ShoppingCartItemDto(
        string ProductId,
        string ProductName,
        string ImageFile,
        decimal Price,
        int Quantity
     );

    public record BasketCheckoutDto(
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string State,
        string zipcode,
        string CardName,
        string CardNumber,
        string Expiration,
        string Cvv,
        string PayementMethod,
        string Country
    );

    public class CreateShoppingCartItemDto
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
