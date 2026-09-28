using Catalog.Mapppers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Response;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByBrand(request.BrandName);
            return products.ToResponseList().ToList();
        }
    }
}
