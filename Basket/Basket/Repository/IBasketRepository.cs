using Basket.Entities;

namespace Basket.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart shopping);

        Task DeleteBasket(string userName);
    }
}
