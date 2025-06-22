namespace MiniMarket.API.DTOs
{
    public class ProductListDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }

    public class ProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required string Description { get; set; }
    }
}
