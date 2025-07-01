using System.ComponentModel.DataAnnotations;

namespace MiniMarket.API.DTOs
{
    public class ProductListDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
    }

    public class ProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required string Description { get; set; }
    }

    public class ProductCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters long.")]
        public required string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative integer.")]
        public required int Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public required int Discount { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters long.")]
        public required string Description { get; set; }
    }
}
