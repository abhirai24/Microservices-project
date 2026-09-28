using Catalog.Mapppers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Response;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductBByIdQuery, ProductResponse>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(GetProductBByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductById(request.Id);
            var productResponse = product.ToResponse();

            return productResponse;
        }
    }
}
