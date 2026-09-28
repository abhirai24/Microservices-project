using Catalog.Response;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductByNameQuery(string Name) : IRequest<IList<ProductResponse>>;
    
}
