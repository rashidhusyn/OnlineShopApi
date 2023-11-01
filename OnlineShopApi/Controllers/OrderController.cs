using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Models;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("orders")]
        public async Task<IActionResult> AddOrUpdateOrder([FromBody] AddOrUpdateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            if (orderId != null)
            {
                return Ok(new { OrderId = orderId, Message = "Order added/updated successfully." });
            }

            return BadRequest("Failed to add/update the order.");
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrdersByUser([FromQuery] GetOrdersByUserQuery query)
        {
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpGet("orders/{orderId}/status")]
        public async Task<IActionResult> CheckOrderStatus(string orderId)
        {
            var query = new CheckOrderStatusQuery { OrderId = orderId };
            var orderStatus = await _mediator.Send(query);

            if (orderStatus != OrderStatus.Unknown)
            {
                return Ok(new { OrderId = orderId, Status = orderStatus });
            }

            return NotFound("Order not found.");
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(string orderId)
        {
            var query = new GetOrderDetailsQuery { OrderId = orderId };
            var order = await _mediator.Send(query);

            if (order != null)
            {
                return Ok(order);
            }

            return NotFound("Order not found.");
        }
        [HttpDelete("orders/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderQuery command)
        {
            var deleted = await _mediator.Send(command);

            if (deleted)
            {
                return Ok("Order deleted successfully.");
            }

            return BadRequest("Failed to delete the order.");
        }
    }
}
