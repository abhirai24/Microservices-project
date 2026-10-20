using MediatR;
using Ordering.Commands;
using Ordering.Repositories;
using Ordering.Mappers;

namespace Ordering.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckoutOrderHandler> _logger;

        public CheckoutOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = request.ToEntity();
            var generatedOrder = await _orderRepository.AddAsync(orderEntity);
            var outBoxMessage = OrderMapper.ToOutBoxMessage(generatedOrder);
            await _orderRepository.AddOutboxMessageAsync(outBoxMessage);
            _logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created.");
            return generatedOrder.Id;
        }
    }
}
