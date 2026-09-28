
using Catalog.Specifications;
using Catalog.Response;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllProductsQuery(CatalogSpecParams CatalogSpecParams) : IRequest<Pagination<ProductResponse>>
    {

    }
}
