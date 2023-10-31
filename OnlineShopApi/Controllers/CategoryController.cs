using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateCategory(CreateCategoryQuery command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());

            if (categories != null)
            {
                return Ok(categories);
            }

            return NotFound();
        }


        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Categorie>> GetSingleCategory(string categoryId)
        {
            var query = new GetCategoryQuery { CategoryId = categoryId };
            var category = await _mediator.Send(query);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(string categoryId, UpdateCategoryQuery updateQuery)
        {
            updateQuery.CategoryId = categoryId;

            bool updated = await _mediator.Send(updateQuery);
            if (updated)
            {
                return Ok("Category updated successfully.");
            }

            return NotFound("Category not found or no updates were made.");
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var query = new DeleteCategoryQuery { CategoryId = categoryId };
            var result = await _mediator.Send(query);

            if (result)
            {
                return Ok("Category deleted successfully.");
            }

            return NotFound("Category not found or no deletion was made.");
        }

    }
}
