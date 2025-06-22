namespace MiniMarket.Domain.Models
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }

        public UtilisateurOrder Order { get; set; } = null!;
        public required Product Product { get; set; }
        public required int Quantity { get; set; }
        public required double Price { get; set; }
        public required double Discount { get; set; }
    }
}