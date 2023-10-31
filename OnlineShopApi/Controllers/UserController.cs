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
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(CreateUserQuery command)
        {
            var userid = await _mediator.Send(command);
            return Ok(userid);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            var query = new GetUsersQuery();
            var users = await _mediator.Send(query);
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var query = new GetUserByIdQuery { UserId = id };
            var user = await _mediator.Send(query);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }


        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser(string userId, UpdateUserQuery updateQuery)
        {
            updateQuery.UserId = userId;
            bool updated = await _mediator.Send(updateQuery);

            if (updated)
            {
                return Ok("Product updated successfully.");
            }

            return NotFound("Product not found or no updates were made.");
        }



        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool deleted = await _mediator.Send(new DeleteUserQuery { UserId = userId });

            if (deleted)
            {
                return Ok("Product deleted successfully.");
            }

            return NotFound("Product not found or no deletion was made.");
        }



    }
}
