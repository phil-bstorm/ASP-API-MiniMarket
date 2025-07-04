using Microsoft.EntityFrameworkCore;
using MiniMarket.DAL.Database;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MiniMarketContext _context;

        public OrderRepository(MiniMarketContext context)
        {
            _context = context;
        }

        public Order Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(Order entity)
        {

            bool isDeleted = _context.Orders.Remove(entity).State == EntityState.Deleted;
            _context.SaveChanges();
            return isDeleted;
        }

        public IEnumerable<Order> GetAll(int offset, int limit = 20)
        {
            return _context.Orders
                .Include(o => o.Owner)
                .Skip(offset)
                .Take(limit);
        }

        public Order? GetById(Guid key)
        {
            return _context.Orders
                .Include(o => o.Owner)
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .FirstOrDefault(o => o.Id == key);
        }

        public IEnumerable<Order> GetByIds(List<Guid> keys)
        {
            return _context.Orders
                .Include(o => o.Owner)
                .Where(o => keys.Contains(o.Id));
        }

        public Order Update(Order entity)
        {
            _context.SaveChanges();
            return entity;
        }
    }
}
