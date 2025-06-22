namespace MiniMarket.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required string Description { get; set; }
    }
}
