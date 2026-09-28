using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Response;
using MediatR;
using Catalog.Mapppers;


namespace Catalog.Handlers
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
    {
        private readonly ITypeRepository _typeRepository;

        public GetAllTypesHandler(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typeRepository.GetTypesAsync();
            var productList =  typeList.ToResponseList();

            return productList;
        }
    }
}
