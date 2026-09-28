using Basket.Commands;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repository;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Apply Discount using Grpc call
            foreach(var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var shoppingCartEntity = request.ToEntity();

            // save to Redis
            var updateCart = await _basketRepository.UpdateBasket(shoppingCartEntity);
            // convert back to response
            return updateCart.ToResponse();
        }
    }
}
