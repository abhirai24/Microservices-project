using Catalog.Commands;
using MediatR;
using Catalog.Repositories;
using Catalog.Mapppers;

namespace Catalog.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetProductById(request.Id);
            if(existing == null)
            {
                throw new ApplicationException($"Product witth Productid {request.Id} not found");
            }
            var brand = await _productRepository.GetBrandByBrandIdAsync(request.BrandId);
            var type = await _productRepository.GetTypeByTypeIdAsync(request.TypeId);

            // check if brand is exist or not
            if(brand == null || type == null)
            {
                throw new ApplicationException("Invalid Brand or type is  specified");
            }

            // brand mapping

            var updateProduct = request.ToUpdateEntity(brand, type);

            return await _productRepository.UpdateProduct(updateProduct);
        }
    }

}
