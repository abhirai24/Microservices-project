using Discount.Grpc.Protos;
using Discount.Queries;
using Discount.Mappers;
using Grpc.Core;
using MediatR;
using Discount.Commands;


namespace Discount.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var dto = await _mediator.Send(query);
            return dto.ToModel();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cmd = request.Coupon.ToCreateCommand();
            var dto = await _mediator.Send(cmd);

            return dto.ToModel();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cmd = request.Coupon.ToUpdateCommand();
            var dto = await _mediator.Send(cmd);

            return dto.ToModel();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cmd = new DeleteDiscountCommand(request.ProductName);
            var deleted =  await _mediator.Send(cmd);

            return new DeleteDiscountResponse
            {
                Success = deleted,

            };
        }
    }
}
