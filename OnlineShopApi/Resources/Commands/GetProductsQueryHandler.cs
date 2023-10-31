using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public GetProductsQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productCollection
                .Find(_ => true) 
                .ToListAsync();

            return products.Select(product => new Product
            {
                
                Id = product.Id,
                Title = product.Title,
                Description= product.Description,
                Image=  product.Image,
                Rating= product.Rating,
                Price= product.Price,
                CategoryId= product.CategoryId,
            });
        }

    }
}
