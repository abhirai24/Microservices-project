using Basket.Commands;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers
{
    public static class BasketMapper
    {
        public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartResponse
            {
                UserName = shoppingCart.UserName,
                Items = shoppingCart.Items.Select(item => new ShoppingCartItemResponse
                {
                    Quantity = item.Quality,
                    ImageFile = item.ImageFile,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                }).ToList()
            };

        }

        public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
        {
            return new ShoppingCart
            {
                UserName = command.userName,

                Items = command.Items.Select(item => new ShoppingCartItem
                {
                    ProductId = item.ProductId,
                    Quality = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}
