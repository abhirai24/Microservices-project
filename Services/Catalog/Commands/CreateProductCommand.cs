using Catalog.Entities;
using Catalog.Response;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Summary { get; init; }

        public string ImageFile { get; init; }
        public string BrandId { get; init; }

        public string  TypeId { get; init; }

       
        public decimal Price { get; init; }
    }
}
