using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.API.DTOs;
using MiniMarket.API.Mappers;
using MiniMarket.BLL.Models;
using MiniMarket.BLL.Services.Interfaces;
using MiniMarket.Domain.CustomEnums;
using MiniMarket.Domain.Models;
using System.Security.Claims;

namespace MiniMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet(Name = "GetOrders")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        public ActionResult<List<OrderListDTO>> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 20)
        {
            IEnumerable<Order> orders = _orderService.GetAll(offset, limit);

            return Ok(orders.Select(o => o.ToOrderListDTO()).ToList());
        }

        [HttpGet("{id:guid}", Name = "GetOrderById")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDTO> GetById([FromRoute] Guid id)
        {
            Order order = _orderService.GetById(id);
            return Ok(order.ToOrderDTO());
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDTO> Create([FromBody] List<OrderProductCreate> dto)
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
            {
                if (dto == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Order order = _orderService.Create(requestUserId, dto);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order.ToOrderDTO());
            }

            return Unauthorized(new { message = "You must be logged in to create an order." });
        }

        [HttpDelete("{id:guid}", Name = "DeleteOrder")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete([FromRoute] Guid id, [FromQuery] bool paranoid = true)
        {
            if (_orderService.Delete(id, paranoid))
            {
                return NoContent();
            }
            return NotFound(new { message = "Order not found." });
        }

        [HttpPut("{id:guid}/status", Name = "UpdateOrderStatus")]
        [Authorize(Roles = nameof(UtilisateurRole.Admin))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStatus([FromRoute] Guid id, [FromBody] OrderStatus status)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status))
            {
                return BadRequest(new { message = "Invalid order status." });
            }
            Order order = _orderService.GetById(id);

            order.Status = status;

            _orderService.Update(order);
            return NoContent();
        }
    }
}
