using MiniMarket.Domain.CustomEnums;

namespace MiniMarket.API.DTOs
{
    public class OrderListDTO
    {
        public required string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required OrderStatus Status { get; set; } = OrderStatus.Pending;
    }

    public class OrderDTO
    {
        public required string Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<ProductOrderDTO> Products = [];
    }

    public class ProductOrderDTO
    {
        public ProductListDTO Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
