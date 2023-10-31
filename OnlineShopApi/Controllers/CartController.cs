using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Resources.Queries;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var added = await _mediator.Send(command);
            if (added)
            {
                return Ok("Item added to the cart successfully.");
            }

            return BadRequest("Failed to add the item to the cart.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemCommand command)
        {
            var updated = await _mediator.Send(command);
            if (updated)
            {
                return Ok("Item updated in the cart successfully.");
            }

            return BadRequest("Failed to update the item in the cart.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCartItem([FromBody] DeleteCartCommand command)
        {
            var deleted = await _mediator.Send(command);
            if (deleted)
            {
                return Ok("Item deleted from the cart successfully.");
            }

            return BadRequest("Failed to delete the item from the cart.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCart()
        {
            var query = new GetAllCartsQuery();
            var cart = await _mediator.Send(query);

            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCart(string userId)
        {
            var query = new GetCartQuery { UserId = userId };
            var userCart = await _mediator.Send(query);

            if (userCart != null)
            {
                return Ok(userCart);
            }

            return NotFound("User cart not found.");
        }

    }
}
