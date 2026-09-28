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

    public class CreateShoppingCartItemDto
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
