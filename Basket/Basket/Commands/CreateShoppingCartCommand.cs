using Basket.Responses;
using MediatR;
using Basket.DTOs;

namespace Basket.Commands
{
    public record CreateShoppingCartCommand(string userName, List<CreateShoppingCartItemDto> Items) : IRequest<ShoppingCartResponse>;
    
}
