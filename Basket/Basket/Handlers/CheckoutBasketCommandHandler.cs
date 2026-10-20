using MediatR;
using Basket.Commands;
using Basket.Queries;
using MassTransit;
using Basket.Mappers;

namespace Basket.Handlers
{
    public class CheckoutBasketCommandHandler : IRequestHandler<BasketCheckoutCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<BasketCheckoutCommand> _logger;

        public CheckoutBasketCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketCheckoutCommand> logger)
        {
            _mediator = mediator;
            _publisher = publishEndpoint;
            _logger = logger;
        }
        public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
           var dto = request.Dto;
            _logger.LogInformation("BasketCheckoutCommandHandler called with UserName: {UserName}, TotalPrice: {TotalPrice}", dto.UserName, dto.TotalPrice);
            var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(dto.UserName), cancellationToken);
            if(basketResponse is null || !basketResponse.Items.Any())
            {
                _logger.LogWarning("BasketCheckoutCommandHandler: No basket found for UserName: {UserName}", dto.UserName);
                throw new Exception($"Basket not found for user {dto.UserName}");
            }
            var basket = basketResponse.ToEntity();

            var evt = dto.ToBasketCheckoutEvent(basket);
            _logger.LogInformation("Publishing BasketCheckoutEvent for {user}", basket.UserName);
            await _publisher.Publish(evt, cancellationToken);

            await _mediator.Send(new DeleteBasketByUserNameCommand(dto.UserName), cancellationToken);
            return Unit.Value;
        }
    }
}
