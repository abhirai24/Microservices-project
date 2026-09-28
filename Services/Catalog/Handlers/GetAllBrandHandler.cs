using Catalog.Mapppers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Response;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, IList<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
           var brandList = await _brandRepository.GetBrandsAsync();
           return brandList.ToResponseList();
        }
    }
}
