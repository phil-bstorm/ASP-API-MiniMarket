using MiniMarket.BLL.Models;
using MiniMarket.Domain.Models;

namespace MiniMarket.BLL.Services.Interfaces
{
    public interface IOrderService : IService<Guid, Order>
    {
        public Order Create(int ownerId, List<OrderProductCreate> productIds);
        public bool Delete(Guid id, bool paranoid = true);
    }
}
