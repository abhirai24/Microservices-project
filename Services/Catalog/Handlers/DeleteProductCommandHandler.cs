using Catalog.Commands;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetProductById(request.Id);
            
            if(existing == null)
            {
                throw new ApplicationException($"Current Product with productID = {request.Id} not exist");
            }

            bool deleteresponse = await _productRepository.DeleteProduct(request.Id);

            return deleteresponse;
        }
    }
}
