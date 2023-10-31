using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductQuery, string>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public CreateProductCommandHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<string> Handle(CreateProductQuery request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
                Rating = request.Rating,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };

            await _productCollection.InsertOneAsync(product);

            return product.Id;
        }

    }
}
