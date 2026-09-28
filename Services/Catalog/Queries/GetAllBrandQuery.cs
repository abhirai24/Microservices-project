using MediatR;
using Catalog.Response;

namespace Catalog.Queries
{
    public class GetAllBrandQuery : IRequest<IList<BrandResponse>>
    {

    }
}
