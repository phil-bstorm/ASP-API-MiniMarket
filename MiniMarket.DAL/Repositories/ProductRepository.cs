using Microsoft.EntityFrameworkCore;
using MiniMarket.DAL.Database;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MiniMarketContext _context;

        public ProductRepository(MiniMarketContext context)
        {
            _context = context;
        }

        public Product Create(Product entity)
        {
            Product newProduct = _context.Products.Add(entity).Entity;
            _context.SaveChanges();
            return newProduct;
        }

        public bool Delete(Product entity)
        {
            bool isDeleted = _context.Products.Remove(entity).State == EntityState.Deleted;
            _context.SaveChanges();
            return isDeleted;
        }

        public IEnumerable<Product> GetAll(int offset, int limit = 20)
        {
            return _context.Products
                .Skip(offset)
                .Take(limit);
        }

        public Product? GetById(int key)
        {
            return _context.Products
                .FirstOrDefault(p => p.Id == key);
        }

        public IEnumerable<Product> GetByIds(List<int> keys)
        {
            return _context.Products
                .Where(p => keys.Contains(p.Id));
        }

        public Product Update(Product entity)
        {
            //_context.Products.Update(entity); pas nécessaire car le "tracking" est activé
            _context.SaveChanges();
            return entity;
        }
    }
}
