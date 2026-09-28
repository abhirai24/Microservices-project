using Basket.Mappers;
using Basket.Queries;
using Basket.Repository;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class GetBasketByUserNameHandller : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        public GetBasketByUserNameHandller(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.GetBasket(request.userName);
            if(shoppingCart == null)
            {
                return new ShoppingCartResponse(request.userName)
                {
                    Items = new List<ShoppingCartItemResponse>()
                };
            }

            return shoppingCart.ToResponse(); 
        }
    }
}
