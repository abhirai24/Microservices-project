using Catalog.Response;
using MediatR;

namespace Catalog.Queries
{
    public class GetAllTypesQuery : IRequest<IList<TypeResponse>>
    {

    }
}
