using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct(CreateProductQuery command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

           var query = new GetProductsQuery();
           var products = await _mediator.Send(query);
           return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery { ProductId = id };
            var product = await _mediator.Send(query);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound(); 
        }

        [HttpGet("Limit")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLimitedProducts(int limit = 2)  
        {
            var products = await _mediator.Send(new GetLimitedProductsQuery { Limit = limit });
            return Ok(products);
        }


        [HttpGet("products/sort")]
        public async Task<ActionResult<IEnumerable<Product>>> SortProducts(string sortOrder)
        {
            try
            {
                var result = await _mediator.Send(new SortProductsQuery { SortOrder = sortOrder });

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("products/category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInCategory(string category)
        {
            try
            {
                var result = await _mediator.Send(new GetProductsInCategoryQuery { Category = category });

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, UpdateProductQuery updateQuery)
        {
            updateQuery.ProductId = productId;
            bool updated = await _mediator.Send(updateQuery);

            if (updated)
            {
                return Ok("Product updated successfully.");
            }

            return NotFound("Product not found or no updates were made.");
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            bool deleted = await _mediator.Send(new DeleteProductQuery { ProductId = productId });

            if (deleted)
            {
                return Ok("Product deleted successfully.");
            }

            return NotFound("Product not found or no deletion was made.");
        }





    }
}
