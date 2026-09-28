

using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Response;
using Catalog.Specifications;

namespace Catalog.Mapppers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Summary = product.Summary,
                ImageFile = product.ImageFile,
                Brand = product.Brand,
                Type = product.Type,
                Price = product.Price,
                CreatedDate = product.CreatedDate
            };
        }

        public static Pagination<ProductResponse> ToResponseList(this Pagination<Product> products)
        {
            return new Pagination<ProductResponse>
            (
                products.PageIndex,
                products.PageSize,
                products.TotalItems,
                products.Data.Select(p => p.ToResponse()).ToList()
            );
        }

        public static IList<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToResponse()).ToList();
        }

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type) =>
        new Product
        {
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Brand = brand,
            Type = type,
            Price = command.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        public static Product ToUpdateEntity(this UpdateProductCommand command, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                
            };
        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if (product == null) return null;
            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Summary,
                product.Description,
                product.ImageFile,
                new BrandDto(product.Brand.Id, product.Brand.Name),
                new TypeDto(product.Type.Id, product.Type.Name),
                product.Price,
                DateTimeOffset.UtcNow
            );

        }

        public static UpdateProductCommand ToCommand(this UpdateProductDto command, string id)
        {
            return new UpdateProductCommand
            {
                Id = id,
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                BrandId = command.BrandId,
                TypeId = command.TypeId,
                

            };
        }

    }

}
