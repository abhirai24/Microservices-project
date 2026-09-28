
using Catalog.Response;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductBByIdQuery(string Id) : IRequest<ProductResponse>;
   
}
