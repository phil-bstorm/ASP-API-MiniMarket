using MiniMarket.Domain.CustomEnums;

namespace MiniMarket.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required Utilisateur Owner { get; set; } = null!;
        public List<OrderProduct> Products { get; set; } = [];
    }
}
