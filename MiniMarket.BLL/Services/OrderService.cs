using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Models;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUtilisateurService _utilisateurService;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository repository, IUtilisateurService utilisateurService, IProductService productService)
        {
            _repository = repository;
            _utilisateurService = utilisateurService;
            _productService = productService;
        }

        public Order Create(int ownerId, List<OrderProductCreate> productIds)
        {
            Utilisateur owner = _utilisateurService.GetById(ownerId);
            if (owner is null)
            {
                throw new NotFoundException($"Utilisateur with ID {ownerId} not found.");
            }

            IEnumerable<Product> products = _productService.GetByIds(productIds.Select(Opc => Opc.ProductId).ToList());
            if (products.Count() != productIds.Count)
            {
                throw new NotFoundException("One or more products not found.");
            }

            Order order = new Order
            {
                Owner = owner,
                Products = products.Select(
                    (opc, i) => new OrderProduct
                    {
                        Product = opc,
                        Price = opc.Price,
                        Discount = opc.Discount,
                        Quantity = productIds[i].Quantity,
                    })
            };

            Order newOrder = _repository.Create(order);
            return newOrder;
        }

        public Order Create(Order entity)
        {
            throw new Exception("Dont use this methode, use Create(int owner, List<int> productIds)");
        }

        public bool Delete(Guid id)
        {
            return this.Delete(id);
        }

        public bool Delete(Guid id, bool paranoid = true)
        {
            Order? order = _repository.GetById(id);
            if (order is null)
            {
                throw new NotFoundException();
            }

            if (paranoid)
            {
                order.Status = Domain.CustomEnums.OrderStatus.Cancelled;
                _repository.Update(order);
                return true;
            }

            return _repository.Delete(order);
        }

        public IEnumerable<Order> GetAll(int offset, int limit = 20)
        {
            return _repository.GetAll(offset, limit);
        }

        public Order GetById(Guid key)
        {
            Order? existing = _repository.GetById(key);
            if (existing is null)
            {
                throw new NotFoundException();
            }

            return existing;
        }

        public IEnumerable<Order> GetByIds(List<Guid> keys)
        {
            return _repository.GetByIds(keys);
        }

        public Order Update(Order entity)
        {
            Order? existing = _repository.GetById(entity.Id);
            if (existing is null)
            {
                throw new NotFoundException();
            }

            existing.Status = entity.Status;

            return _repository.Update(existing);
        }
    }
}
