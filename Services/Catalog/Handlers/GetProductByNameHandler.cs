
using Catalog.Mapppers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Response;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByName(request.Name);
            var productResponse =  products.ToResponseList();

            return productResponse;
        }
    }
}
