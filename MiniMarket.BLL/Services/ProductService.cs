using MiniMarket.BLL.CustomExceptions;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.DAL.Repositories.Interfaces;
using MiniMarket.Domain.Models;

namespace MiniMarket.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(Product entity)
        {
            return _productRepository.Create(entity);
        }

        public bool Delete(int id)
        {
            Product? existing = _productRepository.GetById(id);
            if (existing is null)
            {
                throw new NotFoundException();
            }

            return _productRepository.Delete(existing);
        }

        public IEnumerable<Product> GetAll(int offset, int limit = 20)
        {
            return _productRepository.GetAll(offset, limit);
        }

        public Product GetById(int key)
        {
            Product? existing = _productRepository.GetById(key);
            if (existing is null)
            {
                throw new NotFoundException();
            }

            return existing;
        }

        public IEnumerable<Product> GetByIds(List<int> keys)
        {
            return _productRepository.GetByIds(keys);
        }

        public Product Update(Product entity)
        {
            Product? existing = _productRepository.GetById(entity.Id);
            if (existing is null)
            {
                throw new NotFoundException();
            }

            existing.Name = entity.Name;
            existing.Price = entity.Price;
            existing.Discount = entity.Discount;
            existing.Description = entity.Description;

            return _productRepository.Update(existing);
        }
    }
}
