using MiniMarket.API.DTOs;
using MiniMarket.Domain.Models;

namespace MiniMarket.API.Mappers
{
    public static class OrderMappers
    {
        public static OrderListDTO ToOrderListDTO(this UtilisateurOrder o)
        {
            return new OrderListDTO
            {
                Id = o.Id.ToString(),
                OrderDate = o.OrderDate,
                Status = o.Status
            };
        }

        public static OrderDTO ToOrderDTO(this UtilisateurOrder o)
        {
            return new OrderDTO
            {
                Id = o.Id.ToString(),
                OrderDate = o.OrderDate,
                Status = o.Status,
                Products = o.Products.Select(p => p.ToProductOrderDTO()).ToList()
            };
        }

        public static ProductOrderDTO ToProductOrderDTO(this OrderProduct op)
        {
            return new ProductOrderDTO
            {
                Product = op.Product.ToProductListDTO(),
                Quantity = op.Quantity,
                Price = op.Price,
                Discount = op.Discount
            };
        }
    }
}
