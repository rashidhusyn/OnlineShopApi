using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class DeleteProductQueryHandler : IRequestHandler<DeleteProductQuery, bool>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public DeleteProductQueryHandler(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<bool> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.ProductId);
            var result = await _productCollection.DeleteOneAsync(filter, cancellationToken);

            return result.DeletedCount > 0;
        }

    }
}
