namespace MiniMarket.API.DTOs
{
    public class OrderListDTO
    {
        public required string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required string Status { get; set; }
    }

    public class OrderDTO
    {
        public required string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required string Status { get; set; }
        public List<ProductOrderDTO> Products { get; set; } = [];
    }

    public class ProductOrderDTO
    {
        public ProductListDTO Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
