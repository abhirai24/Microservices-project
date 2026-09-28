using MediatR;
using Basket.Responses;

namespace Basket.Queries
{
    public record GetBasketByUserNameQuery(string userName) : IRequest<ShoppingCartResponse>;
    
}
