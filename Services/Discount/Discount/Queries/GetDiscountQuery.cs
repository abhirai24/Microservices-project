using MediatR;
using Discount.DTOs;

namespace Discount.Queries
{
        public record GetDiscountQuery(string productName) : IRequest<CouponDto>;
}
