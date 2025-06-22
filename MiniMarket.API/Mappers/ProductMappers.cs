using MiniMarket.API.DTOs;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Mappers
{
    public static class ProductMappers
    {
        public static ProductListDTO ToProductListDTO(this Product p)
        {
            return new ProductListDTO
            {
                Id = p.Id,
                Name = p.Name
            };
        }

        public static ProductDTO ToProductDTO(this Product p)
        {
            return new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                Description = p.Description
            };
        }
    }
}
