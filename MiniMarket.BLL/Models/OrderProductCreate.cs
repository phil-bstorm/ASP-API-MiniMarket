namespace MiniMarket.BLL.Models
{
    public class OrderProductCreate
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public OrderProductCreate(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        public OrderProductCreate() { } // Parameterless constructor for deserialization
    }
}
