
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record ProductDto(
        string Id,
        string Name,
        string Description,
        string Summary,
        string ImageFile,
        BrandDto Brand,
        TypeDto Type,
        decimal Price,
        DateTimeOffset CreatedDate

    );
    public record BrandDto(
        string Id,
        string Name
    );

    public record TypeDto(
        string Id,
        string Name
    );  

    public record class CreateProductDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Description { get; init; }

        [Required]
        public string Summary { get; init; }

        
        public string? ImageFile { get; init; }

        [Required]
        public string BrandId { get; init; }

        [Required]
        public string TypeId { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; init; }
    }

    public record class UpdateProductDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Description { get; init; }

        [Required]
        public string Summary { get; init; }


        public string? ImageFile { get; init; }

        [Required]
        public string BrandId { get; init; }

        [Required]
        public string TypeId { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; init; }
    }
}
